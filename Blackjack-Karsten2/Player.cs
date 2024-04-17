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
        private bool HasSplitHands = false;
        private int Tokens = 100;
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
            HasSplitHands = false;
            return tempCards;//program.DiscardPile

        }
        public void Split(List<Card> newCards)
        {
            List<Card> tempCards;
            if (Hands.Count == 1 && Hands[0].CanSplit() && !HasSplitHands && newCards.Count == 2)
            {
                tempCards = Hands[0].cards;
                tempCards ??= new List<Card>();
                Hands.Remove(Hands[0]);
                for (int i = 0; i < 2; i++)
                {
                    List<Card> cardsPackage = new List<Card>();
                    cardsPackage.Add(tempCards[i]);
                    cardsPackage.Add(newCards[i]);
                    Hands.Add(new(false, true));
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
                    //Split();
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
