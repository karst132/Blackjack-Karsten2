using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_Karsten2
{
    internal class Deck
    {
        private Random rnd = new Random();
        private List<Card> Cards = new();
        public List<Card> cards { get { return this.Cards; } }
        private DiscardPile Discard = new DiscardPile();
        
        public Deck(int decks)//used to create the deck
        {
            int Value;
            string Name;
            for (int i = 0; i < decks; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 1; k <= 13; k++)
                    {
                        if (k == 1)
                        {
                            Value = 11;
                            Name = "A";
                        }
                        else if (k == 11)
                        {
                            Value = 10;
                            Name = "J";
                        }
                        else if (k == 12)
                        {
                            Value = 10;
                            Name = "Q";
                        }
                        else if (k == 13)
                        {
                            Value = 10;
                            Name = "K";
                        }
                        else
                        {
                            Value = k;
                            Name = k.ToString();
                        }
                        // Card Card = new(j,Value,Name);
                        Cards.Add(new(j, Value, Name));
                    }
                }
            }
        }

        //sorts a list of intigers
        private List<int> Sort(List<int> inputList)
        {
            bool Solved = false;
            int SaveInt;
            if (inputList.Count > 1)
            {
                while (!Solved)
                {
                    Solved = true;
                    for (int i = 1; i < inputList.Count; i++)
                    {
                        if (inputList[i - 1] > inputList[i])
                        {
                            Solved = false;
                            SaveInt = inputList[i];
                            inputList[i] = inputList[i - 1];
                            inputList[i - 1] = SaveInt;
                        }
                    }
                }
            }
            return inputList;
        }

        //used to shuffle the deck
        public void Shuffle()//bad algorithem need optimsing
        { 
            List<Card> OldCards = new();
            for (int i = 0; i < Cards.Count; i++)
            {
                OldCards.Add(new((int)Cards[i].Suit, Cards[i].Value, Cards[i].Name));
            }
            List<int> UsedPosisions = new();
            int Posision;
            for (int i = 0; i < OldCards.Count; i++)
            {
                Posision = rnd.Next(OldCards.Count - UsedPosisions.Count);
                for (int j = 0; j < UsedPosisions.Count; j++)
                {
                    if (Posision >= UsedPosisions[j])
                    {
                        Posision++;
                    }
                }
                Cards[Posision] = OldCards[i];
                UsedPosisions.Add(Posision);
                UsedPosisions = Sort(UsedPosisions);
            }
        }

        public List<Card>? GetAndRemoveCard(int cardsAmount)
        {
            if (Cards.Count < cardsAmount)
            {
                return null;
            }
            else
            {
                List<Card> tempCards = new();
                for (int i = 0; i < cardsAmount; i++)
                {
                    tempCards.Add(new((int)Cards[0].Suit, Cards[0].Value, Cards[0].Name));
                    Cards.Remove(Cards[0]);
                }
                return tempCards;
            }
        }

        public void RefillDeck(List<Card> newCards)
        {
            for (int i = 0; i < newCards.Count(); i++)
            {
                Cards.Add(new((int)newCards[i].Suit, newCards[i].Value, newCards[i].Name));
            }
        }

        public void Reshuffel()
        {
            RefillDeck(Discard.Emty());
            Shuffle();
        }

        public void AddToDiscard (List<Card> cards)
        {
            Discard.AddCards(cards);
        }
    }
}
