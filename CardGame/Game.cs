using System;

namespace CardGame
{
    /* NOTES
     * 
     * User is ALWAYS Player 0
     * Card count is 52 (every card gets an unique combination) 
    */

    public class Game
    {
        List<Card> mainDeck = new List<Card>(52);
        List<Player> playerList;

        /* SETUP */
        /// <summary>
        /// Asks player to enter player count for this session.
        /// </summary>
        public void EnterPlayerCount()
        {
            Console.Write("Enter player count (2-4)\n--> ");
            int choice = -1;

            while (choice == -1)
            {
                ConsoleKey input = Console.ReadKey(true).Key;

                switch(input)
                {
                    case ConsoleKey.D2:
                        choice = 2;
                        break;
                    case ConsoleKey.D3:
                        choice = 3;
                        break;
                    case ConsoleKey.D4:
                        choice = 4;
                        break;
                }
            }

            playerList = new List<Player>(choice);
            for (int i = 0; i < choice; i++)
            {
                playerList.Add(new Player());
            }
        }

        /// <summary>
        /// Fills 'mainDeck' with all card combinations (52)
        /// </summary>
        public void InitMainDeck()
        {
            for (int i = 2; i < 15; i++) // Card Type ENUM range
            {
                for (int j = 1; j < 5; j++) // Card Suit ENUM range
                {
                    mainDeck.Add(new Card(j, i));
                }
            }
        }

        /// <summary>
        /// Shuffles cards in 'mainDeck'.
        /// </summary>
        public void MainDeckShuffle()
        {
            Random rand = new();
            int i = mainDeck.Count;

            while (i > 1)
            {
                i--;
                int k = rand.Next(i + 1);
                Card value = mainDeck[k];
                mainDeck[k] = mainDeck[i];
                mainDeck[i] = value;
            }
        }

        /// <summary>
        /// Distributes cards to Players from 'mainDeck' evenly.
        /// </summary>
        public void DistributeCardsToPlayers()
        {
            int bit = mainDeck.Count / playerList.Count;
            int index = 0;

            for (int i = 0; i < playerList.Count; i++)
            {
                for (int j = 0; j < bit; j++)
                {
                    playerList[i].AddDeckCard(mainDeck[index]);
                    index++;
                }
            }

            /* Corner case from dividing by 3. Extra card for user. */
            if (playerList.Count == 3)
            {
                playerList[0].AddDeckCard(mainDeck[index]);
            }
        }


        /* GAME */
        /// <summary>
        /// Prints cards from all Players hands.
        /// </summary>
        public void ShowActiveHands()
        {
            for (int i = playerList.Count - 1; i >= 0; i--)
            {
                Console.WriteLine($"Player {i + 1} Hand ({playerList[i].GetDeckCount()} in deck)");
                playerList[i].PrintHand();
            }
        }

        /// <summary>
        /// Promts user to make a HIT or STAY choice.
        /// </summary>
        public bool UserChoice()
        {
            Console.Write("1 - Hit\n2 - Stay\n--> ");
            int choice = -1;

            while (choice == -1)
            {
                ConsoleKey input = Console.ReadKey(true).Key;

                switch (input)
                {
                    case ConsoleKey.D1:
                        choice = 1;
                        break;
                    case ConsoleKey.D2:
                        choice = 2;
                        break;
                }
            }

            if (choice == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Makes a bot return HIT or STAY value (HIT - True, STAY - False).
        /// </summary>
        public bool BotChoice(int index)
        {
            Random rnd = new();
            if (playerList[index].GetHandCardSum() >= 19) // Don't HIT after getting 19+ points
            {
                return false;
            }
            else if (playerList[index].GetHandCardSum() > 15)
            {
                return rnd.Next() > (Int32.MaxValue / 2); // Random chance to risk HITTING a card
            }
            else // HIT a card
            {
                return true;
            }
        }

        /// <summary>
        /// One round of game.
        /// </summary>
        public void Round()
        {
            /* Giving 2 cards from Deck to Hand */
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].AddHandCard();
                playerList[i].AddHandCard();
            }

            for (int game = 0; game < 2; game++)
            {
                Console.Clear();
                ShowActiveHands();

                /* HIT or STAY */
                bool[] pChoice = new bool[playerList.Count]; // For collecting player inputs

                pChoice[0] = UserChoice();
                for (int i = 1; i < playerList.Count; i++)
                    pChoice[i] = BotChoice(i);

                /* Reading player choices */
                for (int i = 0; i < playerList.Count; i++)
                {
                    if (pChoice[i])
                        playerList[i].AddHandCard();
                }
            }
        }

        /// <summary>
        /// Compile Round results and clear Players hands.
        /// </summary>
        public void Results()
        {
            Console.Clear();
            ShowActiveHands();

            int[] points = new int[playerList.Count]; // Sum of all card points for every Players Hand
            for (int i = 0; i < playerList.Count; i++)
            {
                points[i] = playerList[i].GetHandCardSum();
                Console.Write($"P{i + 1}: {points[i]}    ");
            }
            Console.Write("\n\n");

            /* Getting array index of winner */
            int winnerindex;
            try
            {
                winnerindex = Array.IndexOf(points, points.Where(x => x <= 21).Max());
            }
            catch
            {
                winnerindex = -1;
            }


            /* Checking for Draw */
            bool isRepeated = false;
            if (winnerindex != -1)
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    if (i == winnerindex)
                        continue;

                    if (points[winnerindex] == points[i])
                        isRepeated = true;
                }
            }


            /* Draw - Returning cards back to Hands */
            if (isRepeated || winnerindex == -1)
            {
                Console.Write("It's a draw!");

                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[i].ConcatDeck(playerList[i].GetHand());
                    playerList[i].ClearHand();
                }
            }
            /* Win - Transfering cards from all hands to winner */
            else if (!isRepeated)
            {
                Console.Write($"Player {winnerindex + 1} won!");

                for (int i = 0; i < playerList.Count; i++)
                {
                    playerList[winnerindex].ConcatDeck(playerList[i].GetHand());
                    playerList[i].ClearHand();
                }
            }

            Thread.Sleep(3000);
        }

        /// <summary>
        /// Checks and deletes all Losers from the game. If only User remains, the game is won (True is returned).
        /// </summary>
        public bool CheckForLosers()
        {
            /* User lost */
            if (playerList[0].GetDeckCount() == 0)
            {
                Console.Clear();
                Console.WriteLine("You lost...");
                Thread.Sleep(5000);
                return true;
            }

            /* Bot lost */
            for (int i = 1; i < playerList.Count; i++)
            {
                if (playerList[i].GetDeckCount() == 0)
                {
                    playerList.RemoveAt(i);
                }
            }

            /* User win */
            if (playerList.Count == 1)
            {
                Console.Clear();
                Console.WriteLine("You won!");
                Thread.Sleep(5000);
                return true;
            }
            
            /* Continue playing */
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Asks the player to restart the game.
        /// </summary>
        public bool Restart()
        {
            Console.Clear();
            Console.Write("Start a new game? Y/N\n--> ");

            while (true)
            {
                ConsoleKey input = Console.ReadKey(true).Key;

                if (input == ConsoleKey.Y)
                {
                    Console.Clear();
                    return true;
                }
                else if (input == ConsoleKey.N)
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}