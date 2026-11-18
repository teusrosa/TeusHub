namespace GameHub.Models;


//aq classe base abstrata usada p qualquer item de qualquer jogo.
public abstract class GameItem
{
    public abstract GameDefinition Game { get; }
    public abstract string Key { get; }
    public abstract string Title { get; }
    public abstract string Subtitle { get; }
    public abstract IEnumerable<DetailRow> GetDetails();

    public virtual string Badge => string.Empty;
    public virtual string? ImageUrl => null;

    public bool HasImage => !string.IsNullOrWhiteSpace(ImageUrl);
    public bool NoImage => !HasImage;

    public string FavoriteKey => $"{Game.Id}:{Key}";
}
