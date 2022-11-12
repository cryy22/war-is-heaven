using System.Collections.Generic;
using UnityEngine;

namespace WarIsHeaven.Cards
{
    [RequireComponent(typeof(Deck))]
    public class DeckInstantiator : MonoBehaviour
    {
        [SerializeField] private CardFactory CardFactory;
        [SerializeField] private List<CardConfig> StartingCards;

        private void Awake()
        {
            var deck = GetComponent<Deck>();
            foreach (CardConfig cardConfig in StartingCards)
            {
                Card card = CardFactory.Create(cardConfig);
                deck.AddCard(card);
            }

            deck.Shuffle();
        }
    }
}
