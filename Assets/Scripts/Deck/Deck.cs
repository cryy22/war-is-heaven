using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Cards
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private Transform Container;

        private readonly List<Card> _cards = new();

        public int Count => _cards.Count;

        public Card TakeCard()
        {
            if (Count == 0) return null;

            Card card = _cards[0];
            _cards.RemoveAt(0);

            return card;
        }

        public virtual void AddCard(Card card)
        {
            card.transform.SetParent(Container);
            _cards.Add(card);
        }

        public void Shuffle() { Randomizer.RandomizeElements(_cards); }
    }
}
