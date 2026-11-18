namespace GameHub.Models;
//favoritar item
public class FavoriteRecord
{
    public string GameId { get; set; } = "";
    public string Key { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string Badge { get; set; } = "";
    public string? ImageUrl { get; set; }
    public List<DetailRow> Details { get; set; } = new();
}

public class FavoriteItem : GameItem
{
    private readonly FavoriteRecord _record;

    public FavoriteItem(FavoriteRecord record) => _record = record;

    public FavoriteItem(GameItem source) : this(new FavoriteRecord
    {
        GameId = source.Game.Id,
        Key = source.Key,
        Title = source.Title,
        Subtitle = source.Subtitle,
        Badge = source.Badge,
        ImageUrl = source.ImageUrl,
        Details = source.GetDetails().ToList()
    })
    {
    }

    public FavoriteRecord ToRecord() => _record;

    public override GameDefinition Game => GameDefinitions.ById(_record.GameId);
    public override string Key => _record.Key;
    public override string Title => _record.Title;
    public override string Subtitle => _record.Subtitle;
    public override string Badge => _record.Badge;
    public override string? ImageUrl => _record.ImageUrl;
    public override IEnumerable<DetailRow> GetDetails() => _record.Details;
}
