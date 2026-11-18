using System.Text.Json.Serialization;

namespace GameHub.Models;

public class NamedRef
{
    public string? Id { get; set; }
    public string? Name { get; set; }
}

public class RarityRef : NamedRef
{
    public string? Color { get; set; }
}

/// <summary>Skin de arma do CS2 (skins.json da CSGO-API do ByMykel).</summary>
public class SkinItem : GameItem
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public NamedRef? Weapon { get; set; }
    public NamedRef? Category { get; set; }
    public NamedRef? Pattern { get; set; }
    public RarityRef? Rarity { get; set; }
    public NamedRef? Team { get; set; }
    public List<NamedRef>? Wears { get; set; }
    public bool Stattrak { get; set; }
    public bool Souvenir { get; set; }
    public string? Image { get; set; }

    [JsonPropertyName("min_float")] public double? MinFloat { get; set; }
    [JsonPropertyName("max_float")] public double? MaxFloat { get; set; }

    public override GameDefinition Game => GameDefinitions.Cs2;
    public override string Key => Id;
    public override string Title => Name;
    public override string Subtitle => $"{Category?.Name ?? "—"} • {Rarity?.Name ?? "—"}";
    public override string? ImageUrl => Image;

    public override string Badge => Stattrak ? "StatTrak™" : Souvenir ? "Souvenir" : string.Empty;

    public override IEnumerable<DetailRow> GetDetails()
    {
        yield return new DetailRow("Arma", Weapon?.Name ?? "—");
        yield return new DetailRow("Categoria", Category?.Name ?? "—");
        yield return new DetailRow("Padrão", Pattern?.Name ?? "—");
        yield return new DetailRow("Raridade", Rarity?.Name ?? "—");
        yield return new DetailRow("Float", $"{MinFloat?.ToString("0.00") ?? "—"} a {MaxFloat?.ToString("0.00") ?? "—"}");
        yield return new DetailRow("Desgastes", Wears is { Count: > 0 } ? string.Join(", ", Wears.Select(w => w.Name)) : "—");
        yield return new DetailRow("Time", Team?.Name ?? "—");
        yield return new DetailRow("StatTrak", Stattrak ? "Sim" : "Não");
        yield return new DetailRow("Souvenir", Souvenir ? "Sim" : "Não");
    }
}
