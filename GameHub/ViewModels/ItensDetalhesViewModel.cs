using System.Collections.ObjectModel;
using GameHub.Models;
using GameHub.Services;

namespace GameHub.ViewModels;
//detalhes do item escolhido
public class ItensDetalhesViewModel : ModeloBaseViewModel, IQueryAttributable
{
    private readonly FavoritesService _favorites;
    private readonly INavigationService _nav;
    private ItemJogo? _item;
    private string _favoriteButtonText = "☆ Favoritar";

    public ItensDetalhesViewModel(FavoritesService favorites, INavigationService nav)
    {
        _favorites = favorites;
        _nav = nav;

    }

    public ItemJogo? Item
    {
        get => _item;
        private set => SetProperty(ref _item, value);
    }

    public ObservableCollection<LinhaDetalhe> Details { get; } = new();

    public string FavoriteButtonText
    {
        get => _favoriteButtonText;
        private set => SetProperty(ref _favoriteButtonText, value);
    }

    public string InfoButtonText => Item is null ? "🎮 Sobre o jogo" : $"🎮 Sobre {Item.Game.DisplayName} (RAWG)";

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Item", out var value) && value is ItemJogo item)
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

    public void ToggleFavorite()
    {
        if (Item is null) return;
        _favorites.Toggle(Item);
        UpdateFavoriteText();
    }

    public Task OpenGameInfoAsync() => Item is null
        ? Task.CompletedTask
        : _nav.GoToGameInfoAsync(Item.Game);

    public Task GoBackAsync() => _nav.GoBackAsync();

    private void UpdateFavoriteText() =>
        FavoriteButtonText = Item is not null && _favorites.IsFavorite(Item) ? "★ Remover dos favoritos" : "☆ Favoritar";
}
