using GameHub.ViewModels;

namespace GameHub.Views;

public partial class GameInfoPage : ContentPage
{
    public GameInfoPage(InformacoesJogoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnOpenCatalogClicked(object? sender, EventArgs e)
    {
        if (BindingContext is InformacoesJogoViewModel viewModel)
            await viewModel.OpenCatalogAsync();
    }

    private async void OnOpenHomeClicked(object? sender, EventArgs e)
    {
        if (BindingContext is InformacoesJogoViewModel viewModel)
            await viewModel.OpenHomeAsync();
    }

    private async void OnBackClicked(object? sender, EventArgs e)
    {
        if (BindingContext is InformacoesJogoViewModel viewModel)
            await viewModel.GoBackAsync();
    }
}
