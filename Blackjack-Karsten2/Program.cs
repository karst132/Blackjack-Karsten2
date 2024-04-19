using static System.Runtime.InteropServices.JavaScript.JSType;
using static Blackjack_Karsten2.Player;

namespace Blackjack_Karsten2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool PreGame = false;
            bool Playing = true;
            bool Start = false;
            bool Stop = false;
            string? Input;
            bool RequestImputValue;
            string[] InputArray;
            int Decks = 1; //amount of decks if not chansed
            int Players = 1; //amount of players if not chansed
            bool PlayingRound;
            bool StatisticsAvailable = false;
            bool PlayingRoundDealer = false;
            Console.WriteLine("always");
            Console.WriteLine("type 'stop' to stop the game");
            Console.WriteLine();
            Console.WriteLine("pre game");
            Console.WriteLine("type 'options decks nr.' to chanse the amount of decks");
            Console.WriteLine("type 'options players nr.' to chanse the amount of players");
            Console.WriteLine("type 'start' to start");
            Console.WriteLine();
            Console.WriteLine("play sesion prep");
            Console.WriteLine("type 'shuffel' to shuffel deck");
            Console.WriteLine();
            Console.WriteLine("playing pre starting round");
            Console.WriteLine("type 'deals cards' to deal the cards and start a round");
            Console.WriteLine("in any situasion where the deck has to little cards to deal or give a player a card in any other situsion");
            Console.WriteLine("type then 'reshuffel'");
            Console.WriteLine();
            Console.WriteLine("in a round while players are playing");
            Console.WriteLine("type 'split player's hand' to split a players hand");
            Console.WriteLine("type 'give player a card' to give the player a card ");
            Console.WriteLine();
            Console.WriteLine("start dealers round");
            Console.WriteLine("type 'reveal hidden card' to show your hidden card");
            Console.WriteLine();
            Console.WriteLine("dealers round");
            Console.WriteLine("type 'hit' to hit and 'stand to stand'");
            Console.WriteLine();
            Console.WriteLine("afther a round of play");
            Console.WriteLine("type 'put cards from hands into discard pile' to put the cards in the discard pile");
            Console.WriteLine();
            while (!Start)
            {
                Input = Console.ReadLine();
                Input ??= "";
                InputArray = Fuctions.StringToWords(Input);

                if (InputArray.Length == 1)
                {
                    if (InputArray[0] == "start")
                    {
                        Start = true;
                        Console.WriteLine("decks " + Decks + " players " + Players);
                    }
                    else if (InputArray[0] == "stop")
                    {
                        Start = true;
                        Playing = false;
                        Stop = true;
                    }
                }
                else if (InputArray.Length > 1 && (InputArray[0] == "options" || InputArray[0] == "start"))
                {
                    if (InputArray[0] == "start")
                    {
                        Start = true;
                    }

                    RequestImputValue = false;

                    for (int i = 1; i < InputArray.Length; i++)
                    {
                        if ((InputArray[i] == "players" || InputArray[i] == "decks") && !RequestImputValue)
                        {
                            RequestImputValue = true;
                        }
                        else if (RequestImputValue)
                        {
                            try
                            {
                                if (InputArray[i - 1] == "players")
                                {
                                    if (Int32.Parse(InputArray[i]) <= 4 && Int32.Parse(InputArray[i]) > 0)
                                    {
                                        Players = Int32.Parse(InputArray[i]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("play with at least 1 player and at most with 4 players");
                                        Start = false;
                                    }
                                }
                                else if (InputArray[i - 1] == "decks")
                                {
                                    if (Int32.Parse(InputArray[i]) <= 4 && Int32.Parse(InputArray[i]) > 0)
                                    {
                                        Decks = Int32.Parse(InputArray[i]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("play with at least 1 deck and at most 4 players");
                                        Start = false;
                                    }
                                }
                            }
                            catch
                            {
                                Console.WriteLine("no intiger given afther " + InputArray[i - 1]);
                                Start = false;
                            }
                            RequestImputValue = false;
                        }
                    }
                    Console.WriteLine("decks " + Decks + " players " + Players);
                }
            }

            if (Playing)
            {
                Console.WriteLine("started game");

                Deck deck = new(Decks);

                
                Dealer dealer = new();
                List<Player> playersList = new();
                for (int i = 0; Players > i; i++)
                {
                    playersList.Add(new Player());
                }

                bool shuffeldDeck = false;

                while (!shuffeldDeck)
                {
                    Input = Console.ReadLine();
                    Input ??= "";
                    if (Input == "shuffel")
                    {
                        deck.Shuffle();
                        Console.WriteLine("shuffeld deck");
                        shuffeldDeck = true;
                    }
                    else if (Input == "Stop")
                    {
                        Stop = true;
                        Playing = false;
                        shuffeldDeck = true;
                    }
                }
                while (Playing)
                {
                    if (PreGame)
                    {
                        Console.WriteLine("start prepering for next round");
                    }
                    while (PreGame)
                    {
                        Input = Console.ReadLine();
                        Input ??= "";
                        if (Input == "stop")
                        {
                            Stop = true;
                            PreGame = false;
                            Playing = false;
                        }
                        else if (Input == "put cards from hands into discard pile")
                        {
                            for (int i = 0; i < playersList.Count; i++)
                            {
                                deck.AddToDiscard(playersList[i].RemoveHands());
                            }
                            deck.AddToDiscard(dealer.RemoveHands());
                            PreGame = false;
                            Console.WriteLine("put cards into discard pile");
                        }
                    }
                    PlayingRound = false;
                    Console.WriteLine("the deck has " + deck.cards.Count + " cards in it");
                    while (!PlayingRound && !Stop)
                    {
                        Input = Console.ReadLine();
                        Input ??= "";
                        if (Input == "stop")
                        {
                            Playing = false;
                            Stop = true;
                        }
                        else if (Input == "deals cards")
                        {
                            List<Card>? tempCards = deck.GetAndRemoveCard((1 + playersList.Count) * 2);
                            if (tempCards == null)
                            {
                                Console.WriteLine("shuffel discard pile in deck first");
                            }
                            else
                            {
                                for (int i = 0; i < playersList.Count; i++)
                                {
                                    playersList[i].NewHand();
                                }
                                dealer.NewHand();
                                for (int i = 0; i < 2; i++)
                                {
                                    for (int j = 0; j < playersList.Count; j++)
                                    {
                                        List<Card> packageCards = new List<Card>();
                                        packageCards.Add(tempCards[i * (playersList.Count + 1) + j]);
                                        playersList[j].AddCardsToHands(0, packageCards);
                                    }
                                    List<Card> packageCardsDealer = new List<Card>();
                                    packageCardsDealer.Add(tempCards[playersList.Count + i * (playersList.Count + 1)]);
                                    if (i == 0)
                                    {
                                        dealer.AddCardsToHands(0, packageCardsDealer);
                                    }
                                    else
                                    {
                                        dealer.AddHiddenCardsToHands(0, packageCardsDealer);
                                    }
                                    PlayingRound = true;
                                }
                                for (int i = 0; i < playersList.Count; i++)
                                {
                                    Console.Write("player"+(i+1)+" hand has");
                                    for (int j = 0; j < playersList[i].hands[0].cards.Count; j++)
                                    {
                                        Console.Write(" " + playersList[i].hands[0].cards[j].Suit + " " + playersList[i].hands[0].cards[j].Name);
                                    }
                                    Console.WriteLine();
                                }
                                Console.Write("your (dealer) hand has");
                                for(int i = 0;i < dealer.hands[0].cards.Count; i++)
                                {
                                    Console.Write(" " + dealer.hands[0].cards[i].Suit + " " + dealer.hands[0].cards[i].Name);
                                }
                                if (dealer.hands[0].hiddenCards.Count == 1)
                                {
                                    Console.WriteLine(" and 1 hidden card");
                                }
                                else
                                {
                                    Console.WriteLine();
                                }
                            }
                        }
                        else if (Input == "reshuffel")
                        {
                            if (deck.cards.Count < 2+playersList.Count*2)
                            {
                                deck.Reshuffel();
                                Console.WriteLine("reshuffeled");
                            }
                            else
                            {
                                Console.WriteLine("deck does not need to be shuffeled");
                            }
                        }
                    }
                    while (PlayingRound)
                    {
                        
                        List<ActionStates[]> PlayersActions = new List<ActionStates[]>();
                        for (int i = 0; i < playersList.Count; i++)
                        {
                            PlayersActions.Add(playersList[i].Actions(0));
                            for (int j = 0; j < PlayersActions[i].Length; j++)
                            {
                                if (!playersList[i].hands[j].standing)
                                {
                                    Console.WriteLine("player" + (i + 1) + " with hand" + (j + 1) + " action is " + PlayersActions[i][j]);
                                }

                                if (PlayersActions[i][j] == ActionStates.Split)
                                {
                                    bool dealerSplitHands = false;
                                    while (!dealerSplitHands)
                                    {
                                        Input = Console.ReadLine();
                                        Input ??= "";
                                        if (Input == "split player's hand" || Input == "split players hand")
                                        {
                                            if (deck.cards.Count >= 2)
                                            {
                                                playersList[i].Split(deck.GetAndRemoveCard(2));
                                                dealerSplitHands = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("shuffel discard pile into deck first");
                                            }
                                        }
                                        else if (Input == "reshuffel")
                                        {
                                            if (deck.cards.Count < 2)
                                            {
                                                deck.Reshuffel();
                                                Console.WriteLine("reshuffeled");
                                            }
                                            else
                                            {
                                                Console.WriteLine("deck does not need to be shuffeled");
                                            }
                                        }
                                        else if (Input == "stop")
                                        {
                                            dealerSplitHands = true;
                                            Stop = true;
                                            Playing = false;
                                        }
                                    }
                                }
                                else if (PlayersActions[i][j] == ActionStates.Hit && !playersList[i].hands[j].standing)
                                {
                                    bool dealerHitHand = false;
                                    while (!dealerHitHand)
                                    {
                                        Input = Console.ReadLine();
                                        Input ??= "";
                                        if (Input == "give player a card")
                                        {
                                            if (deck.cards.Count >= 1)
                                            {
                                                playersList[i].Hit(deck.GetAndRemoveCard(1), j);
                                                dealerHitHand = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("shuffel discard pile into deck first");
                                            }
                                        }
                                        else if (Input == "reshuffel")
                                        {
                                            if (deck.cards.Count < 1)
                                            {
                                                deck.Reshuffel();
                                                Console.WriteLine("reshuffeled");
                                            }
                                            else
                                            {
                                                Console.WriteLine("deck does not need to be shuffeled");
                                            }
                                        }
                                        else if (Input == "stop")
                                        {
                                            dealerHitHand = true;
                                            Stop = true;
                                            Playing = false;
                                        }
                                    }
                                }
                                else if (PlayersActions[i][j] == ActionStates.Stand && !playersList[i].hands[j].standing)
                                {
                                    playersList[i].hands[j].standing = true;
                                }
                                else if (PlayersActions[i][j] == ActionStates.fault)
                                {
                                    Console.WriteLine("fault");
                                }
                            }
                        }
                        PlayingRound = false;
                        for (int i = 0; i < playersList.Count; i++)
                        {
                            for (int j = 0;j < playersList[i].hands.Count; j++)
                            {
                                if (!playersList[i].hands[j].standing)
                                {
                                    PlayingRound = true;
                                }
                            }
                        }
                    }

                    Console.WriteLine("all players have stand");
                    if (!Stop)
                    {
                        for (int i = 0;i < playersList.Count;i++)
                        {
                            for (int j = 0;j < playersList[i].hands.Count;j++)
                            {
                                Console.Write("player" + (i+1) +" with hand" + (j+1) + " has value of " + playersList[i].hands[j].HandValue() + " with the cards");
                                for (int k = 0; k < playersList[i].hands[j].cards.Count; k++)
                                {
                                    Console.Write(" " + playersList[i].hands[j].cards[k].Suit + " " + playersList[i].hands[j].cards[k].Name);
                                }
                                Console.WriteLine();
                            }
                        }
                        PlayingRoundDealer = true;
                    }

                    bool waitingToReavelCard = true;
                    while (PlayingRoundDealer)
                    {
                        while (waitingToReavelCard)
                        {
                            Input = Console.ReadLine();
                            Input ??= "";
                            if(Input == "reveal hidden card")
                            {
                                dealer.UnhiddeCards();
                                waitingToReavelCard = false;
                                Console.Write("your (dealer) hand has a value of " + dealer.hands[0].HandValue() + " with cards");
                                for(int i = 0;i < dealer.hands[0].cards.Count; i++)
                                {
                                    Console.Write(" " + dealer.hands[0].cards[i].Suit + " " + dealer.hands[0].cards[i].Name);
                                }
                                Console.WriteLine();
                            }
                            else if(Input == "Stop")
                            {
                                Stop = true;
                                waitingToReavelCard = false;
                                PlayingRoundDealer = false;
                            }
                        }

                        if (!Stop)
                        {
                            Input = Console.ReadLine();
                            Input ??= "";
                            if (Input == "stand") 
                            {
                                if (dealer.hands[0].HandValue() > 16)
                                {
                                    PlayingRoundDealer = false;
                                }
                                else
                                {
                                    Console.WriteLine("You may not stand now");
                                }
                            }
                            else if (Input == "hit")
                            {
                                if (dealer.hands[0].HandValue() <= 16 && deck.cards.Count >= 1)
                                {
                                    dealer.Hit(deck.GetAndRemoveCard(1), 0);
                                    Console.Write("your (dealer) hand has a value of " + dealer.hands[0].HandValue() + " with cards");
                                    for (int i = 0; i < dealer.hands[0].cards.Count; i++)
                                    {
                                        Console.Write(" " + dealer.hands[0].cards[i].Suit + " " + dealer.hands[0].cards[i].Name);
                                    }
                                    Console.WriteLine();

                                }
                                else if(dealer.hands[0].HandValue() < 17 && deck.cards.Count < 1)
                                {
                                    Console.WriteLine("shuffel discard pile into deck first");
                                }
                                else
                                {
                                    Console.WriteLine("you may not hit now");
                                }
                            }
                            else if (Input == "reshuffel" )
                            {
                                if (deck.cards.Count < 1 && dealer.hands[0].HandValue() < 17)
                                {
                                    deck.Reshuffel();
                                    Console.WriteLine("reshuffeled");
                                }
                                else
                                {
                                    Console.WriteLine("deck does not need to be shuffeled");
                                }
                            }
                            else if (Input == "stop")
                            {
                                Stop = true;
                                PlayingRoundDealer = false;
                            }
                        }
                    }

                    if (!Stop)
                    {
                        Console.WriteLine();
                        Console.WriteLine("round ended with results");
                        for (int i = 0; i < playersList.Count; i++)
                        {
                            for (int j = 0;j < playersList[i].hands.Count; j++)
                            {
                                if (playersList[i].hands[j].HandValue() <= 21 && (!(playersList[i].hands[j].HandValue() <= dealer.hands[0].HandValue() && dealer.hands[0].HandValue() <= 21))  || (playersList[i].hands[j].HandValue() == dealer.hands[0].HandValue()) && playersList[i].hands[j].HandValue() == 21 && playersList[i].hands[j].cards.Count==2 && !(dealer.hands[0].cards.Count==2))//part afther or checks if player should win because he has blackjack and the dealer not chanse 
                                {
                                    Console.WriteLine("palyer" + (i+1) + " with hand" + (j+1) + " won aigens dealer");
                                }
                                else if (playersList[i].hands[j].HandValue() <= 21 && playersList[i].hands[j].HandValue() == dealer.hands[0].HandValue())
                                {
                                    Console.WriteLine("dealer and palyer" + (i + 1) + " with hand" + (j + 1) + " tied");
                                }
                                else
                                {
                                    Console.WriteLine("dealer won aignst palyer" + (i + 1) + " with hand" + (j + 1));
                                }
                            }
                        }
                    }
                    Console.WriteLine();
                    PreGame = true;
                }
            }

            if (StatisticsAvailable)//needs to be implemented latter
            {

            }
            else
            {
                //Console.WriteLine("not engough data for statistics");
            }
        }
    }

    static class Fuctions
    {
        public static string[] StringToWords(string input)//chek if fuction can be made more effiecient
        {
            char[] charter;
            char[] charterOfWord;
            List<int> spaceLocasions = new();
            List<int> emptyLocasions = new();
            bool locasionIsEmty;
            int emptyLocasionsCounter = 0;
            string[] strings;
            string[] outputs;
            charter = input.ToCharArray();
            if (charter.Length != 0)
            {
                for (int i = 0; i < charter.Length; i++)
                {
                    if (char.IsWhiteSpace(charter[i]))
                    {
                        spaceLocasions.Add(i);
                    }
                }
                strings = new string[spaceLocasions.Count + 1];
                if (spaceLocasions.Count != 0)
                {
                    for (int i = 0; i < spaceLocasions.Count + 1; i++)
                    {
                        if (i == 0)
                        {
                            charterOfWord = new char[spaceLocasions[0]];
                            for (int j = 0; j < charterOfWord.Length; j++)
                            {
                                charterOfWord[j] = charter[j];
                            }
                        }
                        else if (i == spaceLocasions.Count)
                        {
                            charterOfWord = new char[charter.Length - spaceLocasions[i - 1] - 1];
                            for (int j = 0; j < charterOfWord.Length; j++)
                            {
                                charterOfWord[j] = charter[j + spaceLocasions[i - 1] + 1];
                            }
                        }
                        else
                        {
                            charterOfWord = new char[spaceLocasions[i] - spaceLocasions[i - 1] - 1];
                            for (int j = 0; j < charterOfWord.Length; j++)
                            {
                                charterOfWord[j] = charter[j + spaceLocasions[i - 1] + 1];
                            }
                        }
                        strings[i] = new string(charterOfWord);
                    }
                }
                else
                {
                    strings[0] = input;
                }
                for (int i = 0; i < strings.Length; i++)
                {
                    if (strings[i].Equals("") || strings[i].Equals(" "))
                    {
                        emptyLocasions.Add(i);
                    }
                }
                outputs = new string[strings.Length - emptyLocasions.Count];
                for (int i = 0; i < strings.Length; i++)
                {
                    locasionIsEmty = false;
                    for (int j = 0; j < emptyLocasions.Count; j++)
                    {
                        if (i == emptyLocasions[j])
                        {
                            locasionIsEmty = true;
                        }
                    }
                    if (locasionIsEmty)
                    {
                        emptyLocasionsCounter++;
                    }
                    else
                    {
                        outputs[i - emptyLocasionsCounter] = strings[i];
                    }
                }
            }
            else
            {
                outputs = new string[0];
            }
            return outputs;
        }
    }
}


