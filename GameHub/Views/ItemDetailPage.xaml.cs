using GameHub.ViewModels;

namespace GameHub.Views;

public partial class ItemDetailPage : ContentPage
{
    public ItemDetailPage(ItensDetalhesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
