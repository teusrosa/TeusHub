using GameHub.ViewModels;

namespace GameHub.Views;

public abstract class CatalogPage : ContentPage
{
    private readonly CatalogoViewModel _viewModel;

    protected CatalogPage(CatalogoViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = viewModel;
        Title = viewModel.Title;
        Content = new CatalogView();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.EnsureLoadedAsync();
    }
}

public class GtaPage : CatalogPage
{
    public GtaPage(GtaViewModel viewModel) : base(viewModel) { }
}

public class FortnitePage : CatalogPage
{
    public FortnitePage(FortniteViewModel viewModel) : base(viewModel) { }
}

public class Cs2Page : CatalogPage
{
    public Cs2Page(Cs2ViewModel viewModel) : base(viewModel) { }
}
