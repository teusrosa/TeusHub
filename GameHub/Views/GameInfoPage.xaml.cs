using GameHub.ViewModels;

namespace GameHub.Views;

public partial class GameInfoPage : ContentPage
{
    public GameInfoPage(GameInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
