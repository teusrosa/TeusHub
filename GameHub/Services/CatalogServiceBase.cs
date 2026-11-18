using System.Text.Json;
using GameHub.Models;
using Microsoft.Maui.Storage;

namespace GameHub.Services;

public abstract class CatalogServiceBase : ICatalogService
{
    protected static readonly HttpClient Http = CreateHttp();
    protected static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public abstract GameDefinition Game { get; }
    public abstract Task<IReadOnlyList<GameItem>> GetItemsAsync(string? search, CancellationToken ct = default);

    private static HttpClient CreateHttp()
    {
        var http = new HttpClient { Timeout = TimeSpan.FromSeconds(120) };
        http.DefaultRequestHeaders.UserAgent.ParseAdd("GameHubApp/1.0");
        return http;
    }

    protected static async Task<List<T>> GetListCachedAsync<T>(string url, string fileName, CancellationToken ct)
    {
        var path = Path.Combine(FileSystem.CacheDirectory, fileName);

        if (!File.Exists(path))
        {
            var temp = path + ".tmp";
            using (var resp = await Http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, ct))
            {
                resp.EnsureSuccessStatusCode();
                await using var fs = File.Create(temp);
                await resp.Content.CopyToAsync(fs, ct);
            }
            File.Move(temp, path, overwrite: true);
        }

        try
        {
            await using var stream = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<List<T>>(stream, JsonOptions, ct) ?? new List<T>();
        }
        catch (JsonException)
        {
            File.Delete(path);
            throw;
        }
    }
}

public abstract class CachedFileCatalogService<T> : CatalogServiceBase where T : GameItem
{
    private List<T>? _all;

    protected abstract string Url { get; }
    protected abstract string CacheFileName { get; }
    protected abstract bool Matches(T item, string term);
    protected virtual int MaxResults => int.MaxValue;
    protected virtual IEnumerable<T> Order(IEnumerable<T> items) => items;

    public override async Task<IReadOnlyList<GameItem>> GetItemsAsync(string? search, CancellationToken ct = default)
    {
        _all ??= await GetListCachedAsync<T>(Url, CacheFileName, ct);

        IEnumerable<T> query = _all;
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(i => Matches(i, term));
        }

        return Order(query).Take(MaxResults).Cast<GameItem>().ToList();
    }
}
