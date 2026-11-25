using GameHub.Models;

namespace GameHub.Services;

public class ShellNavigationService : INavigationService
{
    // pilha
    public Task GoToItemDetailAsync(ItemJogo item) =>
        Shell.Current.GoToAsync(Routes.ItemDetail, new ShellNavigationQueryParameters { { "Item", item } });

    public Task GoToGameInfoAsync(DefinicaoJogo game) =>
        Shell.Current.GoToAsync(Routes.GameInfo, new ShellNavigationQueryParameters { { "Game", game } });

    public Task GoBackAsync() => Shell.Current.GoToAsync("..");

    // rodape
    public Task GoToGameTabAsync(DefinicaoJogo game) =>
        Shell.Current.GoToAsync($"//{Routes.Games}/{game.Route}");

    public Task GoToFavoritesAsync() =>
        Shell.Current.GoToAsync($"//{Routes.Games}/{Routes.Favorites}");

    // menu l
    public Task GoToHomeAsync() => Shell.Current.GoToAsync($"//{Routes.Home}");

    public void OpenFlyout() => Shell.Current.FlyoutIsPresented = true;
}
