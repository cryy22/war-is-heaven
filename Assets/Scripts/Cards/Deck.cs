using System.Collections;
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

        public IEnumerator AddCard(Card card)
        {
            Cards.Add(card);
            card.transform.SetParent(Container);
            yield return AnimateNewCardPositions();

            ConfigureAddedCard(card);
        }

        public void AddCardInstantly(Card card)
        {
            Cards.Add(card);
            card.transform.SetParent(Container);
            UpdateCardPositions();

            ConfigureAddedCard(card);
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
            if (Cards.Remove(card) == false) return false;

            UpdateCardPositions();
            ConfigureRemovedCard(card);
            return true;
        }

        public void Shuffle() { Randomizer.RandomizeElements(Cards); }

        protected virtual void ConfigureAddedCard(Card card) { }
        protected virtual void ConfigureRemovedCard(Card card) { }

        private void UpdateCardPositions()
        {
            CardsPositions cardsPositions = CardPositionDatasource.CalculateCardsPositions(Cards);

            foreach (KeyValuePair<Card, Vector3> cardPosition in cardsPositions)
                cardPosition.Key.transform.localPosition = cardPosition.Value;
        }

        private IEnumerator AnimateNewCardPositions()
        {
            CardsPositions cardsPositions = CardPositionDatasource.CalculateCardsPositions(Cards);

            List<Coroutine> coroutines = new();
            foreach (KeyValuePair<Card, Vector3> cardPosition in cardsPositions)
                coroutines.Add(
                    StartCoroutine(
                        Mover.MoveLocal(transform: cardPosition.Key.transform, end: cardPosition.Value)
                    )
                );

            foreach (Coroutine coroutine in coroutines) yield return coroutine;
        }
    }
}
