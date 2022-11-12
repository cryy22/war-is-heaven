using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Cards
{
    public class Deck : MonoBehaviour
    {
        private readonly List<Card> _cards = new();

        public int Count => _cards.Count;

        public Card DrawCard()
        {
            if (Count == 0) return null;

            Card card = _cards[0];
            _cards.RemoveAt(0);

            return card;
        }

        public void AddCard(Card card)
        {
            card.Flip(Card.SideType.Back);

            card.transform.SetParent(transform);
            card.transform.localPosition = Vector3.zero;
            card.transform.localScale = Vector3.one;

            _cards.Add(card);
        }

        public void Shuffle() { Randomizer.RandomizeElements(_cards); }
    }
}
