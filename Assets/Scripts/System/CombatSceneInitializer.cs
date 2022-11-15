using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarIsHeaven.Cards;
using WarIsHeaven.Cards.Decks;
using WarIsHeaven.Coordination;
using WarIsHeaven.Units;

namespace WarIsHeaven.System
{
    public class CombatSceneInitializer : MonoBehaviour
    {
        [SerializeField] private GameCoordinator GameCoordinator;
        [SerializeField] private Deck Deck;
        [SerializeField] private Unit PlayerUnit;

        [SerializeField] private CardFactory CardFactory;
        [SerializeField] private List<CardSet> CardSets;
        [SerializeField] private UnitConfig PlayerUnitConfig;

        private void Awake()
        {
            List<Card> cards = CardSets
                .SelectMany(set => set.CardQuantities)
                .SelectMany(cq => cq.CreateCards(CardFactory))
                .ToList();

            foreach (Card card in cards) Deck.AddCardInstantly(card);
            Deck.Shuffle();

            PlayerUnit.Initialize(PlayerUnitConfig);
        }

        private void Start() { GameCoordinator.BeginGame(); }
    }
}
