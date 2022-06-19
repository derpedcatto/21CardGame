using System;

namespace CardGame
{
    public enum CardSuit
    {
        Diamonds = 1,
        Clubs,
        Hearts,
        Spades
    }
    public enum CardType
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class Card
    {
        CardSuit suit;
        CardType type;

        /// <summary>
        /// Card constructor.
        /// </summary>
        public Card(int suit, int type)
        {
            this.suit = (CardSuit)suit;
            this.type = (CardType)type;
        }


        /// <summary>
        /// Returns a card numerical value that is used for calculating 'Hand' results.
        /// </summary>
        public int GetCardValue()
        {
            if ((int)type > 1 && (int)type < 11)
            {
                return (int)type;
            }
            else if (type == CardType.Ace)
            {
                return 11;
            }
            else
            {
                return 10;
            }
        }
        /// <summary>
        /// Prints a stylized card to console. Note: prints ONLY to right.
        /// </summary>
        public void PrintCard()
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            int _y = y;
            string _suit = SuitToString();
            string _type = TypeToString();

            Console.Write("--------");
            Console.SetCursorPosition(x, _y += 1);
            Console.Write("- {0}  {1} -", _type, _suit);
            Console.SetCursorPosition(x, _y += 1);
            Console.Write("-      -");
            Console.SetCursorPosition(x, _y += 1);
            Console.Write("-      -");
            Console.SetCursorPosition(x, _y += 1);
            Console.Write("- {1}  {0} -", _type, _suit);
            Console.SetCursorPosition(x, _y += 1);
            Console.Write("--------");

            Console.SetCursorPosition(x + 10, y); // Moving to right
        }


        /// <summary>
        /// Returns a symbol, representing card value (Ace, 7, etc).
        /// </summary>
        string TypeToString()
        {
            if (type >= CardType.Two && type <= CardType.Nine)
            {
                return Convert.ToString((int)type);
            }
            else
            {
                switch (type)
                {
                    case CardType.Ten:
                        return "X";
                    case CardType.Jack:
                        return "J";
                    case CardType.Queen:
                        return "Q";
                    case CardType.King:
                        return "K";
                    case CardType.Ace:
                        return "A";
                    default:
                        return "?";
                }
            }
        }
        /// <summary>
        /// Returns a symbol, representing card suit (Diamons, etc).
        /// </summary>
        string SuitToString()
        {
            switch(suit)
            {
                case CardSuit.Diamonds:
                    return "♦";
                case CardSuit.Clubs:
                    return "♣";
                case CardSuit.Hearts:
                    return "♥";
                case CardSuit.Spades:
                    return "♠";
                default:
                    return "?";
            }
        }
    }
}