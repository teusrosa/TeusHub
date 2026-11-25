using System.Collections.Specialized;
using GameHub.Models;
using GameHub.Services;

namespace GameHub.ViewModels;

//aqui vai abrir uma aba do jogo escolhido
public class GameCard
{
    public GameCard(DefinicaoJogo game)
    {
        Game = game;
    }

    public DefinicaoJogo Game { get; }
}

public class InicioViewModel : ModeloBaseViewModel
{
    private readonly FavoritesService _favorites;
    private readonly INavigationService _nav;

    public InicioViewModel(INavigationService nav, FavoritesService favorites)
    {
        _nav = nav;
        _favorites = favorites;
        Title = "GameHub";

        Games = DefinicoesJogos.All
            .Select(g => new GameCard(g)) // RODAPÉ
            .ToList();

        _favorites.Items.CollectionChanged += OnFavoritesChanged;
    }

    public IReadOnlyList<GameCard> Games { get; }
    public int FavoritesCount => _favorites.Items.Count;

    public Task OpenGameAsync(GameCard card) => _nav.GoToGameTabAsync(card.Game);

    public void OpenFlyout() => _nav.OpenFlyout();

    public Task OpenFavoritesAsync() => _nav.GoToFavoritesAsync();

    private void OnFavoritesChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
        OnPropertyChanged(nameof(FavoritesCount));
}
