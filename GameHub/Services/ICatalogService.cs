using GameHub.Models;

namespace GameHub.Services;

public interface ICatalogService
{
    GameDefinition Game { get; }
    Task<IReadOnlyList<GameItem>> GetItemsAsync(string? search, CancellationToken ct = default);
}

public interface IGameInfoService
{
    Task<GameInfo?> GetAsync(GameDefinition game, CancellationToken ct = default);
}

public interface INavigationService
{
    Task GoToItemDetailAsync(GameItem item);
    Task GoToGameInfoAsync(GameDefinition game);
    Task GoBackAsync();

    Task GoToGameTabAsync(GameDefinition game);
    Task GoToFavoritesAsync();

    Task GoToHomeAsync();
    void OpenFlyout();
}
