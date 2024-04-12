using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blackjack_Karsten2
{
    internal class Program
    {
        static class Fuctions{
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
            if(charter.Length != 0){
                for(int i = 0; i < charter.Length; i++){
                    if(char.IsWhiteSpace(charter[i])){
                        spaceLocasions.Add(i);
                    }
                }
                strings = new string[spaceLocasions.Count+1];
                if(spaceLocasions.Count != 0){
                    for(int i = 0; i < spaceLocasions.Count+1; i++){
                        if(i == 0){
                            charterOfWord = new char[spaceLocasions[0]];
                            for(int j = 0; j < charterOfWord.Length; j++){
                                charterOfWord[j] = charter[j];
                            }
                        }
                        else if(i == spaceLocasions.Count){
                            charterOfWord = new char[charter.Length-spaceLocasions[i-1]-1];
                            for(int j = 0; j < charterOfWord.Length; j++){
                                charterOfWord[j] = charter[j+spaceLocasions[i-1]+1];
                            }                        
                        }
                        else{
                            charterOfWord = new char[spaceLocasions[i]-spaceLocasions[i-1]-1];
                            for(int j = 0; j < charterOfWord.Length; j++){
                                charterOfWord[j] = charter[j+spaceLocasions[i-1]+1];
                            }
                        }
                        strings[i] = new string (charterOfWord);
                    }
                }
                else{
                    strings[0] = input;
                }
                for(int i = 0; i < strings.Length; i++){
                    if(strings[i].Equals("") || strings[i].Equals(" ")){
                        emptyLocasions.Add(i);
                    }
                }
                outputs = new string[strings.Length-emptyLocasions.Count];
                for(int i = 0; i < strings.Length; i++){
                    locasionIsEmty = false;
                    for(int j = 0; j < emptyLocasions.Count; j++){
                        if(i == emptyLocasions[j]){
                            locasionIsEmty= true;
                        }
                    }
                    if(locasionIsEmty){
                        emptyLocasionsCounter++;
                    }
                    else{
                        outputs[i-emptyLocasionsCounter] = strings[i];
                    }
                }
            }
            else{
                outputs = new string[0];
            }
            return outputs;
        }

    }
        static void Main(string[] args)
        {
            bool playing = true;
            bool start = false;
            bool stop = false;
            bool cardsDealt;
            string? input;
            bool requestImputValue;
            string[] inputArray;
            int decks = 1; //amount of decks if not chansed
            int players = 1; //amount of players if not chansed
            bool playingRound;
            while (!start)
            {
                input = Console.ReadLine();
                input ??= "";
                inputArray = Fuctions.StringToWords(input);
                // Console.WriteLine(inputArray.Length);
                if (inputArray.Length == 1)
                {
                    if (inputArray[0] == "start")
                    {
                        start = true;
                        Console.WriteLine("decks " + decks + " players " + players);
                    }
                    else if (inputArray[0] == "stop")
                    {
                        start = true;
                        playing = false;
                        stop = true;
                    }
                }
                else if (inputArray.Length > 1 && (inputArray[0] == "options" || inputArray[0] == "start"))
                {
                    if (inputArray[0] == "start")
                    {
                        start = true;
                    }
                    requestImputValue = false;
                    for (int i = 1; i < inputArray.Length; i++)
                    {
                        if ((inputArray[i] == "players" || inputArray[i] == "decks") && !requestImputValue)
                        {
                            requestImputValue = true;
                        }
                        else if (requestImputValue)
                        {
                            try
                            {
                                if (inputArray[i - 1] == "players")
                                {
                                    if (Int32.Parse(inputArray[i]) <= 4 && Int32.Parse(inputArray[i]) > 0)
                                    {
                                        players = Int32.Parse(inputArray[i]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("play with at least 1 player and at most with 4 players");
                                        start = false;
                                    }
                                }
                                else if (inputArray[i - 1] == "decks")
                                {
                                    if (Int32.Parse(inputArray[i]) <= 4 && Int32.Parse(inputArray[i]) > 0)
                                    {
                                        decks = Int32.Parse(inputArray[i]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("play with at least 1 deck and at most 4 players");
                                        start = false;
                                    }
                                }
                            }
                            catch
                            {
                                Console.WriteLine("no intiger given afther " + inputArray[i - 1]);
                                start = false;
                            }
                            requestImputValue = false;
                        }

                    }
                    Console.WriteLine("decks " + decks + " players " + players);
                }
            }
            if (playing)
            {
                Console.WriteLine("started");

                // Method.Card method = new();
                // Console.WriteLine("♠♣♥♦");
                Deck deck = new(decks);
                deck.Shuffle(); //let dealer shuffel deck
                while (playing)
                {

                    playingRound = false;
                    cardsDealt = false;
                    input = Console.ReadLine();
                    input ??= "";
                    inputArray = Fuctions.StringToWords(input);
                    while (!playingRound && !stop)
                    {
                        if (inputArray.Length == 1 && inputArray[0] == "stop")
                        {
                            playing = false;
                            stop = true;
                        }
                        else if (inputArray.Length == 1 && inputArray[0] == "start")
                        {
                            if (cardsDealt)
                            {
                                playingRound = true;
                            }
                            else
                            {
                                Console.WriteLine("you did't dealt the cards");
                            }

                        }
                        else if (inputArray.Length == 2 && inputArray[0] == "deals" && inputArray[1] == "cards")
                        {

                        }
                    }

                    while (playingRound)
                    {

                    }
                }
            }
            // for(int i = 0;deck.Cards.Count > i;i++){
            //     Console.WriteLine(i + " " + deck.Cards[i].Suit + " " + deck.Cards[i].Name);
            // }



            // for(int i = 0;deck.Cards.Count > i;i++){
            //     Console.WriteLine(i + " " + deck.Cards[i].Suit + " " + deck.Cards[i].Name);
            // }

            // List<Card> Cards = new();
            // Cards.Add(new Card(1,2,"2"));
            // Cards.Add(new Card(2,2,"2"));

            // Method.Player player = new();
            // player.NewHand();
            // player.AddCardsToHands(0,Cards);
            // player.Split();


        }
    }
}

