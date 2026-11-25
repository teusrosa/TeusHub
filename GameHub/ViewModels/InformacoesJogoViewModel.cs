using System.Windows.Input;
using GameHub.Models;
using GameHub.Services;

namespace GameHub.ViewModels;

//aba do jogo escolhidp
public class InformacoesJogoViewModel : ModeloBaseViewModel, IQueryAttributable
{
    private readonly IGameInfoService _service;
    private readonly INavigationService _nav;
    private DefinicaoJogo? _game;
    private InformacoesJogo? _info;

    public InformacoesJogoViewModel(IGameInfoService service, INavigationService nav)
    {
        _service = service;
        _nav = nav;

        OpenCatalogCommand = new Command(async () =>
        {
            if (Game is not null) await _nav.GoToGameTabAsync(Game); // troca a aba do RODAPÉ
        });
        BackCommand = new Command(async () => await _nav.GoBackAsync());
        OpenHomeCommand = new Command(async () => await _nav.GoToHomeAsync()); // MENU LATERAL
    }

    public DefinicaoJogo? Game
    {
        get => _game;
        private set => SetProperty(ref _game, value);
    }

    public InformacoesJogo? Info
    {
        get => _info;
        private set
        {
            if (SetProperty(ref _info, value))
                OnPropertyChanged(nameof(HasInfo));
        }
    }

    public bool HasInfo => Info is not null;
    public string CatalogButtonText => Game is null ? "Ver catálogo" : $"{Game.Emoji} Ver catálogo de {Game.DisplayName}";

    public ICommand OpenCatalogCommand { get; }
    public ICommand BackCommand { get; }
    public ICommand OpenHomeCommand { get; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Game", out var value) && value is DefinicaoJogo game)
        {
            Game = game;
            Title = game.DisplayName;
            OnPropertyChanged(nameof(CatalogButtonText));
            _ = LoadAsync();
        }
    }

    private async Task LoadAsync()
    {
        if (Game is null) return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;
            Info = await _service.GetAsync(Game);
            if (Info is null)
                ErrorMessage = "Jogo não encontrado na RAWG.";
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
