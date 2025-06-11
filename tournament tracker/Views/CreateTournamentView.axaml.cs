using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using DynamicData;
using System;
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
            if (selectableTeams.Count > 0) SelectTeamComboBox.SelectedIndex = 0;
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
            if (selectablePrizes.Count > 0) PrizeListListBox.SelectedIndex = 0;
            
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
            EntryFeeTextBox.Background = Brushes.LightCoral;
        }
        else EntryFeeTextBox.Background = Brushes.White;
        if (TournamentNameTextBox.Text == null || TournamentNameTextBox.Text == "")
        {
            TournamentNameTextBox.Background = Brushes.LightCoral;
            isValid = false;
        }
        else TournamentNameTextBox.Background = Brushes.White;
        if (teams.Count>1 && isValid)
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
            CreateMatchups(id);
            CreateTournamentStatus.Text = "Tournament created !";

        }
        else CreateTournamentStatus.Text = "Tournament not created !";


    }
    
    public void AddTeam(Team team)
    {
        selectableTeams.Add(team);
    }

    private void CreateMatchups(int tournamentId)
    {
        List<Team> teamCopy = new List<Team>(teams);
        Team teamA, teamB;
        Random rng = new Random();
        int matchupId;
        int currentRound=2;
        int temp;
        int exponent = 1;        
        while (exponent * 2 <= teamCopy.Count) exponent *= 2; // get the amount of max number if teams in round 1
        exponent *= 2;
        int remainingMatchesInCurrentRound;
        Debug.WriteLine("exponent = " + exponent);
        temp =  (2* teamCopy.Count -exponent) / 2; // number of players in 1rst round
        for (int i=0; i<temp; i++)    // rest starts round 2
        {
            Debug.WriteLine("in 1rst loop");
            teamA = teamCopy[rng.Next(teamCopy.Count - 1)];
            teamCopy.Remove(teamA);
            teamB = teamCopy[rng.Next(teamCopy.Count - 1)];
            teamCopy.Remove(teamB);
            matchupId=GlobalConfig.Connections[0].AddMatchup(tournamentId, teamA.Id, teamB.Id,1);
         
        }
        exponent /= 2;    // remaining matches 
        remainingMatchesInCurrentRound = exponent/2;
        while (teamCopy.Count > 0)
        {
            teamA = teamCopy[rng.Next(teamCopy.Count - 1)];
            teamCopy.Remove(teamA);
            if (teamCopy.Count > 0)
            {
                teamB = teamCopy[rng.Next(teamCopy.Count - 1)];
                teamCopy.Remove(teamB);
            }
            else teamB = null;
            if (teamB == null)
            {
                matchupId = GlobalConfig.Connections[0].AddMatchup(tournamentId, teamA.Id, null,2);
                
            }
            else
            {
                matchupId = GlobalConfig.Connections[0].AddMatchup(tournamentId, teamA.Id, teamB.Id,2);
                
            }
            remainingMatchesInCurrentRound--;
        }
        
        while (exponent>1)   // exponent=1 means final match
        {
            for (int i = 0; i < remainingMatchesInCurrentRound; i++)
            {
                matchupId = GlobalConfig.Connections[0].AddMatchup(tournamentId, null, null,currentRound);
                
            }
            currentRound++;
            exponent /= 2;
            remainingMatchesInCurrentRound = exponent/2;
        }


    }
}