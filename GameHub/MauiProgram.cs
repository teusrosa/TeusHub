using GameHub.Services;
using GameHub.ViewModels;
using GameHub.Views;
using Microsoft.Extensions.Logging;

namespace GameHub;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        // Serviços (APIs e navegação)
        builder.Services.AddSingleton<INavigationService, ShellNavigationService>();
        builder.Services.AddSingleton<FavoritesService>();
        builder.Services.AddSingleton<IGameInfoService, RawgGameInfoService>();
        builder.Services.AddSingleton<GtaCatalogService>();
        builder.Services.AddSingleton<FortniteCatalogService>();
        builder.Services.AddSingleton<Cs2CatalogService>();

        // ViewModels
        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<GtaViewModel>();
        builder.Services.AddTransient<FortniteViewModel>();
        builder.Services.AddTransient<Cs2ViewModel>();
        builder.Services.AddTransient<FavoritesViewModel>();
        builder.Services.AddTransient<ItemDetailViewModel>();
        builder.Services.AddTransient<GameInfoViewModel>();

        // Páginas
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<GtaPage>();
        builder.Services.AddTransient<FortnitePage>();
        builder.Services.AddTransient<Cs2Page>();
        builder.Services.AddTransient<FavoritesPage>();
        builder.Services.AddTransient<AboutPage>();
        builder.Services.AddTransient<ItemDetailPage>();
        builder.Services.AddTransient<GameInfoPage>();

        // Rotas da PILHA (não aparecem no menu nem no rodapé)
        Routing.RegisterRoute(Routes.ItemDetail, typeof(ItemDetailPage));
        Routing.RegisterRoute(Routes.GameInfo, typeof(GameInfoPage));

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}
