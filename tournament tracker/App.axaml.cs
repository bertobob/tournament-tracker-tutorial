using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using tournament_tracker.ViewModels;
using tournament_tracker.Views;
using tournament_tracker.Configuration;

namespace tournament_tracker;

public partial class App : Application
{
   
    public override void Initialize()
    {

        AvaloniaXamlLoader.Load(this);
        GlobalConfig.InitializeConnections(true, false);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
