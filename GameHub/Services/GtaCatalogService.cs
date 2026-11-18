using GameHub.Models;

namespace GameHub.Services;
//info da api do gta
public class GtaCatalogService : CachedFileCatalogService<VehicleItem>
{
    public override GameDefinition Game => GameDefinitions.GtaV;

    protected override string Url =>
        "https://raw.githubusercontent.com/DurtyFree/gta-v-data-dumps/master/vehicles.json";

    protected override string CacheFileName => "gta_vehicles.json";

    protected override bool Matches(VehicleItem v, string term) =>
        Contains(v.Title, term) || Contains(v.Name, term) || Contains(v.Manufacturer, term) || Contains(v.Class, term);

    protected override IEnumerable<VehicleItem> Order(IEnumerable<VehicleItem> items) =>
        items.OrderBy(v => v.Title, StringComparer.OrdinalIgnoreCase);

    private static bool Contains(string? text, string term) =>
        text?.Contains(term, StringComparison.OrdinalIgnoreCase) == true;
}
