using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Karsten2
{
    internal class DiscardPile
    {
        public readonly List<Card> Cards = new();
        public List<Card> cards { get { return this.Cards; } };
        public void AddCards(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Cards.Add(new((int)cards[i].Suit, cards[i].Value, cards[i].Name));
            }
        }

        public void Emty()
        {//make it return a list of all cards removed 
            for (int i = 0; i < Cards.Count; i++)
            {
                Cards.Remove(Cards[i]);
            }
        }

    }
}
}
