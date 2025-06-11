using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using tournament_tracker.Configuration;
using tournament_tracker.Models;
namespace tournament_tracker.Views;

public partial class TournamentDashboardView : UserControl
{
    ObservableCollection<Tournament> tournaments;
    public TournamentDashboardView()
    {
        InitializeComponent();
        tournaments = new ObservableCollection<Tournament>(GlobalConfig.Connections[0].GetTournaments());
        SelectTournamentComboBox.ItemsSource = tournaments;
    }

    public void OnLoadTournamentClicked(object? sender , RoutedEventArgs e)
    {
        Debug.WriteLine("OnLoadTournamentClicked");
        if (SelectTournamentComboBox.SelectedItem!=null)
        {
            GlobalConfig.CurrentTournament = (Tournament)SelectTournamentComboBox.SelectedItem;
            var mainWindow = (MainWindow)VisualRoot;
            var tab2 = mainWindow.FindControl<TabItem>("TournamentView");
            if (mainWindow.TournamentView != null)
            {
                // 2. Methode aufrufen
                mainWindow.TournamentViewer.LoadCurrentTournament();
            }

            
        }
    }
    public void OnCreateTournamentClicked(object? sender, RoutedEventArgs e)
    {

        var mainWindow = (MainWindow)VisualRoot;
        mainWindow.TabController.SelectedIndex = 1;
    }
}