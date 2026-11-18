namespace GameHub;

public static class AppConfig
{
    public static readonly string RawgApiKey = "ba81736e6a7b43e1b9be9b1e4d3d5be0";

    public static bool HasRawgKey =>
        !string.IsNullOrWhiteSpace(RawgApiKey) && RawgApiKey != "ba81736e6a7b43e1b9be9b1e4d3d5be0";
}

public static class Routes
{
    public const string Home = "home";
    public const string Games = "games";
    public const string Favorites = "favorites";

    public const string ItemDetail = "itemdetail";
    public const string GameInfo = "gameinfo";
}
