using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarIsHeaven.Cards;
using WarIsHeaven.Cards.Decks;
using WarIsHeaven.Coordination;

namespace WarIsHeaven.System
{
    public class CombatSceneInitializer : MonoBehaviour
    {
        [SerializeField] private GameCoordinator GameCoordinator;
        [SerializeField] private Deck Deck;

        [SerializeField] private CardFactory CardFactory;
        [SerializeField] private List<CardSet> CardSets;

        private void Awake()
        {
            List<Card> cards = CardSets
                .SelectMany(set => set.CardQuantities)
                .SelectMany(cq => cq.CreateCards(CardFactory))
                .ToList();

            foreach (Card card in cards) Deck.AddCardInstantly(card);
            Deck.Shuffle();
        }

        private void Start() { GameCoordinator.BeginGame(); }
    }
}
