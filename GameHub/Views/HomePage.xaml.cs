using GameHub.ViewModels;

namespace GameHub.Views;

public partial class HomePage : ContentPage
{
    public HomePage(InicioViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
