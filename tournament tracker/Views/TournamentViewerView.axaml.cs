using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Reactive.Linq;
using tournament_tracker.Configuration;
using tournament_tracker.Models;

namespace tournament_tracker.Views;

public partial class TournamentViewerView : UserControl, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private ObservableCollection<string> rounds = new();
    private ObservableCollection<Matchup> matches = new();
    public TournamentViewerView()
    {
        InitializeComponent();
        DataContext = this;                         
    }

    public void LoadCurrentTournament()
    {
        rounds.Clear();
        Tournament tournament = GlobalConfig.CurrentTournament;        
        TName.Text = tournament.TournamentName;
        tournament.GetMatches();
        int roundNumbers=0;
        foreach (var matchup in tournament.Matches)
        {
            Debug.WriteLine(matchup.Id);
            if (matchup.RoundNumber > roundNumbers) roundNumbers = matchup.RoundNumber;
        }
        Debug.WriteLine("roundnumbers : " + roundNumbers);
        for (int i=1;i<roundNumbers+1 ; i++)
        {
            rounds.Add("Round " + i.ToString());
            Debug.WriteLine("adding round : "+i.ToString());
        }
        
        RoundSelectComboBox.ItemsSource = rounds;
      
        
    }
    public void RoundSelectComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Tournament tournament = GlobalConfig.CurrentTournament;
        int roundNumber = RoundSelectComboBox.SelectedIndex+1;
        matches.Clear();
        foreach (var match in tournament.Matches)
        {
            if (match.RoundNumber == roundNumber)
            {
                if ((bool)UnplayedOnly.IsChecked)
                {
                    if (match.WinnerId==null) matches.Add(match);
                }
                else matches.Add(match);
            }                
        }
        MatchesListBox.ItemsSource = matches;

    }

    private void UnplayedOnly_CheckedChanged(object? sender, RoutedEventArgs e)
    {
        RoundSelectComboBox_SelectionChanged(null, null);
    }

    public void MatchesListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Team1Score.Background = Brushes.White;
        Team2Score.Background = Brushes.White;
        if (MatchesListBox.SelectedItem != null)
        {
            Matchup selectedMatch = (Matchup)MatchesListBox.SelectedItem;

            Team1Score.IsEnabled = (selectedMatch.WinnerId == null);
            Team2Score.IsEnabled = (selectedMatch.WinnerId == null);
            ScoreButton.IsEnabled = (selectedMatch.WinnerId == null);            

            if (selectedMatch.TeamAId != null)
            {
                Team1Name.Text = GlobalConfig.Connections[0].GetTeamNameById((int)selectedMatch.TeamAId);
            }
            else Team1Name.Text = "Name not found";
            if (selectedMatch.TeamBId != null)
                Team2Name.Text = GlobalConfig.Connections[0].GetTeamNameById((int)selectedMatch.TeamBId);
            else Team2Name.Text = "Name not found";            
            Team1Score.Text = (selectedMatch.TeamAScore != null) ? selectedMatch.TeamAScore.ToString() : "";
            Team2Score.Text = (selectedMatch.TeamBScore != null) ? selectedMatch.TeamBScore.ToString() : "";

        }
    }

    public void OnScoreClicked(object? sender, RoutedEventArgs e)
    {
        bool isValid = true;
        isValid=int.TryParse(Team1Score.Text,out int team1Score);
        if (!isValid) Team1Score.Background = Brushes.LightCoral;
        else Team1Score.Background = Brushes.White;
        if (!int.TryParse(Team2Score.Text, out int team2Score))
        {
            Team2Score.Background = Brushes.LightCoral;
            isValid = false;
        }            
        else Team2Score.Background = Brushes.White;
        if (MatchesListBox.SelectedItem != null)
        {
            if (((Matchup)MatchesListBox.SelectedItem).TeamBId == null
                || ((Matchup)MatchesListBox.SelectedItem).TeamAId == null) isValid = false;
        }
        
        
        if (isValid && MatchesListBox.SelectedItem!=null)
        {
            ProcessMatch(team1Score, team2Score);
            TournamentViewerStatus.Text = "Score saved !";
        }
        else TournamentViewerStatus.Text = "Score couldnt be saved !";
    }

    public void ProcessMatch(int team1Score,int team2Score)
    {        
        Matchup match = (Matchup)MatchesListBox.SelectedItem;
        match.TeamAScore = team1Score;
        match.TeamBScore = team2Score;
        int winnerId = (team1Score > team2Score) ? (int)match.TeamAId : (int)match.TeamBId;
        GlobalConfig.Connections[0].UpdateScore(match.Id,  team1Score, team2Score, winnerId); 
        if (match.RoundNumber<RoundSelectComboBox.ItemCount)        // noch nicht das finale
        {
            foreach (Matchup nextMatch in GlobalConfig.CurrentTournament.Matches)
            {
                if (nextMatch.RoundNumber == match.RoundNumber + 1)
                {
                    if (nextMatch.TeamAId == null)
                    {
                        nextMatch.TeamAId = winnerId;
                        GlobalConfig.Connections[0].AddTeamToNextRound(nextMatch.Id, nextMatch.TeamAId, null);
                        break;
                    }
                    if (nextMatch.TeamBId == null)
                    {
                        nextMatch.TeamBId = winnerId;
                        GlobalConfig.Connections[0].AddTeamToNextRound(nextMatch.Id, null , nextMatch.TeamBId);
                        break;
                    }
                }
            }
            
        }


        
    }
    protected void OnPropertyChanged(string n) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}
