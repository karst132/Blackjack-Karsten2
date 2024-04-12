using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Karsten2
{
    internal class Hand
    {
        public readonly bool Dealer;
        public readonly bool FromSplit;
        private List<Card> Cards = new List<Card>(); //may have stoped somting from working

        public List<Card>? cards { get; } //may have stoped somting from working
        public Hand(bool dealer, bool fromSplit)
        {
            Dealer = dealer;
            FromSplit = fromSplit;
        }
        public void AddCards(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Cards.Add(new((int)cards[i].Suit, cards[i].Value, cards[i].Name));
            }
        }

        public int HandValue()
        {
            int value = 0;
            int aCounter = 0;
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i].Value == 11)
                {
                    aCounter++;
                }
                value += Cards[i].Value;
            }
            while (aCounter > 0)
            {
                aCounter--;
                if (value > 21)
                {
                    value -= 10;
                }
                else
                {
                    aCounter = 0;
                }
            }
            return value;
        }
        public bool CanSplit()
        {
            if (!Dealer && !FromSplit && Cards.Count == 2 && Cards[0].Value == Cards[1].Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Card> GetCards()
        {
            List<Card> tempCards = new();
            for (int i = 0; i < Cards.Count; i++)
            {
                tempCards.Add(new((int)Cards[i].Suit, Cards[i].Value, Cards[i].Name));
            }
            return tempCards;
        }
    }
}
