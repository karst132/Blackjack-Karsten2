using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Karsten2
{
    internal class Player
    {
        private List<Hand> Hands = new();
        private bool HasSplitHands = false;

        public void NewHand()
        {
            Hands.Add(new(false, false));
        }

        public void AddCardsToHands(int hand, List<Card> cards)
        {
            Hands[hand].AddCards(cards);
        }

        public List<Card> RemoveHands()
        {
            List<Card> tempCards = new();
            for (int i = 0; i < Hands.Count; i++)
            {
                for (int j = 0; i < Hands[i].cards.Count; j++)
                {
                    tempCards.Add(new((int)Hands[i].cards[j].Suit, Hands[i].cards[j].Value, Hands[i].cards[j].Name));
                }
                Hands.Remove(Hands[i]);
            }
            return tempCards;//program.DiscardPile

        }
        public void Split()
        {
            List<Card> tempCards;
            if (Hands.Count == 1 && Hands[0].CanSplit() && !HasSplitHands)
            {
                tempCards = Hands[0].cards;
                tempCards ??= new List<Card>();
                Hands.Remove(Hands[0]);
                for (int i = 0; i < 2; i++)
                {
                    Hands.Add(new(false, true));
                    Hands[i].AddCards(tempCards);
                }
                HasSplitHands = true;
            }
            else
            {
                Console.WriteLine("Failed to split");
            }
        }
    }
}
