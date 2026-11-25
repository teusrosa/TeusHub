using GameHub.Models;
using GameHub.ViewModels;

namespace GameHub.Views;

public partial class CatalogView : ContentView
{
    public CatalogView()
    {
        InitializeComponent();
    }

    private async void OnBackClicked(object? sender, EventArgs e)
    {
        if (BindingContext is CatalogoViewModel viewModel)
            await viewModel.GoBackAsync();
    }

    private async void OnSearchButtonPressed(object? sender, EventArgs e)
    {
        if (BindingContext is CatalogoViewModel viewModel)
            await viewModel.LoadAsync();
    }

    private async void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is ItemJogo item && BindingContext is CatalogoViewModel vm)
            await vm.OpenItemAsync(item);

        if (sender is CollectionView list)
            list.SelectedItem = null; // permite tocar no mesmo item de novo
    }
}
