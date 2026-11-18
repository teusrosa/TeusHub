namespace GameHub;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    private async void OnFavoritesClicked(object? sender, EventArgs e)
    {
        FlyoutIsPresented = false;
        await GoToAsync($"//{Routes.Games}/{Routes.Favorites}");
    }
}
