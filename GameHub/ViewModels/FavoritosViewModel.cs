using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GameHub.Models;
using GameHub.Services;

namespace GameHub.ViewModels;
//ver favoritos
public class FavoritosViewModel : ModeloBaseViewModel
{
    private readonly FavoritesService _favorites;
    private readonly INavigationService _nav;

    public FavoritosViewModel(FavoritesService favorites, INavigationService nav)
    {
        _favorites = favorites;
        _nav = nav;
        Title = "Favoritos";

        _favorites.Items.CollectionChanged += OnChanged;
    }

    public ObservableCollection<ItemJogo> Items => _favorites.Items;
    public bool IsEmpty => Items.Count == 0;
    public bool HasItems => Items.Count > 0;

    public Task OpenItemAsync(ItemJogo item) => _nav.GoToItemDetailAsync(item);

    public void Clear() => _favorites.Clear();

    private void OnChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsEmpty));
        OnPropertyChanged(nameof(HasItems));
    }
}
