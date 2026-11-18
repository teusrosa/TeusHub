using System.Collections.ObjectModel;
using System.Windows.Input;
using GameHub.Models;
using GameHub.Services;

namespace GameHub.ViewModels;

//parte de visualização do catalogo de cada jogo
public abstract class CatalogViewModel : BaseViewModel
{
    private readonly ICatalogService _service;
    private readonly INavigationService _nav;
    private bool _loaded;
    private string _searchText = "";
    private ObservableCollection<GameItem> _items = new();

    protected CatalogViewModel(ICatalogService service, INavigationService nav)
    {
        _service = service;
        _nav = nav;
        Title = service.Game.DisplayName;

        SearchCommand = new Command(async () => await LoadAsync());
        OpenItemCommand = new Command<GameItem>(async item =>
        {
            if (item is not null) await _nav.GoToItemDetailAsync(item); // PILHA
        });
    }

    public GameDefinition Game => _service.Game;
    public virtual string SearchPlaceholder => $"Buscar em {Game.DisplayName}...";

    public ObservableCollection<GameItem> Items
    {
        get => _items;
        private set => SetProperty(ref _items, value);
    }

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ICommand SearchCommand { get; }
    public ICommand OpenItemCommand { get; }

    public async Task EnsureLoadedAsync()
    {
        if (!_loaded) await LoadAsync();
    }

    public async Task LoadAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            ErrorMessage = null;
            var result = await _service.GetItemsAsync(SearchText);
            Items = new ObservableCollection<GameItem>(result);
            _loaded = true;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Não foi possível carregar os dados: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}

public class GtaViewModel : CatalogViewModel
{
    public GtaViewModel(GtaCatalogService service, INavigationService nav) : base(service, nav) { }
    public override string SearchPlaceholder => "Buscar veículo, fabricante ou classe...";
}

public class FortniteViewModel : CatalogViewModel
{
    public FortniteViewModel(FortniteCatalogService service, INavigationService nav) : base(service, nav) { }
    public override string SearchPlaceholder => "Buscar skin ou cosmético (ex.: ninja)...";
}

public class Cs2ViewModel : CatalogViewModel
{
    public Cs2ViewModel(Cs2CatalogService service, INavigationService nav) : base(service, nav) { }
    public override string SearchPlaceholder => "Buscar arma ou skin (ex.: AK-47)...";
}
