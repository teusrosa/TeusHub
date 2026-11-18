using GameHub.Models;

namespace GameHub.Services;

/// <summary>Contrato de qualquer catálogo de jogo (GTA, Fortnite, CS2...).</summary>
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
    // PILHA
    Task GoToItemDetailAsync(GameItem item);
    Task GoToGameInfoAsync(GameDefinition game);
    Task GoBackAsync();

    // RODAPÉ (abas)
    Task GoToGameTabAsync(GameDefinition game);
    Task GoToFavoritesAsync();

    // MENU LATERAL
    Task GoToHomeAsync();
    void OpenFlyout();
}
