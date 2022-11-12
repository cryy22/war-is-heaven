using System;
using System.Collections;
using Cards;
using Cards.CardActions;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Coordination
{
    public class GameCoordinator : MonoBehaviour
    {
        [SerializeField] private Button EndTurnButton;
        [SerializeField] private Unit PlayerUnit;
        [SerializeField] private Unit EnemyUnit;
        [SerializeField] private Deck Deck;
        [SerializeField] private Hand PlayerHand;
        [SerializeField] private Deck Discard;

        [SerializeField] private int DrawsPerTurn;

        private bool _isPlayerTurn = true;

        private void Start() { StartCoroutine(RunGame()); }

        private void OnEnable()
        {
            EndTurnButton.onClick.AddListener(EndTurnButtonClicked);
            PlayerUnit.MouseClicked += UnitClickedEventHandler;
            EnemyUnit.MouseClicked += UnitClickedEventHandler;
        }

        private void OnDisable()
        {
            EndTurnButton.onClick.RemoveListener(EndTurnButtonClicked);
            PlayerUnit.MouseClicked -= UnitClickedEventHandler;
            EnemyUnit.MouseClicked -= UnitClickedEventHandler;
        }

        private void EndTurnButtonClicked()
        {
            PlayerHand.ResetSelections();
            _isPlayerTurn = false;
        }

        private void UnitClickedEventHandler(object sender, EventArgs _)
        {
            if (!_isPlayerTurn) return;
            if (PlayerHand.SelectedCard == null) return;

            Card card = PlayerHand.SelectedCard;
            var unit = (Unit) sender;
            card.Play(
                new CardAction.Context
                {
                    Target = unit,
                }
            );

            PlayerHand.RemoveCard(card);
            Discard.AddCard(card);
        }

        private IEnumerator RunGame()
        {
            while (true)
            {
                DrawCards();

                _isPlayerTurn = true;
                yield return new WaitUntil(() => _isPlayerTurn == false);

                EnemyUnit.Attack(PlayerUnit);
            }
        }

        private void DrawCards()
        {
            for (var i = 0; i < DrawsPerTurn; i++)
            {
                if (Deck.Count == 0)
                {
                    if (Discard.Count == 0) return;

                    Discard.Shuffle();
                    while (Discard.Count > 0) Deck.AddCard(Discard.DrawCard());
                }

                Card card = Deck.DrawCard();
                card.Flip(Card.SideType.Front);
                PlayerHand.AddCard(card);
            }
        }
    }
}
