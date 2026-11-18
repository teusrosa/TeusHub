using GameHub.Models;
using GameHub.ViewModels;

namespace GameHub.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewModel;

    public FavoritesPage(FavoritesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is GameItem item)
            _viewModel.OpenItemCommand.Execute(item);

        if (sender is CollectionView list)
            list.SelectedItem = null;
    }
}
