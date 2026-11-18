using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GameHub.Models;

namespace GameHub.Services;
//info da api do rawg
public class RawgGameInfoService : IGameInfoService
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(30) };
    private readonly Dictionary<string, GameInfo> _cache = new();

    public async Task<GameInfo?> GetAsync(GameDefinition game, CancellationToken ct = default)
    {
        if (!AppConfig.HasRawgKey)
            throw new InvalidOperationException(
                "Criar sua key na Api do Rawg");

        if (_cache.TryGetValue(game.Id, out var cached))
            return cached;

        var key = Uri.EscapeDataString(AppConfig.RawgApiKey);
        var search = await Http.GetFromJsonAsync<RawgSearch>(
            $"https://api.rawg.io/api/games?key={key}&search={Uri.EscapeDataString(game.RawgSearchName)}&page_size=1", ct);

        var id = search?.Results.FirstOrDefault()?.Id;
        if (id is null) return null;

        var d = await Http.GetFromJsonAsync<RawgGame>($"https://api.rawg.io/api/games/{id}?key={key}", ct);
        if (d is null) return null;

        var info = new GameInfo
        {
            Name = d.Name ?? game.DisplayName,
            Released = d.Released,
            Rating = d.Rating,
            Metacritic = d.Metacritic,
            Playtime = d.Playtime,
            Description = d.DescriptionRaw,
            BackgroundImage = d.BackgroundImage,
            Website = d.Website,
            Genres = Names(d.Genres),
            Developers = Names(d.Developers),
            Publishers = Names(d.Publishers),
            Platforms = d.Platforms.Select(p => p.Platform?.Name).Where(n => !string.IsNullOrEmpty(n)).Select(n => n!).ToList()
        };

        _cache[game.Id] = info;
        return info;
    }

    private static List<string> Names(List<RawgNamed> list) =>
        list.Select(n => n.Name).Where(n => !string.IsNullOrEmpty(n)).Select(n => n!).ToList();

    private class RawgSearch
    {
        [JsonPropertyName("results")] public List<RawgGame> Results { get; set; } = new();
    }

    private class RawgNamed
    {
        [JsonPropertyName("name")] public string? Name { get; set; }
    }

    private class RawgPlatformWrapper
    {
        [JsonPropertyName("platform")] public RawgNamed? Platform { get; set; }
    }

    private class RawgGame
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("released")] public string? Released { get; set; }
        [JsonPropertyName("rating")] public double Rating { get; set; }
        [JsonPropertyName("metacritic")] public int? Metacritic { get; set; }
        [JsonPropertyName("playtime")] public int Playtime { get; set; }
        [JsonPropertyName("description_raw")] public string? DescriptionRaw { get; set; }
        [JsonPropertyName("background_image")] public string? BackgroundImage { get; set; }
        [JsonPropertyName("website")] public string? Website { get; set; }
        [JsonPropertyName("genres")] public List<RawgNamed> Genres { get; set; } = new();
        [JsonPropertyName("developers")] public List<RawgNamed> Developers { get; set; } = new();
        [JsonPropertyName("publishers")] public List<RawgNamed> Publishers { get; set; } = new();
        [JsonPropertyName("platforms")] public List<RawgPlatformWrapper> Platforms { get; set; } = new();
    }
}
