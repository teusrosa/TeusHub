using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using GameHub.Models;
using GameHub.Services;

namespace GameHub.ViewModels;
//ver favoritos
public class FavoritosViewModel : ModeloBaseViewModel
{
    private readonly FavoritesService _favorites;

    public FavoritosViewModel(FavoritesService favorites, INavigationService nav)
    {
        _favorites = favorites;
        Title = "Favoritos";

        OpenItemCommand = new Command<ItemJogo>(async item =>
        {
            if (item is not null) await nav.GoToItemDetailAsync(item); // PILHA
        });
        ClearCommand = new Command(_favorites.Clear);

        _favorites.Items.CollectionChanged += OnChanged;
    }

    public ObservableCollection<ItemJogo> Items => _favorites.Items;
    public bool IsEmpty => Items.Count == 0;
    public bool HasItems => Items.Count > 0;

    public ICommand OpenItemCommand { get; }
    public ICommand ClearCommand { get; }

    private void OnChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsEmpty));
        OnPropertyChanged(nameof(HasItems));
    }
}
