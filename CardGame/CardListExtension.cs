using System;

namespace CardGame
{
    public static class CardListExtension
    {
        public static void PrintDeck(this IList<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].PrintCard();
            }    
            Console.SetCursorPosition(0, Console.CursorTop + 7);
        }
    }
}
