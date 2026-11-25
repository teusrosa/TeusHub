using GameHub.Models;

namespace GameHub.Services;
//pega as info da api
public class Cs2CatalogService : CachedFileCatalogService<ItemSkin>
{
    public override DefinicaoJogo Game => DefinicoesJogos.Cs2;

    protected override string Url =>
        "https://raw.githubusercontent.com/ByMykel/CSGO-API/main/public/api/en/skins.json";

    protected override string CacheFileName => "cs2_skins.json";

    protected override int MaxResults => 300;

    protected override bool Matches(ItemSkin s, string term) =>
        s.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
        s.Weapon?.Name?.Contains(term, StringComparison.OrdinalIgnoreCase) == true ||
        s.Rarity?.Name?.Contains(term, StringComparison.OrdinalIgnoreCase) == true;
}
