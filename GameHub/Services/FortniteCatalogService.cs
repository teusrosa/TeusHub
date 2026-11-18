using System.Net;
using System.Net.Http.Json;
using GameHub.Models;

namespace GameHub.Services;

public class ApiResponse<T>
{
    public int Status { get; set; }
    public T? Data { get; set; }
}

/// <summary>API 2: Fortnite-API (busca de cosméticos do Battle Royale).</summary>
public class FortniteCatalogService : CatalogServiceBase
{
    private const string DefaultTerm = "skull"; // termo inicial para a lista não começar vazia

    public override GameDefinition Game => GameDefinitions.Fortnite;

    public override async Task<IReadOnlyList<GameItem>> GetItemsAsync(string? search, CancellationToken ct = default)
    {
        var term = string.IsNullOrWhiteSpace(search) ? DefaultTerm : search.Trim();
        var url = "https://fortnite-api.com/v2/cosmetics/br/search/all" +
                  $"?name={Uri.EscapeDataString(term)}&matchMethod=contains&language=pt-BR";

        using var resp = await Http.GetAsync(url, ct);
        if (resp.StatusCode == HttpStatusCode.NotFound)
            return Array.Empty<GameItem>(); // a API responde 404 quando nada é encontrado

        resp.EnsureSuccessStatusCode();
        var body = await resp.Content.ReadFromJsonAsync<ApiResponse<List<CosmeticItem>>>(JsonOptions, ct);

        return (body?.Data ?? new List<CosmeticItem>()).Take(100).Cast<GameItem>().ToList();
    }
}
