using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarIsHeaven.Cards;
using WarIsHeaven.Cards.Decks;
using WarIsHeaven.Coordination;
using WarIsHeaven.Units;

namespace WarIsHeaven.SceneControls
{
    public class CombatSceneInitializer : MonoBehaviour
    {
        [SerializeField] private GameCoordinator GameCoordinator;
        [SerializeField] private Deck Deck;
        [SerializeField] private Unit PlayerUnit;
        [SerializeField] private EnemyUnit EnemyUnit;

        [SerializeField] private CardFactory CardFactory;
        [SerializeField] private CombatSceneConfig Config;

        private void Awake()
        {
            List<Card> cards = Config.CardSets
                .SelectMany(set => set.CardQuantities)
                .SelectMany(cq => cq.CreateCards(CardFactory))
                .ToList();

            foreach (Card card in cards) Deck.AddCardInstantly(card);
            Deck.Shuffle();

            PlayerUnit.Initialize(Config.PlayerUnitConfig);
            EnemyUnit.Initialize(Config.EnemyUnitConfig);
        }

        private void Start() { GameCoordinator.BeginGame(); }
    }
}
