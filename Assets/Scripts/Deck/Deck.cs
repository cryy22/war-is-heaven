using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Cards
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] protected Transform Container;

        private readonly List<Card> _cards = new();

        public int Count => _cards.Count;

        public virtual void AddCard(Card card)
        {
            card.transform.SetParent(Container);
            _cards.Add(card);
        }

        public Card TakeCard()
        {
            if (Count == 0) return null;

            Card card = _cards[0];
            RemoveCard(card);
            return card;
        }

        public virtual bool RemoveCard(Card card) { return _cards.Remove(card); }

        public void Shuffle() { Randomizer.RandomizeElements(_cards); }
    }
}
