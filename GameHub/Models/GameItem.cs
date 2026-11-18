namespace GameHub.Models;

/// <summary>
/// Classe base abstrata de qualquer item de qualquer jogo (veículo, skin, cosmético...).
/// As telas só conhecem GameItem: o que muda em cada jogo é resolvido por polimorfismo.
/// </summary>
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

    /// <summary>Chave única entre jogos (usada nos favoritos).</summary>
    public string FavoriteKey => $"{Game.Id}:{Key}";
}
