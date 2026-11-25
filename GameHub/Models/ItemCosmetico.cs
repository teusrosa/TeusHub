namespace GameHub.Models;
//fortnite
public class InformacaoValor
{
    public string? Value { get; set; }
    public string? DisplayValue { get; set; }
}

public class InformacaoIntroducao
{
    public string? Text { get; set; }
}

public class ImagensCosmetico
{
    public string? SmallIcon { get; set; }
    public string? Icon { get; set; }
}

public class ItemCosmetico : ItemJogo
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public InformacaoValor? Type { get; set; }
    public InformacaoValor? Rarity { get; set; }
    public InformacaoIntroducao? Introduction { get; set; }
    public ImagensCosmetico? Images { get; set; }

    public override DefinicaoJogo Game => DefinicoesJogos.Fortnite;
    public override string Key => Id;
    public override string Title => Name;
    public override string Subtitle => $"{Type?.DisplayValue ?? "—"} • {Rarity?.DisplayValue ?? "—"}";
    public override string Badge => Rarity?.DisplayValue ?? string.Empty;
    public override string? ImageUrl => Images?.Icon ?? Images?.SmallIcon;

    public override IEnumerable<LinhaDetalhe> GetDetails()
    {
        yield return new LinhaDetalhe("ID", Id);
        yield return new LinhaDetalhe("Tipo", Type?.DisplayValue ?? "—");
        yield return new LinhaDetalhe("Raridade", Rarity?.DisplayValue ?? "—");
        yield return new LinhaDetalhe("Descrição", string.IsNullOrWhiteSpace(Description) ? "—" : Description!);
        yield return new LinhaDetalhe("Introdução", Introduction?.Text ?? "—");
    }
}
