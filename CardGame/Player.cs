using System;

namespace CardGame
{
    public class Player
    {
        List<Card> deck = new List<Card>();
        List<Card> hand = new List<Card>();

        /// <summary>
        /// Merges card list with Player Deck.
        /// </summary>
        public void ConcatDeck(List<Card> other)
        {
            deck.AddRange(other);
        }

        /// <summary>
        /// Clears a players Hand.
        /// </summary>
        public void ClearHand()
        {
            hand.Clear();
        }

        /// <summary>
        /// Adds a card to Players Deck.
        /// </summary>
        public void AddDeckCard(Card card)
        {
            deck.Add(card);
        }

        /// <summary>
        /// Adds a random card to Players Hand from Deck. If Deck is empty, does nothing.
        /// </summary>
        public void AddHandCard()
        {
            if (deck.Count < 1)
                return;

            Random rnd = new();
            int i = rnd.Next(deck.Count);

            hand.Add(deck[i]);
            deck.RemoveAt(i);
        }


        /// <summary>
        /// Returns a Deck card count.
        /// </summary>
        public int GetDeckCount()
        {
            return deck.Count;
        }

        /// <summary>
        /// Returns a Hand card count.
        /// </summary>
        public int GetHandCount()
        {
            return hand.Count;
        }

        /// <summary>
        /// Returns a card value sum from Hand. Used for results calculations.
        /// </summary>
        public int GetHandCardSum()
        {
            int sum = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                sum += hand[i].GetCardValue();
            }
            return sum;
        }


        /// <summary>
        /// Returns a card from Hand by index.
        /// </summary>
        public Card GetHandCard(int index)
        {
            return hand[index];
        }

        /// <summary>
        /// Returns players Hand.
        /// </summary>
        public List<Card> GetHand()
        {
            return hand;
        }

        /// <summary>
        /// Returns players Deck.
        /// </summary>
        public List<Card> GetDeck()
        {
            return deck;
        }


        /// <summary>
        /// Print players Hand.
        /// </summary>
        public void PrintHand()
        {
            hand.PrintDeck();
        }

        /// <summary>
        /// Print players Deck.
        /// </summary>
        public void PrintDeck()
        {
            deck.PrintDeck();
        }
    }
}