using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Karsten2
{
    internal class Player
    {
        public enum ActionStates
        {
            fault = -1,
            Hit,
            Stand,
            Split,
        }
        private List<Hand> Hands = new();
        public List<Hand> hands { get { return this.Hands; } }
        private bool HasSplitHands = false;
        private int Tokens = 200;//curently used in some fuctions but not used to train dealler
        public int tokens { get { return this.Tokens; } }
        private int DeafaulBetTokens = 20;
        

        public void NewHand()
        {
            int betTokens = 0;
            if (Tokens > DeafaulBetTokens)
            {
                betTokens = DeafaulBetTokens;
            }
            else
            {
                betTokens = Tokens;
            }
            Tokens -= betTokens;
            Hands.Add(new(false, false, betTokens));
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
                for (int j = 0; j < Hands[i].cards.Count; j++)
                {
                    tempCards.Add(new((int)Hands[i].cards[j].Suit, Hands[i].cards[j].Value, Hands[i].cards[j].Name));
                }
            }
            Hands.Clear();
            HasSplitHands = false;
            return tempCards;//program.DiscardPile

        }
        public void Split(List<Card> newCards)
        {
            int betTokens = 0;
            List<Card> tempCards = new List<Card>(); ;
            if (Hands.Count == 1 && Hands[0].CanSplit() && !HasSplitHands && newCards.Count == 2)
            {
                for (int i = 0; i < Hands[0].cards.Count; i++)
                {
                    tempCards.Add(new((int)Hands[0].cards[i].Suit, Hands[0].cards[i].Value, Hands[0].cards[i].Name));
                }
                betTokens = Hands[0].betTokens;
                Hands.Remove(Hands[0]);
                for (int i = 0; i < 2; i++)
                {
                    List<Card> cardsPackage = new List<Card>();
                    cardsPackage.Add(tempCards[i]);
                    cardsPackage.Add(newCards[i]);
                    if (i == 0)
                    {
                        Hands.Add(new(false, true, betTokens));
                    }
                    else
                    {
                        if (Tokens > DeafaulBetTokens)
                        {
                            betTokens = DeafaulBetTokens;
                        }
                        else
                        {
                            betTokens = Tokens;
                        }
                        Tokens -= betTokens;
                        Hands.Add(new(false, false, betTokens));
                    }
                    
                    Hands[i].AddCards(cardsPackage);
                }
                HasSplitHands = true;
            }
            else
            {
                Console.WriteLine("Failed to split");
            }
        }

        public void Hit(List<Card> newCards, int hand)
        {
            Hands[hand].AddCards(newCards);
        }

        public ActionStates Action(int dealerValue, int hand)
        {
            try
            {
                if (!(HasSplitHands || Hands.Count > 1  /*only one check should realy be needed in this or statement*/  ) && Tokens >= 1 && Hands[0].CanSplit())
                {
                    return (ActionStates)2;//split
                }
                else if (Hands[hand].HandValue() > 17)//may be random number between x and y or any need to be another nuber other than 17 may also need to include dealer value
                {
                    return (ActionStates)1;//stand
                }
                else
                {
                    return (ActionStates)0;//hit
                }
            }
            catch
            {
                return (ActionStates)(-1);
            }

        }
        public ActionStates[] Actions(int dealerValue)
        {
            List<ActionStates> actions = new List<ActionStates>();
            for (int i = 0; i < Hands.Count; i++)
            {
                actions.Add(Action(dealerValue, i));
            }
            return actions.ToArray();
        }
    }
}
