using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCardLib
{
    public enum Value
    {
        Ace = 1, // I've set Ace to 1 for simplicity, but its value can be 1 or 11 in the game logic
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10, // Face cards (Jack, Queen, King) typically have a value of 10 in Blackjack
        Queen = 10,
        King = 10
    }
}
