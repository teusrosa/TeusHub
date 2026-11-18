using GameHub.Models;
using GameHub.ViewModels;

namespace GameHub.Views;

public partial class CatalogView : ContentView
{
    public CatalogView()
    {
        InitializeComponent();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is GameItem item && BindingContext is CatalogViewModel vm)
            vm.OpenItemCommand.Execute(item);

        if (sender is CollectionView list)
            list.SelectedItem = null; // permite tocar no mesmo item de novo
    }
}
