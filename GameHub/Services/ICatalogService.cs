using GameHub.Models;

namespace GameHub.Services;

public interface ICatalogService
{
    DefinicaoJogo Game { get; }
    Task<IReadOnlyList<ItemJogo>> GetItemsAsync(string? search, CancellationToken ct = default);
}

public interface IGameInfoService
{
    Task<InformacoesJogo?> GetAsync(DefinicaoJogo game, CancellationToken ct = default);
}

public interface INavigationService
{
    Task GoToItemDetailAsync(ItemJogo item);
    Task GoToGameInfoAsync(DefinicaoJogo game);
    Task GoBackAsync();

    Task GoToGameTabAsync(DefinicaoJogo game);
    Task GoToFavoritesAsync();

    Task GoToHomeAsync();
    void OpenFlyout();
}
