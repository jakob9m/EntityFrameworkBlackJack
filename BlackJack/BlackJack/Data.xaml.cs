using Controller;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BlackJack
{
    /// <summary>
    /// Interaction logic for Data.xaml
    /// </summary>
    public partial class Data : Window
    {
        Controller.Controller controller;
        private String player;

        public Data()
        {
            InitializeComponent();
            controller = new Controller.Controller();
            UpdatePlayerList();
        }

        public void UpdatePlayerList() // Update the player list
        {
            dGridPlayers.ItemsSource = controller.GetAllPlayers().DefaultView;
            dGridGames.ItemsSource = null;
            dGridResults.ItemsSource = null;
        }

        private void btnAddPlayer_Click(object sender, RoutedEventArgs e) // Add a player
        {
            String name = txtBoxAddPlayer.Text;

            controller.CreatePlayer(name);

            UpdatePlayerList();
        }

      

        private void dGridPlayers_PreviewKeyDown(object sender, KeyEventArgs e) // Delete a player with delete key
        {
            if (e.Key == Key.Delete && dGridPlayers.SelectedItem != null)
            {
                // Get the selected item
                DataRowView rowView = (DataRowView)dGridPlayers.SelectedItem;
                string playerId = rowView.Row[0].ToString();


                MessageBox.Show($"{playerId} has been deleted");
                controller.RemovePlayer(playerId);

                UpdatePlayerList();

                e.Handled = true;  // Mark the event as handled
            }
        }

        private void dGridResults_PreviewKeyDown(object sender, KeyEventArgs e) // Delete a result with delete key
        {
            if (e.Key == Key.Delete && dGridResults.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)dGridResults.SelectedItem;

                string GameId = rowView.Row[1].ToString();
                MessageBox.Show($"Result has been deleted");
                controller.RemoveResult(GameId, player);

                UpdatePlayerList();
            }
        }

        private void dGridGames_PreviewKeyDown(object sender, KeyEventArgs e) // Delete a game with delete key
        {
            if (e.Key == Key.Delete && dGridGames.SelectedItem != null)
            {
                DataRowView rowView = (DataRowView)dGridGames.SelectedItem;
                string gameId = rowView.Row[0].ToString();

                MessageBox.Show($"{gameId} has been deleted");
                controller.removeGame(gameId);
                UpdatePlayerList();  
            }
        }



        private void dGridPlayers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e) // Update a player name in the datagrid
        {
            if (e.Column.Header.ToString() == "Name")  
            {
                string newPlayerName = ((TextBox)e.EditingElement).Text;

                DataRowView rowView = (DataRowView)dGridPlayers.SelectedItem;
                string playerId = rowView.Row[0].ToString();

                controller.UpdatePlayerName(playerId, newPlayerName);
            }
        }

        
        private void dGridPlayers_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)// Make the name column (only) editable
        {
            // If the column being generated is not "Name", set it as read-only
            if (e.PropertyName != "Name")
            {
                e.Column.IsReadOnly = true;
            }
        }
        

        private void dGridResults_SelectionChanged(object sender, SelectionChangedEventArgs e)// Get the game details for the selected result
        {
            if (dGridResults.SelectedItem != null) 
            {
                DataRowView rowView = dGridResults.SelectedItem as DataRowView;
                if (rowView != null)
                {
                    string GameId = rowView["GameId"].ToString();
                    
                    DataTable GameFromResult = controller.GetGameFromResult(GameId);

                    dGridGames.ItemsSource = GameFromResult.DefaultView;
                }
            }

        }

        private void dGridPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e) // Get the results for the selected player
        {
            if (dGridPlayers.SelectedItem != null) 
            {
                DataRowView rowView = dGridPlayers.SelectedItem as DataRowView; 
                
                if (rowView != null)
                {
                    // Extract the PlayerId value from the selected row
                    string playerId = rowView["PlayerId"].ToString();
                    player = playerId;


                    // Get the results for the player using the playerId
                    DataTable resultsForPlayer = controller.GetResultsForPlayer(playerId);

                    dGridResults.ItemsSource = resultsForPlayer.DefaultView;
                }

                
            }
        }

        private void dGridGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dGridGames.SelectedItem != null)
            {
                DataRowView rowView = dGridGames.SelectedItem as DataRowView;

                if (rowView != null)
                {
                    // Extract the GameId value from the selected row
                    string gameId = rowView["GameId"].ToString();
                   

                    // Get the results for the game using the gameId
                    DataTable resultsForGame = controller.getResultFromGame(gameId);

                    dGridResults.ItemsSource = resultsForGame.DefaultView;
                }
            }
        }

        private void btnShowAllGames_Click(object sender, RoutedEventArgs e)
        {
          
            dGridGames.ItemsSource = controller.GetAllGames().DefaultView;
            dGridResults.ItemsSource = null;
        }
    }
}
