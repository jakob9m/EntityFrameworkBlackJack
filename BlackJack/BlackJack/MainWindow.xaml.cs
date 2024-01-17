using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows;
using GameCardLib;
using System.ComponentModel.Design;
using BlackJack;
using Controller;
using System.Data;
using Utilities;

namespace Blackjack
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Player.OnPlayerCreated += Player_OnPlayerCreated;
            controller = new Controller.Controller();

        }

        Deck deck;
        List<Player> players = new List<Player>();
        Player dealer;
        String currentplayer;
        String GameId;
        int deckSize;
        DataTable dt;
        private Queue<Player> playersQueue = new Queue<Player>();
        Controller.Controller controller;
       

        private void startGame() //method to start a game round
        {
            ClearCardImagesFromBoard("All");
            lblHandValue.Content = "";
            btnHit.IsEnabled = true;
            btnStand.IsEnabled = true;

            if (players.Count == 0)
            {
                MessageBox.Show("No players have been added!");
                return;
            }

            if (dealer == null)
            {
                dealer = new Player("Dealer", "Dealer");

                dealer.OnCardAdded -= Player_OnCardAdded;

                dealer.OnCardAdded += Player_OnCardAdded;
            }

            // Initialize the dealer
            dealer.Hand = new Hand();

            // Empty the queue if it's not already (useful for game restarts)
            playersQueue.Clear();

            // Subscribe to events for each player and enqueue them for turns
            players.ForEach(player =>
            {
                // Clear player's hand by re-instantiating it
                player.clearHand();
                player.IsFinished = false;

                player.OnPlayerBust -= Player_OnPlayerBust;  // Remove event handler to avoid multiple subscriptions
                player.OnCardAdded -= Player_OnCardAdded;
                player.OnPlayerFinished -= Player_OnPlayerFinished;

                player.OnCardAdded += Player_OnCardAdded;
                player.OnPlayerBust += Player_OnPlayerBust;
                player.OnPlayerFinished += Player_OnPlayerFinished;

                // Enqueue player for their turn
                playersQueue.Enqueue(player);
            });

            // Deal initial cards to the dealer
            dealer.AddCard(deck.DrawCard());
            dealer.AddCard(deck.DrawCard());

            // Start the game for the first player in the queue
            StartNextPlayerTurn();
        }

        private void StartNextPlayerTurn()//method to start the next player's turn
        {
            if (playersQueue.Count > 0)
            {
                ClearCardImagesFromBoard("Player");
                Player currentPlayer = playersQueue.Dequeue();

                // Update UI for currentPlayer's turn
                currentplayer = currentPlayer.Name;
                gBoxPlayer.Header = currentplayer;

                currentPlayer.AddCard(deck.DrawCard());
                currentPlayer.AddCard(deck.DrawCard());
            }
        }

        private void Player_OnCardAdded(Player player, Card card)//method to add a card to the player's hand
        {
            String Type = "";
            // Update UI when a card is added to player's hand
            if (player.Name != "Dealer")
            {
                Type = "Player";
                AddCardImageToStackPanel(card.ToString(), Type);

            }
            else if (player.Name == "Dealer" && player.Hand.NumberOfCards != 2)
            {
                Type = "Dealer";
                AddCardImageToStackPanel(card.ToString(), Type);

            }
            else if (player.Name == "Dealer" && player.Hand.NumberOfCards == 2) //hide the second card for dealer
            {
                Type = "DealerHidden";
                AddCardImageToStackPanel(card.ToString(), Type);
            }
            DisplayDeck();
        }

       


        private void BtnHit_Click(object sender, RoutedEventArgs e) //draw a card for the current player
        {

            // Find the current player based on the name
            players.FirstOrDefault(p => p.Name == currentplayer)?.AddCard(deck.DrawCard());
        }


        private void BtnStand_Click(object sender, RoutedEventArgs e)//end the current player's turn
        {
            // Find the current player based on the name
            Player currentPlayerObject = players.FirstOrDefault(p => p.Name == currentplayer);
            ClearCardImagesFromBoard("Player");

            // If we found the current player, set its IsFinished property and raise the event
            if (currentPlayerObject != null)
            {
               // currentPlayerObject.IsFinished = true;
                currentPlayerObject.FinishPlayer();
                
                // Update currentplayer to the next player in the list
                int currentIndex = players.IndexOf(currentPlayerObject);
                if (currentIndex < players.Count - 1) // If it's not the last player
                {
                    currentplayer = players[currentIndex + 1].Name;
                    StartNextPlayerTurn();
                }
                else
                {
                   
                    btnHit.IsEnabled = false;
                    btnStand.IsEnabled = false;
                   
                    DealerPlay();
                }
                

            }
        }


        private void DealerPlay()//method to play the dealer's turn
        {
            ClearCardImagesFromBoard("All");
            AddCardImageToStackPanel(dealer.Hand.Cards[0].ToString(), "Dealer");
            AddCardImageToStackPanel(dealer.Hand.Cards[1].ToString(), "Dealer");
            // Dealer draws cards while their score is below 17
            while (dealer.Score < 17)
            {
                dealer.AddCard(deck.DrawCard());
            }

            var winners = players.Where(player => player.Score <= 21 && (dealer.Score > 21 || player.Score > dealer.Score)).ToList();

            // Determine the winner and display a message
            if (dealer.Score <= 21 && winners.Count == 0)
            {
                dealer.Wins++;
                MessageBox.Show("Dealer wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (winners.Count > 0)
            {
                foreach (var winner in winners)
                {
                    winner.Wins++;
                    MessageBox.Show($"{winner.Name} wins!", "Congratulations", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (winners.Count == 0 && dealer.Score <= 21)
            {
                MessageBox.Show("It's a tie!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No one wins. All players and dealer busted!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            lblScore.Content = DisplayPlayersByScore();
            EndGame();
        }


        private void EndGame() //method to end the game
        {
            // Display a MessageBox to ask the player for the next action
            MessageBoxResult result = MessageBox.Show("Do you want to start a new game?", "End of Game", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    // Logic for starting a new game
                    startGame();
                    break;

                case MessageBoxResult.No: //save to db 

                    upDateToDb();
                    dealer = null;
                    players.Clear();
                    ClearCardImagesFromBoard("All");
                    lblScore.Content = "";  
                    lblHandValue.Content = "";
                    break;

                case MessageBoxResult.Cancel: // save to db and quit
                    upDateToDb();
                    Application.Current.Shutdown();
                    break;
            }
        }

       

        public void upDateToDb()
        {
            // 1. Get the display text for all players.
            string Result = DisplayPlayersByScore();

            // 2. Loop through each player and save their result to the database.
            foreach (Player player in players)
            {
                // Using GameId field and player's PlayerId for saving
                controller.CreateResult(player.PlayerID, GameId, Result);
            }
        }

        private void BtnNewGame_Click(object sender, RoutedEventArgs e) // show new game settings
        {
           
            dt = controller.GetAllPlayers();
            lBoxPlayers.ItemsSource = dt.DefaultView;
            gBoxNewGame.Visibility = Visibility.Visible;
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfDecks = cBoxNoOfDecks.SelectedIndex + 1;
            deckSize = numberOfDecks;
            players.Clear();
            dealer = null;

            foreach (var item in lBoxPlayers.Items)
            {
                // Get the ListBoxItem for the current data item
                ListBoxItem listBoxItem = lBoxPlayers.ItemContainerGenerator.ContainerFromItem(item) as ListBoxItem;

                // Find the CheckBox inside the ListBoxItem
                var checkBox = ContainerChecker.FindChild<CheckBox>(listBoxItem, string.Empty);

                if (checkBox != null && checkBox.IsChecked == true)
                {
                    string playerName = checkBox.Content.ToString();

                    // Filter the dataTable to get playerId for the checked playerName
                    DataRow[] rows = dt.Select($"Name = '{playerName}'"); // Assuming column name in dataTable is "Name"

                    if (rows.Length > 0)
                    {
                        string playerId = rows[0]["PlayerId"].ToString(); // Assuming column name for player ID in dataTable is "PlayerId"
                        
                        AddPlayer(playerId, playerName);
                    }
                }
            }

            
            GameId = controller.CreateGame(players.Count, deckSize);

            deck = new Deck();
            deck.InitializeDeck(numberOfDecks);
            deck.Shuffle();
            deck.OnDeckEmptyAndShuffled -= Deck_OnDeckEmptyAndShuffled;
            deck.OnDeckEmptyAndShuffled += Deck_OnDeckEmptyAndShuffled;

            startGame();
            gBoxNewGame.Visibility = Visibility.Hidden;
        }

        private void AddPlayer(string playerID, string playerName)
        {
            Player newPlayer = new Player(playerID, playerName);
            

            players.Add(newPlayer);
        }


        private void AddCardImageToStackPanel(string cardImageFileName, string Type)//method to add a card image to the stack panel
        {
            var cardImage = new Image();
            cardImage.Source = new BitmapImage(new Uri($"pack://application:,,,/Cards/{cardImageFileName}", UriKind.Absolute));
            cardImage.Height = 100; // or any other desired height

            if (Type == "Player")
            {
                PlayerArea.Children.Add(cardImage);
            }
            else if (Type == "Dealer")
            {
                DealerArea.Children.Add(cardImage);
            }else if (Type == "DealerHidden")
            {
                cardImage.Source = new BitmapImage(new Uri($"pack://application:,,,/Cards/red joker.png", UriKind.Absolute));
                DealerArea.Children.Add(cardImage);
            }
            
        }


        private void Player_OnPlayerCreated(Player newPlayer)
        {
            if (newPlayer.Name != "Dealer")
            {
                MessageBox.Show($"{newPlayer.Name} is added.");
            }
        }

        private void Player_OnPlayerBust(Player bustedPlayer)
        {
            MessageBox.Show($"{bustedPlayer.Name} has busted!");
            ClearCardImagesFromBoard("Player");
            DisplayPlayersValues();

            // Update currentplayer to the next player in the list
            int currentIndex = players.IndexOf(bustedPlayer);
            bustedPlayer.IsFinished = true;

            if (currentIndex < players.Count - 1) // If it's not the last player
            {
                currentplayer = players[currentIndex + 1].Name;
                StartNextPlayerTurn();
                // Optionally, you can also update the UI or any other logic here
            }
           

            // Check if all players have busted
            if (players.All(p => p.IsFinished))
            {
                // If all players have busted, restart the game
                MessageBox.Show($"All players turn is over");
                DealerPlay();
                return;  // Exit the method early since we're starting over
            }
        }


        private void ClearCardImagesFromBoard(String Type)
        {
            // Assuming the name of your StackPanel is "stackPanelCards"
            
            if (Type == "All")
            {
                DealerArea.Children.Clear();
            }
            PlayerArea.Children.Clear();

        }

        private void Player_OnPlayerFinished(Player finishedPlayer)
        {
            MessageBox.Show($"{finishedPlayer.Name} is finished!");
            ClearCardImagesFromBoard("Player");
            DisplayPlayersValues();

        }

        private bool AreAllPlayersFinished()
        {
            return players.All(p => p.IsFinished);
        }

        private void Deck_OnDeckEmptyAndShuffled()
        {
            MessageBox.Show("The deck was empty and has been shuffled!");
        }

        private void btnShuffle_Click(object sender, RoutedEventArgs e)
        {
            deck.Shuffle();
            MessageBox.Show("The deck has been shuffled!");
        }
       
        public string DisplayPlayersByScore()
        {
            // Combine dealer into the list for sorting
            List<Player> allPlayers = new List<Player>(players);
            allPlayers.Add(dealer);

            // Sort players by score in descending order
            allPlayers.Sort((p1, p2) => p2.Wins.CompareTo(p1.Wins)); // p2 before p1 for descending

            StringBuilder displayText = new StringBuilder();

            for (int i = 0; i < allPlayers.Count; i++)
            {
                displayText.AppendLine($"{i + 1}: {allPlayers[i].Name} : {allPlayers[i].Wins} score");
            }

            return displayText.ToString();
        }

        public void DisplayPlayersValues()
        {
            StringBuilder scoresText = new StringBuilder();

            foreach (var player in players)
            {
                scoresText.AppendLine($"{player.Name} hand value: {player.Score}");
            }

            lblHandValue.Content = scoresText.ToString();  // For WPF
        }

        public void DisplayDeck()
        {
            lblDeck.Content = $"decks: {deck.NumberOfDecks}, Cards: {deck.NumberOfCards()}";
        }

        private void btnSaveScore_Click(object sender, RoutedEventArgs e)
        {
            // Combine dealer into the list for sorting
            List<Player> allPlayers = new List<Player>(players);
            allPlayers.Add(dealer);

            // Sort players by score in descending order
            allPlayers.Sort((p1, p2) => p2.Wins.CompareTo(p1.Wins)); // p2 before p1 for descending

            StringBuilder SaveText = new StringBuilder();

            for (int i = 0; i < allPlayers.Count; i++)
            {
                SaveText.AppendLine($"{i + 1}: {allPlayers[i].Name} : {allPlayers[i].Wins} score");
            }

            Utilities.Serialization.SerializeTextToFile(SaveText);
        }

        private void btnData_Click(object sender, RoutedEventArgs e)
        {
            Data dataWindow = new Data();  // Create a new instance of the Data window
            dataWindow.Show();
        }
    }
}

