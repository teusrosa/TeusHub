using System.Text.Json.Serialization;

namespace GameHub.Models;

public class ReferenciaNomeada
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}

public class ReferenciaRaridade : ReferenciaNomeada
{
    public string? Color { get; set; }
}

//cs
public class ItemSkin : ItemJogo
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public ReferenciaNomeada? Weapon { get; set; }
    public ReferenciaNomeada? Category { get; set; }
    public ReferenciaNomeada? Pattern { get; set; }
    public ReferenciaRaridade? Rarity { get; set; }
    public ReferenciaNomeada? Team { get; set; }
    public List<ReferenciaNomeada>? Wears { get; set; }
    public bool Stattrak { get; set; }
    public bool Souvenir { get; set; }
    public string? Image { get; set; }

    [JsonPropertyName("min_float")] public double? MinFloat { get; set; }
    [JsonPropertyName("max_float")] public double? MaxFloat { get; set; }

    public override DefinicaoJogo Game => DefinicoesJogos.Cs2;
    public override string Key => Id;
    public override string Title => Name;
    public override string Subtitle => $"{Category?.Name ?? "—"} • {Rarity?.Name ?? "—"}";
    public override string? ImageUrl => Image;

    public override string Badge => Stattrak ? "StatTrak™" : Souvenir ? "Souvenir" : string.Empty;

    public override IEnumerable<LinhaDetalhe> GetDetails()
    {
        yield return new LinhaDetalhe("Arma", Weapon?.Name ?? "—");
        yield return new LinhaDetalhe("Categoria", Category?.Name ?? "—");
        yield return new LinhaDetalhe("Padrão", Pattern?.Name ?? "—");
        yield return new LinhaDetalhe("Raridade", Rarity?.Name ?? "—");
        yield return new LinhaDetalhe("Float", $"{MinFloat?.ToString("0.00") ?? "—"} a {MaxFloat?.ToString("0.00") ?? "—"}");
        yield return new LinhaDetalhe("Desgastes", Wears is { Count: > 0 } ? string.Join(", ", Wears.Select(w => w.Name)) : "—");
        yield return new LinhaDetalhe("Time", Team?.Name ?? "—");
        yield return new LinhaDetalhe("StatTrak", Stattrak ? "Sim" : "Não");
        yield return new LinhaDetalhe("Souvenir", Souvenir ? "Sim" : "Não");
    }
}
