using GameHub.ViewModels;

namespace GameHub.Views;

/// <summary>
/// Página base das 3 abas do rodapé (herança): monta o CatalogView e carrega os dados.
/// GtaPage, FortnitePage e Cs2Page só passam o ViewModel do seu jogo.
/// </summary>
public abstract class CatalogPage : ContentPage
{
    private readonly CatalogViewModel _viewModel;

    protected CatalogPage(CatalogViewModel viewModel)
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
