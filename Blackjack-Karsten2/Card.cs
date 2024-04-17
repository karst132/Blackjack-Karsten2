using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Karsten2
{
    internal class Card
    {
        public enum Suits
        {
            Heart,
            Diamonds,
            Cubs,
            Spades,
        }
        public readonly Suits Suit;
        public readonly int Value;
        public readonly String Name;

        //used to create a card
        public Card(int suit, int value, string name)
        {
            Suit = (Suits)suit;
            Value = value;
            Name = name;
        }


    }
}
