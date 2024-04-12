using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Karsten2
{
    internal class Dealer
    {
        private List<Hand> Hands = new();

        public void NewHand()
        {
            Hands.Add(new(true, false));
        }

        public void AddCardsToHands(int hand, List<Card> cards)
        {
            Hands[hand].AddCards(cards);
        }

        public void RemoveHands()
        {
            for (int i = 0; i < Hands.Count; i++)
            {
                Hands.Remove(Hands[i]);
            }
        }
    }
}
