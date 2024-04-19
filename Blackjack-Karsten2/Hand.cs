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
        private int BetTokens;
        public int betTokens { get { return this.BetTokens; } }
        private List<Card> Cards = new List<Card>();
        public List<Card> cards { get { return this.Cards; } } 

        private List<Card> HiddenCards = new List<Card>();
        public List<Card> hiddenCards { get { return this.HiddenCards; } }
        private bool Standing = false;
        public bool standing { get { return this.Standing; } set { Standing = value; } }
        public Hand(bool dealer, bool fromSplit,int betTokens = 0)
        {
            Dealer = dealer;
            FromSplit = fromSplit;
            if (!dealer)
            {
                BetTokens = betTokens;
            }
        }
        public void AddCards(List<Card>? cards = null, List<Card>? hiddenCards = null)
        {
            hiddenCards ??= new List<Card>();
            cards ??= new List<Card>();
            for (int i = 0; i < cards.Count; i++)
            {
                Cards.Add(new((int)cards[i].Suit, cards[i].Value, cards[i].Name));
            }
            if (Dealer)
            {
                for (int i = 0; i < hiddenCards.Count; i++)
                {
                    HiddenCards.Add(new((int)hiddenCards[i].Suit, hiddenCards[i].Value, hiddenCards[i].Name));
                }
            }
        }

        public void UnhiddeCards()
        {
            for(int i = 0;i < HiddenCards.Count; i++) {
                cards.Add(new((int)HiddenCards[i].Suit, HiddenCards[i].Value, HiddenCards[i].Name));
            }
            HiddenCards.Clear();
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
    }
}
