using Microsoft.UI.Xaml;

namespace GameHub.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => GameHub.MauiProgram.CreateMauiApp();
}
