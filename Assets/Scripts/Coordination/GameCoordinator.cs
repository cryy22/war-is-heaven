using System.Collections;
using Units;
using UnityEngine;
using UnityEngine.UI;

namespace Coordination
{
    public class GameCoordinator : MonoBehaviour
    {
        [SerializeField] private Unit PlayerUnit;
        [SerializeField] private Unit EnemyUnit;

        [SerializeField] private Button EndTurnButton;

        private bool _isPlayerTurn = true;

        private void Awake() { StartCoroutine(RunGame()); }

        private void OnEnable() { EndTurnButton.onClick.AddListener(EndTurnButtonClicked); }

        private void OnDisable() { EndTurnButton.onClick.RemoveListener(EndTurnButtonClicked); }

        private void EndTurnButtonClicked() { _isPlayerTurn = false; }

        private IEnumerator RunGame()
        {
            while (true)
            {
                yield return new WaitUntil(() => _isPlayerTurn == false);
                EnemyUnit.Attack(PlayerUnit);
                _isPlayerTurn = true;
            }
        }
    }
}
