using System;
using System.Collections.Generic;

namespace WarIsHeaven.Cards.Decks
{
    [Serializable]
    public struct CardQuantity
    {
        public CardConfig Card;
        public int Quantity;

        public List<Card> CreateCards(CardFactory factory)
        {
            List<Card> cards = new();
            for (var i = 0; i < Quantity; i++) cards.Add(factory.Create(Card));

            return cards;
        }
    }
}
