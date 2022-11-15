using System.Collections.Generic;
using UnityEngine;

namespace WarIsHeaven.Cards.Decks
{
    [RequireComponent(typeof(Deck))]
    public class DeckInstantiator : MonoBehaviour
    {
        [SerializeField] private CardFactory CardFactory;
        [SerializeField] private List<CardConfig> StartingCards;

        private void Start()
        {
            var deck = GetComponent<Deck>();
            foreach (CardConfig cardConfig in StartingCards)
            {
                Card card = CardFactory.Create(cardConfig);
                deck.AddCardInstantly(card);
            }

            deck.Shuffle();
        }
    }
}
