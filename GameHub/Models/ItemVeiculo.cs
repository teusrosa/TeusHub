using System.Text.Json;

namespace GameHub.Models;
//gta
public class ItemVeiculo : ItemJogo
{
    public string Name { get; set; } = "";
    public Dictionary<string, JsonElement>? DisplayName { get; set; }
    public string? Manufacturer { get; set; }
    public string? Class { get; set; }
    public string? DlcName { get; set; }
    public string? HandlingId { get; set; }

    public override DefinicaoJogo Game => DefinicoesJogos.GtaV;
    public override string Key => Name;

    public override string Title
    {
        get
        {
            string? english = null;
            if (DisplayName?.TryGetValue("English", out var value) == true && value.ValueKind == JsonValueKind.String)
                english = value.GetString();
            return string.IsNullOrWhiteSpace(english) || english == "NULL" ? Name : english;
        }
    }

    public override string Subtitle => $"{Manufacturer ?? "—"} • {Class ?? "—"}";
    public override string Badge => Class ?? string.Empty;

    public override IEnumerable<LinhaDetalhe> GetDetails()
    {
        yield return new LinhaDetalhe("Nome interno", Name);
        yield return new LinhaDetalhe("Fabricante", Manufacturer ?? "—");
        yield return new LinhaDetalhe("Classe", Class ?? "—");
        yield return new LinhaDetalhe("DLC", DlcName ?? "—");
        yield return new LinhaDetalhe("Handling ID", HandlingId ?? "—");
    }
}
