using System.Collections.Generic;
using UnityEngine;
using WarIsHeaven.Helpers;

namespace WarIsHeaven.Cards
{
    using CardsPositions = Dictionary<Card, Vector3>;

    public class Deck : MonoBehaviour
    {
        [SerializeField] protected Transform Container;
        [SerializeField] private UICardPositionDatasource CardPositionDatasource;

        protected readonly List<Card> Cards = new();

        public int Count => Cards.Count;

        public virtual void AddCard(Card card)
        {
            Cards.Add(card);

            card.transform.SetParent(Container);
            UpdateCardPositions();
        }

        public Card TakeCard()
        {
            if (Count == 0) return null;

            Card card = Cards[0];
            RemoveCard(card);
            return card;
        }

        public virtual bool RemoveCard(Card card)
        {
            bool result = Cards.Remove(card);
            if (result) UpdateCardPositions();

            return result;
        }

        public void Shuffle() { Randomizer.RandomizeElements(Cards); }

        private void UpdateCardPositions()
        {
            CardsPositions cardsPositions = CardPositionDatasource.CalculateCardsPositions(Cards);

            foreach (KeyValuePair<Card, Vector3> cardPosition in cardsPositions)
                cardPosition.Key.transform.localPosition = cardPosition.Value;
        }
    }
}
