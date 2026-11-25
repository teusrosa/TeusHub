using GameHub.ViewModels;

namespace GameHub.Views;

public partial class HomePage : ContentPage
{
    public HomePage(InicioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnGameCardTapped(object? sender, TappedEventArgs e)
    {
        if (sender is Border border && border.BindingContext is GameCard card && BindingContext is InicioViewModel viewModel)
            await viewModel.OpenGameAsync(card);
    }

    private void OnOpenFlyoutClicked(object? sender, EventArgs e)
    {
        if (BindingContext is InicioViewModel viewModel)
            viewModel.OpenFlyout();
    }

    private async void OnOpenFavoritesClicked(object? sender, EventArgs e)
    {
        if (BindingContext is InicioViewModel viewModel)
            await viewModel.OpenFavoritesAsync();
    }
}
