using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using tournament_tracker.Configuration;

namespace tournament_tracker.Views;

public partial class CreatePrizeView : UserControl
{
    public CreatePrizeView()
    {
        InitializeComponent();
    }

    public void OnCreatePrizeClicked(object? sender, RoutedEventArgs e)
    {
        bool isValid = int.TryParse(PlaceNumberTextBox.Text, out int placeNumber);
        if (isValid)
            PlaceNumberTextBox.Background = Brushes.White;
        else
            PlaceNumberTextBox.Background = Brushes.LightCoral;
        if(PlaceNameTextBox.Text=="")
        {
            PlaceNameTextBox.Background = Brushes.LightCoral;
            isValid = false;
        }
        else PlaceNameTextBox.Background = Brushes.White;
        if (!decimal.TryParse(PrizeAmountTextBox.Text, out decimal prizeAmount))
        {
            isValid = false;
            PrizeAmountTextBox.Background = Brushes.LightCoral;
        }
        else PrizeAmountTextBox.Background = Brushes.White;
        
        if (isValid)
        {
            if (GlobalConfig.Connections[0].AddPrize(placeNumber, PlaceNameTextBox.Text, prizeAmount))
            {
                Message.Text = "Prize Created !";
            }
            else Message.Text = "Something went wrong !";
        }

    }
}