namespace GameHub.Models;
//favoritar item
public class RegistroFavorito
{
    public string GameId { get; set; } = "";
    public string Key { get; set; } = "";
    public string Title { get; set; } = "";
    public string Subtitle { get; set; } = "";
    public string Badge { get; set; } = "";
    public string? ImageUrl { get; set; }
    public List<LinhaDetalhe> Details { get; set; } = new();
}

public class ItemFavorito : ItemJogo
{
    private readonly RegistroFavorito _record;

    public ItemFavorito(RegistroFavorito record) => _record = record;

    public ItemFavorito(ItemJogo source) : this(new RegistroFavorito
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

    public RegistroFavorito ToRecord() => _record;

    public override DefinicaoJogo Game => DefinicoesJogos.ById(_record.GameId);
    public override string Key => _record.Key;
    public override string Title => _record.Title;
    public override string Subtitle => _record.Subtitle;
    public override string Badge => _record.Badge;
    public override string? ImageUrl => _record.ImageUrl;
    public override IEnumerable<LinhaDetalhe> GetDetails() => _record.Details;
}
