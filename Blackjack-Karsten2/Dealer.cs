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
        public List<Hand> hands { get { return this.Hands; } }

        public void NewHand()
        {
            Hands.Add(new(true, false));
        }
        public void AddCardsToHands(int hand, List<Card> cards)
        {
            Hands[hand].AddCards(cards);
        }

        public void AddHiddenCardsToHands(int hand, List<Card> cards)
        {
            Hands[hand].AddCards(hiddenCards:cards);
        }
        public void Hit(List<Card> newCards, int hand)
        {
            Hands[hand].AddCards(newCards);
        }

        public void UnhiddeCards()
        {
            hands[0].UnhiddeCards();
        }
        public List<Card> RemoveHands()
        {
            List<Card> tempCards = new();
            for (int i = 0; i < Hands.Count; i++)
            {
                for (int j = 0; j < Hands[i].cards.Count; j++)
                {
                    tempCards.Add(new((int)Hands[i].cards[j].Suit, Hands[i].cards[j].Value, Hands[i].cards[j].Name));
                }
                for (int j = 0; j < Hands[i].hiddenCards.Count; j++)
                {
                    tempCards.Add(new((int)Hands[i].hiddenCards[j].Suit, Hands[i].hiddenCards[j].Value, Hands[i].hiddenCards[j].Name));
                }
            }
            Hands.Clear();
            return tempCards;//program.DiscardPile

        }
    }
}
