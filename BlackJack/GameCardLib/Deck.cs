using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Deck
    {
        private List<Card> cards;
        private Random randomArranger;
        public int NumberOfDecks { get; set; }

        public List<Card> Cards => cards;
        public event Action OnDeckEmptyAndShuffled;



        // Properties
        public bool GameIsDone { get; set; }

        // Constructor
        public Deck(int numberOfDecks = 1)
        {
            cards = new List<Card>();
            randomArranger = new Random();
            InitializeDeck(numberOfDecks);
        }

        // Methods
        public void InitializeDeck(int numberOfDecks)
        {
            cards.Clear();
            NumberOfDecks = numberOfDecks;
            for (int i = 0; i < numberOfDecks; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Value value in Enum.GetValues(typeof(Value)))
                    {
                        cards.Add(new Card(suit, value));
                    }
                }
            }
        }



        public void Shuffle()
        {
            int n = cards.Count;

            while (n > 1)
            {
                n--;
                int k = randomArranger.Next(n + 1);
                Card card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }
        }

        public Card DrawCard()
        {
            if (cards.Count == 0)
            {
                // Repopulate the deck. I'm assuming a single deck for simplicity. Adjust as needed.
                InitializeDeck(1);

                // Shuffle the deck
                Shuffle();

                // Raise the event
                OnDeckEmptyAndShuffled?.Invoke();

            }

            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }


        public int NumberOfCards()
        {
            return cards.Count;
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }


        public int SumOfCards()
        {
            int sum = 0;
            foreach (Card card in cards)
            {
                sum += (int)card.Value;
            }
            return sum;
        }

        public override string ToString()
        {
            string deckString = "";
            foreach (Card card in cards)
            {
                deckString += card.ToString() + "\n";
            }
            return deckString;
        }
    }
}
