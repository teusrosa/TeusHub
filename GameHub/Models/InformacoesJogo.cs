namespace GameHub.Models;
//usada info do jogo, tipo jogo/nota
public class InformacoesJogo
{
    public string Name { get; init; } = "";
    public string? Released { get; init; }
    public double Rating { get; init; }
    public int? Metacritic { get; init; }
    public int Playtime { get; init; }
    public string? Description { get; init; }
    public string? BackgroundImage { get; init; }
    public string? Website { get; init; }
    public List<string> Genres { get; init; } = new();
    public List<string> Developers { get; init; } = new();
    public List<string> Publishers { get; init; } = new();
    public List<string> Platforms { get; init; } = new();

    public IReadOnlyList<LinhaDetalhe> Rows => new List<LinhaDetalhe>
    {
        new("Lançamento", Released ?? "—"),
        new("Nota dos jogadores", Rating > 0 ? $"{Rating:0.0} / 5" : "—"),
        new("Metacritic", Metacritic?.ToString() ?? "—"),
        new("Tempo médio", Playtime > 0 ? $"{Playtime} h" : "—"),
        new("Gêneros", Join(Genres)),
        new("Desenvolvedora", Join(Developers)),
        new("Publicadora", Join(Publishers)),
        new("Plataformas", Join(Platforms)),
    };

    private static string Join(List<string> list) => list.Count == 0 ? "—" : string.Join(", ", list);
}
