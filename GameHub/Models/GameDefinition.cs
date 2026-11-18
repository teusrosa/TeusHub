namespace GameHub.Models;

/// <summary>Descreve um jogo do app e liga as 3 APIs de dados à API RAWG (nome usado na busca).</summary>
public sealed record GameDefinition(string Id, string DisplayName, string RawgSearchName, string Emoji, string Description)
{
    /// <summary>Rota da aba (rodapé) deste jogo no Shell.</summary>
    public string Route => Id;
}

public static class GameDefinitions
{
    public static readonly GameDefinition GtaV = new("gta", "GTA V", "Grand Theft Auto V", "🚗",
        "Veículos do jogo (GTA V Data Dumps)");

    public static readonly GameDefinition Fortnite = new("fortnite", "Fortnite", "Fortnite", "🪂",
        "Skins e cosméticos (Fortnite-API)");

    public static readonly GameDefinition Cs2 = new("cs2", "Counter-Strike 2", "Counter-Strike 2", "🔫",
        "Skins de armas (CSGO-API)");

    public static IReadOnlyList<GameDefinition> All { get; } = new[] { GtaV, Fortnite, Cs2 };

    public static GameDefinition ById(string id) => All.FirstOrDefault(g => g.Id == id) ?? GtaV;
}
