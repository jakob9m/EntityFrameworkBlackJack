using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Hand
    {
        // Fields
        private List<Card> _cards;

        public List<Card> Cards => _cards;

        // Properties
        public Card LastCard
        {
            get
            {
                if (!_cards.Any())
                    return null;

                return _cards.Last();
            }
        }

        public int NumberOfCards => _cards.Count;

        public int Score
        {
            get
            {
                int score = 0;
                foreach (Card card in _cards)
                {
                    score += (int)card.Value; // assuming enum values for card values are set as per their numeric worth.
                }

                return score;
            }
        }

        // Constructor
        public Hand()
        {
            _cards = new List<Card>();
        }

        // Methods
        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public override string ToString()
        {
            return $"Hand has {NumberOfCards} cards, the cards are: {string.Join(" ", _cards)} Score: {Score}";
        }
    }


}
