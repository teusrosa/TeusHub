namespace GameHub.Models;
//fortnite
public class ValueInfo
{
    public string? Value { get; set; }
    public string? DisplayValue { get; set; }
}

public class IntroductionInfo
{
    public string? Text { get; set; }
}

public class CosmeticImages
{
    public string? SmallIcon { get; set; }
    public string? Icon { get; set; }
}

public class CosmeticItem : GameItem
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public ValueInfo? Type { get; set; }
    public ValueInfo? Rarity { get; set; }
    public IntroductionInfo? Introduction { get; set; }
    public CosmeticImages? Images { get; set; }

    public override GameDefinition Game => GameDefinitions.Fortnite;
    public override string Key => Id;
    public override string Title => Name;
    public override string Subtitle => $"{Type?.DisplayValue ?? "—"} • {Rarity?.DisplayValue ?? "—"}";
    public override string Badge => Rarity?.DisplayValue ?? string.Empty;
    public override string? ImageUrl => Images?.Icon ?? Images?.SmallIcon;

    public override IEnumerable<DetailRow> GetDetails()
    {
        yield return new DetailRow("ID", Id);
        yield return new DetailRow("Tipo", Type?.DisplayValue ?? "—");
        yield return new DetailRow("Raridade", Rarity?.DisplayValue ?? "—");
        yield return new DetailRow("Descrição", string.IsNullOrWhiteSpace(Description) ? "—" : Description!);
        yield return new DetailRow("Introdução", Introduction?.Text ?? "—");
    }
}
