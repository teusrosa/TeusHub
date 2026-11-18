using System.Text.Json;

namespace GameHub.Models;
//gta
public class VehicleItem : GameItem
{
    public string Name { get; set; } = "";
    public Dictionary<string, JsonElement>? DisplayName { get; set; }
    public string? Manufacturer { get; set; }
    public string? Class { get; set; }
    public string? DlcName { get; set; }
    public string? HandlingId { get; set; }

    public override GameDefinition Game => GameDefinitions.GtaV;
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

    public override IEnumerable<DetailRow> GetDetails()
    {
        yield return new DetailRow("Nome interno", Name);
        yield return new DetailRow("Fabricante", Manufacturer ?? "—");
        yield return new DetailRow("Classe", Class ?? "—");
        yield return new DetailRow("DLC", DlcName ?? "—");
        yield return new DetailRow("Handling ID", HandlingId ?? "—");
    }
}
