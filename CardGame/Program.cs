using System;
using System.Runtime.InteropServices;

namespace CardGame
{
    class Program
    {
        /*-------------------- DISABLE RESIZING --------------------*/
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        /*-------------------- DISABLE RESIZING --------------------*/


        static void Main()
        {
            /*-------------------- DISABLE RESIZING --------------------*/
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
            /*-------------------- DISABLE RESIZING --------------------*/
            
            Console.SetWindowSize(38, 36);
            Console.SetBufferSize(50, 45);

            Console.Title = "21 Card Game";
            Console.WriteLine("Пользователь - Игрок 1\n");
            Console.WriteLine("В руку кидаются две рандомные карты из");
            Console.WriteLine("колоды (Deck) игрока. Совершается два");
            Console.WriteLine("хода, в которых у игроков есть выбор:");
            Console.WriteLine("Взять новую карту или Пропустить ход.");
            Console.WriteLine("Выигрывает игрок, который получил");
            Console.WriteLine("больше всего очков, но не больше 21.");
            Console.WriteLine("При выигрыше игрок забирает все карты");
            Console.WriteLine("со стола себе в колоду.\n");

            while (true)
            {
                Game game = new Game();
                game.EnterPlayerCount();
                game.InitMainDeck();
                game.MainDeckShuffle();
                game.DistributeCardsToPlayers();

                while (true)
                {
                    game.Round();
                    game.Results();

                    if (game.CheckForLosers())
                    {
                        if (game.Restart())
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}

/*
User - Player 1
В руку кидаются две рандомные карты. Совершается два хода, в которых
у игроков есть выбор Взять новую карту или Пропустить ход.
Выигрывает игрок, который ближе всего к 21.
Игрок забирает все карты с Рук других игроков и кладёт в колоду.
*/