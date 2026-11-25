using GameHub.ViewModels;

namespace GameHub.Views;

public partial class GameInfoPage : ContentPage
{
    public GameInfoPage(InformacoesJogoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
