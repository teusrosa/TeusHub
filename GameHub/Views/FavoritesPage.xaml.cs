using GameHub.Models;
using GameHub.ViewModels;

namespace GameHub.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritosViewModel _viewModel;

    public FavoritesPage(FavoritosViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ItemJogo item)
            _viewModel.OpenItemCommand.Execute(item);

        if (sender is CollectionView list)
            list.SelectedItem = null;
    }
}
