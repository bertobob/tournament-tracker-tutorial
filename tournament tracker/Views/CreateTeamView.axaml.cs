using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using tournament_tracker.Configuration;
using tournament_tracker.Models;

namespace tournament_tracker.Views;

public partial class CreateTeamView : UserControl
{
    /// <summary>
    /// holds the currently Person Objekts added to the team
    /// </summary>
    ObservableCollection<Person> TeamMembers;
    /// <summary>
    /// holds the Persons that can be selected in Combobox
    /// </summary>
    ObservableCollection<Person> selectedTeamMembers;

    public CreateTeamView()
    {
        InitializeComponent();
        selectedTeamMembers=new();
        TeamMembers = new();
        foreach (var person in GlobalConfig.Persons)
        {
            selectedTeamMembers.Add(person);
        }
        
        SelectMemberComboBox.ItemsSource = selectedTeamMembers;
        TeamMemberListBox.ItemsSource = TeamMembers;
        
    }

    private void OnAddMemberClicked(object? sender, RoutedEventArgs e)
    {
        if (SelectMemberComboBox.SelectedItem !=null)
        {
            this.TeamMembers.Add((Person) SelectMemberComboBox.SelectedItem);
            this.selectedTeamMembers.Remove((Person)SelectMemberComboBox.SelectedItem);
            if (selectedTeamMembers.Count > 0)
            {
                SelectMemberComboBox.SelectedItem = selectedTeamMembers[0];
            }

        }
    }

    private void OnRemoveMemberClicked(object? sender, RoutedEventArgs e)
    {
        if (TeamMemberListBox.SelectedItem is Person selected)
        {
            TeamMembers.Remove(selected);
            selectedTeamMembers.Add(selected);
        }
    }
    private void OnCreateMemberClicked(object? sender, RoutedEventArgs e)
    {
        bool isValid = true;
        string name = FirstNameTextBox.Text;
        string lastName=LastNameTextBox.Text;
        string email=EmailTextBox.Text;
        string cellphone= CellphoneTextBox.Text;
        if (!LettersOnly(name))
        {
            FirstNameTextBox.Background = Brushes.LightCoral;
            isValid = false;
        }
        else
        {
            FirstNameTextBox.Background = Brushes.White;
        }
        if (!LettersOnly(lastName))
        {
            LastNameTextBox.Background = Brushes.LightCoral;
            isValid = false;
        }
        else
        {
            LastNameTextBox.Background = Brushes.White;
        }
        if (!isValidEmail(email))
        {
            EmailTextBox.Background = Brushes.LightCoral;
            isValid = false;
        }
        else
        {
            EmailTextBox.Background = Brushes.White;
        }
        if (!isValidCellphone(cellphone))
        {
            CellphoneTextBox.Background = Brushes.LightCoral;
            isValid = false;
        }
        else
        {
            CellphoneTextBox.Background = Brushes.White;
        }

        if (isValid)
        {
            Person person = new Person(name, lastName, email, cellphone);
            GlobalConfig.Connections[0].AddPersonToDB(person);
            selectedTeamMembers.Add(person);
        }


    }

    private bool LettersOnly(string input)
    {
        return (input != null && input.All(char.IsLetter));
    }

    private bool isValidEmail(string input)
    {
        if (input == null) return false;
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(input, pattern);

    }

    private bool isValidCellphone(string input)
    {
        if (input == null) return false;
        string pattern = @"^0\d+$";
        return Regex.IsMatch(input, pattern);
    }
    private void OnCreateTeamClicked(object? sender, RoutedEventArgs e)
    {
        if(TeamMembers.Count >0 && TeamnameTextBox.Text!="" && TeamnameTextBox.Text!=null )
        {
            int teamID = GlobalConfig.Connections[0].CreateTeam(TeamnameTextBox.Text);
            foreach (Person member in TeamMembers)
            {
                GlobalConfig.Connections[0].AddTeamMember(teamID, member.ID);
            }
            
        }
    }
}