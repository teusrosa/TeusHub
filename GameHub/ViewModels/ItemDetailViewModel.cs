using System.Collections.ObjectModel;
using System.Windows.Input;
using GameHub.Models;
using GameHub.Services;

namespace GameHub.ViewModels;

/// <summary>Tela de detalhe empilhada (PILHA). Funciona para qualquer GameItem.</summary>
public class ItemDetailViewModel : BaseViewModel, IQueryAttributable
{
    private readonly FavoritesService _favorites;
    private readonly INavigationService _nav;
    private GameItem? _item;
    private string _favoriteButtonText = "☆ Favoritar";

    public ItemDetailViewModel(FavoritesService favorites, INavigationService nav)
    {
        _favorites = favorites;
        _nav = nav;

        ToggleFavoriteCommand = new Command(ToggleFavorite);
        OpenGameInfoCommand = new Command(async () =>
        {
            if (Item is not null) await _nav.GoToGameInfoAsync(Item.Game); // empilha outra página
        });
    }

    public GameItem? Item
    {
        get => _item;
        private set => SetProperty(ref _item, value);
    }

    public ObservableCollection<DetailRow> Details { get; } = new();

    public string FavoriteButtonText
    {
        get => _favoriteButtonText;
        private set => SetProperty(ref _favoriteButtonText, value);
    }

    public string InfoButtonText => Item is null ? "🎮 Sobre o jogo" : $"🎮 Sobre {Item.Game.DisplayName} (RAWG)";

    public ICommand ToggleFavoriteCommand { get; }
    public ICommand OpenGameInfoCommand { get; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Item", out var value) && value is GameItem item)
        {
            Item = item;
            Title = item.Title;
            Details.Clear();
            foreach (var row in item.GetDetails())
                Details.Add(row);

            UpdateFavoriteText();
            OnPropertyChanged(nameof(InfoButtonText));
        }
    }

    private void ToggleFavorite()
    {
        if (Item is null) return;
        _favorites.Toggle(Item);
        UpdateFavoriteText();
    }

    private void UpdateFavoriteText() =>
        FavoriteButtonText = Item is not null && _favorites.IsFavorite(Item) ? "★ Remover dos favoritos" : "☆ Favoritar";
}
