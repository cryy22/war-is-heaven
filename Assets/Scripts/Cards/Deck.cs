using System.Collections.Generic;
using Helpers;
using UnityEngine;

namespace Cards
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private CardFactory CardFactory;
        [SerializeField] private List<CardConfig> StartingCards;

        private readonly List<Card> _cards = new();

        private void Awake() { PopulateDeck(); }

        private void PopulateDeck()
        {
            foreach (CardConfig config in StartingCards)
            {
                Card card = CardFactory.Create(config);

                card.transform.SetParent(parent: transform, worldPositionStays: false);
                _cards.Add(card);
            }

            Randomizer.RandomizeElements(_cards);
        }
    }
}
