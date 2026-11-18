using GameHub.Models;

namespace GameHub.Services;

/// <summary>API 3: CSGO-API do ByMykel (skins.json). O arquivo é grande, por isso usa cache.</summary>
public class Cs2CatalogService : CachedFileCatalogService<SkinItem>
{
    public override GameDefinition Game => GameDefinitions.Cs2;

    protected override string Url =>
        "https://raw.githubusercontent.com/ByMykel/CSGO-API/main/public/api/en/skins.json";

    protected override string CacheFileName => "cs2_skins.json";

    // Evita montar uma lista gigante na tela.
    protected override int MaxResults => 300;

    protected override bool Matches(SkinItem s, string term) =>
        s.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
        s.Weapon?.Name?.Contains(term, StringComparison.OrdinalIgnoreCase) == true ||
        s.Rarity?.Name?.Contains(term, StringComparison.OrdinalIgnoreCase) == true;
}
