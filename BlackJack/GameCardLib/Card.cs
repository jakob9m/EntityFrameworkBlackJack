using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public class Card
    {
        // Properties
        public Suit Suit { get; set; }
        public Value Value { get; set; }

        // Constructor
        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }

        // Methods
        public override string ToString()
        {
            return $"{Value} {Suit}.png";
        }
    }
}
