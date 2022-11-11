using System.Collections;
using Cards;
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
        [SerializeField] private Transform PlayerHand;

        private bool _isPlayerTurn = true;

        private void Start() { StartCoroutine(RunGame()); }

        private void OnEnable() { EndTurnButton.onClick.AddListener(EndTurnButtonClicked); }

        private void OnDisable() { EndTurnButton.onClick.RemoveListener(EndTurnButtonClicked); }

        private void EndTurnButtonClicked() { _isPlayerTurn = false; }

        private IEnumerator RunGame()
        {
            while (true)
            {
                Card card = Deck.DrawCard();
                card.Flip();
                card.transform.SetParent(parent: PlayerHand, worldPositionStays: false);

                yield return new WaitUntil(() => _isPlayerTurn == false);

                EnemyUnit.Attack(PlayerUnit);
                _isPlayerTurn = true;
            }
        }
    }
}
