using GameHub.ViewModels;

namespace GameHub.Views;

public partial class ItemDetailPage : ContentPage
{
    public ItemDetailPage(ItensDetalhesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void OnToggleFavoriteClicked(object? sender, EventArgs e)
    {
        if (BindingContext is ItensDetalhesViewModel viewModel)
            viewModel.ToggleFavorite();
    }

    private async void OnOpenGameInfoClicked(object? sender, EventArgs e)
    {
        if (BindingContext is ItensDetalhesViewModel viewModel)
            await viewModel.OpenGameInfoAsync();
    }

    private async void OnBackClicked(object? sender, EventArgs e)
    {
        if (BindingContext is ItensDetalhesViewModel viewModel)
            await viewModel.GoBackAsync();
    }
}
