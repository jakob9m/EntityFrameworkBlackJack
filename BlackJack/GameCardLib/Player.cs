using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GameCardLib
{
    public class Player
    {
        // Properties
        public Hand Hand { get; set; }
        public bool IsFinished { get; set; }
        public string Name { get; set; }
        public string PlayerID { get; set; }
        public int Wins { get; set; }
        public bool Winner { get; set; }
        public int Score => Hand.Score;

        public event Action<Player, Card> OnCardAdded; // Now sends both the Player and the Card
        public static event Action<Player> OnPlayerCreated;
        public event Action<Player> OnPlayerBust;
        public event Action<Player> OnPlayerFinished;

        // Constructor
        public Player(string playerID, string name)
        {
            PlayerID = playerID;
            Name = name;
            Hand = new Hand();
            OnPlayerCreated?.Invoke(this);
        }

        public void clearHand()
        {
            Hand.Cards.Clear();
        }

        // Methods
        public void AddCard(Card card)
        {
            Hand.AddCard(card);
            OnCardAdded?.Invoke(this, card);

            if (Score > 21)
            {
                OnPlayerBust?.Invoke(this);
                IsFinished = true;  // Set player as finished if busted
            }
        }

        public override string ToString()
        {
            return $"Player {Name}, ID: {PlayerID}, Score: {Hand.Score}";
        }

        public void FinishPlayer()
        {
            IsFinished = true;
            OnPlayerFinished?.Invoke(this);
        }
    }

}
