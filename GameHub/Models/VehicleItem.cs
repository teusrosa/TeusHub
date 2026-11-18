namespace GameHub.Models;
//gta
public class VehicleItem : GameItem
{
    public string Name { get; set; } = "";
    public Dictionary<string, string?>? DisplayName { get; set; }
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
            DisplayName?.TryGetValue("English", out english);
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
