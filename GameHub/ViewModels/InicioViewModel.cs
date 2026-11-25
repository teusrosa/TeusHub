using System.Collections.Specialized;
using System.Windows.Input;
using GameHub.Models;
using GameHub.Services;

namespace GameHub.ViewModels;

//aqui vai abrir uma aba do jogo escolhido
public class GameCard
{
    public GameCard(DefinicaoJogo game, ICommand openCommand)
    {
        Game = game;
        OpenCommand = openCommand;
    }

    public DefinicaoJogo Game { get; }
    public ICommand OpenCommand { get; }
}

public class InicioViewModel : ModeloBaseViewModel
{
    private readonly FavoritesService _favorites;

    public InicioViewModel(INavigationService nav, FavoritesService favorites)
    {
        _favorites = favorites;
        Title = "GameHub";

        Games = DefinicoesJogos.All
            .Select(g => new GameCard(g, new Command(async () => await nav.GoToGameTabAsync(g)))) // RODAPÉ
            .ToList();

        OpenFlyoutCommand = new Command(nav.OpenFlyout);                                           // MENU LATERAL
        OpenFavoritesCommand = new Command(async () => await nav.GoToFavoritesAsync());            // RODAPÉ

        _favorites.Items.CollectionChanged += OnFavoritesChanged;
    }

    public IReadOnlyList<GameCard> Games { get; }
    public ICommand OpenFlyoutCommand { get; }
    public ICommand OpenFavoritesCommand { get; }
    public int FavoritesCount => _favorites.Items.Count;

    private void OnFavoritesChanged(object? sender, NotifyCollectionChangedEventArgs e) =>
        OnPropertyChanged(nameof(FavoritesCount));
}
