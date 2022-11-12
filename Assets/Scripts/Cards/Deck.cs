using System.Collections.Generic;
using UnityEngine;
using WarIsHeaven.Helpers;

namespace WarIsHeaven.Cards
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] protected Transform Container;

        protected readonly List<Card> Cards = new();

        public int Count => Cards.Count;

        public virtual void AddCard(Card card)
        {
            card.transform.SetParent(Container);
            Cards.Add(card);
        }

        public Card TakeCard()
        {
            if (Count == 0) return null;

            Card card = Cards[0];
            RemoveCard(card);
            return card;
        }

        public virtual bool RemoveCard(Card card) { return Cards.Remove(card); }

        public void Shuffle() { Randomizer.RandomizeElements(Cards); }
    }
}
