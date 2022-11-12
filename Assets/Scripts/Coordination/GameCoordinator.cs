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
            if (PlayerHand.SelectedCard == null) return;

            var unit = (Unit) sender;
            PlayerHand.SelectedCard.InvokeActions(
                new CardAction.Context
                {
                    Target = unit,
                }
            );
        }

        private IEnumerator RunGame()
        {
            while (true)
            {
                Card card = Deck.DrawCard();
                card.Flip();
                PlayerHand.AddCard(card);

                yield return new WaitUntil(() => _isPlayerTurn == false);

                EnemyUnit.Attack(PlayerUnit);
                _isPlayerTurn = true;
            }
        }
    }
}
