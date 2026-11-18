using GameHub.Models;

namespace GameHub.Services;

/// <summary>
/// Centraliza os 3 modos de navegação do app em um só lugar:
///  - PILHA:        GoToAsync("rota") empilha páginas (detalhe, info do jogo) e GoToAsync("..") volta.
///  - RODAPÉ:       rota absoluta //games/{aba} troca a aba do TabBar.
///  - MENU LATERAL: FlyoutIsPresented abre o menu; //home etc. trocam o item do menu.
/// </summary>
public class ShellNavigationService : INavigationService
{
    // ----- PILHA -----
    public Task GoToItemDetailAsync(GameItem item) =>
        Shell.Current.GoToAsync(Routes.ItemDetail, new ShellNavigationQueryParameters { { "Item", item } });

    public Task GoToGameInfoAsync(GameDefinition game) =>
        Shell.Current.GoToAsync(Routes.GameInfo, new ShellNavigationQueryParameters { { "Game", game } });

    public Task GoBackAsync() => Shell.Current.GoToAsync("..");

    // ----- RODAPÉ (abas) -----
    public Task GoToGameTabAsync(GameDefinition game) =>
        Shell.Current.GoToAsync($"//{Routes.Games}/{game.Route}");

    public Task GoToFavoritesAsync() =>
        Shell.Current.GoToAsync($"//{Routes.Games}/{Routes.Favorites}");

    // ----- MENU LATERAL -----
    public Task GoToHomeAsync() => Shell.Current.GoToAsync($"//{Routes.Home}");

    public void OpenFlyout() => Shell.Current.FlyoutIsPresented = true;
}
