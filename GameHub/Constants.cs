namespace GameHub;

public static class AppConfig
{
    /// <summary>
    /// Chave GRATUITA da API RAWG (a API que relaciona GTA V, Fortnite e CS2).
    /// Crie a sua em https://rawg.io/apidocs e cole aqui.
    /// </summary>
    public static readonly string RawgApiKey = "COLE_SUA_CHAVE_AQUI";

    public static bool HasRawgKey =>
        !string.IsNullOrWhiteSpace(RawgApiKey) && RawgApiKey != "COLE_SUA_CHAVE_AQUI";
}

/// <summary>Rotas do Shell (menu lateral, rodapé e pilha).</summary>
public static class Routes
{
    // Menu lateral (FlyoutItems)
    public const string Home = "home";
    public const string Games = "games";
    public const string About = "about";

    // Rodapé (abas dentro de "games")
    public const string Favorites = "favorites";

    // Pilha (páginas empilhadas com GoToAsync)
    public const string ItemDetail = "itemdetail";
    public const string GameInfo = "gameinfo";
}
