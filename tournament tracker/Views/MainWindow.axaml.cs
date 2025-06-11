using Avalonia.Controls;

namespace tournament_tracker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.WindowState = WindowState.Maximized;
       
    }
    private void MainTab_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (TabController.SelectedIndex == 1) // Index des Tournament-Tabs
            TournamentViewer.LoadCurrentTournament();
    }
}
