using System.Net;
using System.Net.Http.Json;
using GameHub.Models;

namespace GameHub.Services;
//info da api do fortnite
public class ApiResponse<T>
{
    public int Status { get; set; }
    public T? Data { get; set; }
}

public class FortniteCatalogService : CatalogServiceBase
{
    private const string DefaultTerm = "skull";

    public override DefinicaoJogo Game => DefinicoesJogos.Fortnite;

    public override async Task<IReadOnlyList<ItemJogo>> GetItemsAsync(string? search, CancellationToken ct = default)
    {
        var term = string.IsNullOrWhiteSpace(search) ? DefaultTerm : search.Trim();
        var url = "https://fortnite-api.com/v2/cosmetics/br/search/all" +
                  $"?name={Uri.EscapeDataString(term)}&matchMethod=contains&language=pt-BR";

        using var resp = await Http.GetAsync(url, ct);
        if (resp.StatusCode == HttpStatusCode.NotFound)
            return Array.Empty<ItemJogo>();

        resp.EnsureSuccessStatusCode();
        var body = await resp.Content.ReadFromJsonAsync<ApiResponse<List<ItemCosmetico>>>(JsonOptions, ct);

        return (body?.Data ?? new List<ItemCosmetico>()).Take(100).Cast<ItemJogo>().ToList();
    }
}
