using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using DynamicData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using tournament_tracker.Configuration;
using tournament_tracker.Models;

namespace tournament_tracker.Views;

public partial class CreateTournamentView : UserControl
{
    /// <summary>
    /// holds the teams currently selected for tournament
    /// </summary>
    ObservableCollection <Team> teams;
    /// <summary>
    /// hold the prizes currently selected for tournament
    /// </summary>
    ObservableCollection<Prize> prizes;
    /// <summary>
    /// holds the selectable teams not yet selected
    /// </summary>
    ObservableCollection<Team> selectableTeams;
    /// <summary>
    /// holds the selectable prizes not yet selected
    /// </summary>
    ObservableCollection<Prize> selectablePrizes;
    public CreateTournamentView()
    {
        InitializeComponent();
        teams = new();
        prizes = new();
        selectablePrizes = new();
        selectableTeams = new();

        foreach (var team in GlobalConfig.Teams)
        {
            selectableTeams.Add(team);
        }
        foreach (var prize in GlobalConfig.Prizes)
        {
            Debug.WriteLine(prize.Id);
            selectablePrizes.Add(prize);
        }

        SelectTeamComboBox.ItemsSource = selectableTeams;
        TournamentMemberListBox.ItemsSource = teams;
        PrizeListListBox.ItemsSource = selectablePrizes;
        TournamentPrizesListBox.ItemsSource = prizes;

    }
    public void OnAddTeamClicked(object? sender, RoutedEventArgs e)
    {
        if (SelectTeamComboBox.SelectedItem!=null)
        {
            teams.Add((Team)SelectTeamComboBox.SelectedItem);
            selectableTeams.Remove((Team)SelectTeamComboBox.SelectedItem);
        }
    }

    public void OnRemoveTeamClicked(object? sender,RoutedEventArgs e)
    {
        if (TournamentMemberListBox.SelectedItem!=null)
        {
            selectableTeams.Add((Team)TournamentMemberListBox.SelectedItem);
            teams.Remove((Team)TournamentMemberListBox.SelectedItem);
        }
    }
    public void OnCreateNewClicked(object? sender, RoutedEventArgs e)
    {
        var mainWindow = (MainWindow)VisualRoot;
        mainWindow.TabController.SelectedIndex = 2;
    }

    public void OnCreatePrizeClicked(object? sender, RoutedEventArgs e)
    {
        var mainWindow = (MainWindow)VisualRoot;
        mainWindow.TabController.SelectedIndex = 3;
    }

    public void OnAddPrizeClicked(object? sender, RoutedEventArgs e)
    {
        if(PrizeListListBox.SelectedItem!=null)
        {
            prizes.Add((Prize)PrizeListListBox.SelectedItem);
            selectablePrizes.Remove((Prize)PrizeListListBox.SelectedItem);
            
        }
    }

    public void OnRemovePrizeClicked(object? sender, RoutedEventArgs e)
    {
        if(TournamentPrizesListBox.SelectedItem!=null)
        {
            selectablePrizes.Add((Prize)TournamentPrizesListBox.SelectedItem);
            prizes.Remove((Prize)TournamentPrizesListBox.SelectedItem);
        }
    }
    public void OnCreateTournamentClicked(object? sender, RoutedEventArgs e)
    {
        bool isValid = decimal.TryParse(EntryFeeTextBox.Text, out decimal fee);
        if (!isValid || fee<0)
        {
            Debug.WriteLine($"{fee} kein gueltiger wert");
            EntryFeeTextBox.Background = Brushes.LightCoral;
        }
        else EntryFeeTextBox.Background = Brushes.White;
        if (TournamentNameTextBox.Text == null || TournamentNameTextBox.Text == "")
        {
            TournamentNameTextBox.Background = Brushes.LightCoral;
            isValid = false;
        }
        else TournamentNameTextBox.Background = Brushes.White;
        if (teams.Count>0 && isValid)
        {
            int id=GlobalConfig.Connections[0].AddTournament(TournamentNameTextBox.Text,fee);
            foreach (var team in teams)
            {
                Debug.WriteLine(team.Id);
                GlobalConfig.Connections[0].AddTournamentEntries(id, team.Id);
            }
            if (prizes.Count>0)
            {
                foreach( var prize in prizes)
                {
                    GlobalConfig.Connections[0].AddTournamentPrize(id, prize.Id);
                }
            }

        }
        

    }
}