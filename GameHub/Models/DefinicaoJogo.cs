namespace GameHub.Models;
//info de cada jogo
public sealed record DefinicaoJogo(string Id, string DisplayName, string RawgSearchName, string Emoji, string Description)
{
    public string Route => Id;
}

public static class DefinicoesJogos
{
    public static readonly DefinicaoJogo GtaV = new("gta", "GTA V", "Grand Theft Auto V", "🚗",
        "Veículos do jogo (GTA V Data Dumps)");

    public static readonly DefinicaoJogo Fortnite = new("fortnite", "Fortnite", "Fortnite", "🪂",
        "Skins e cosméticos (Fortnite-API)");

    public static readonly DefinicaoJogo Cs2 = new("cs2", "Counter-Strike 2", "Counter-Strike 2", "🔫",
        "Skins de armas (CSGO-API)");

    public static IReadOnlyList<DefinicaoJogo> All { get; } = new[] { GtaV, Fortnite, Cs2 };

    public static DefinicaoJogo ById(string id) => All.FirstOrDefault(g => g.Id == id) ?? GtaV;
}
