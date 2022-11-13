using System.Collections;
using UnityEngine;
using WarIsHeaven.Audio;
using WarIsHeaven.Helpers;
using WarIsHeaven.Intents;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Units
{
    public class EnemyUnit : Unit
    {
        [SerializeField] private Killable AttackComponent;
        [SerializeField] private IntentFactory IntentFactory;
        [SerializeField] private Transform IntentContainer;

        private Intent _intent;

        public Killable Attack => AttackComponent;

        public void CreateIntent()
        {
            if (_intent != null) Destroy(_intent.gameObject);

            _intent = IntentFactory.Create();
            _intent.transform.SetParent(parent: IntentContainer, worldPositionStays: false);
        }

        public IEnumerator TakeTurn(Unit target)
        {
            if (_intent == null) yield break;
            _intent.gameObject.SetActive(false);

            FXPlayer.Instance.PlayMonsterAttack();

            Vector3 initialPosition = transform.position;
            yield return Mover.Move(transform: transform, end: target.transform.position, duration: 0.125f);
            transform.position = initialPosition;

            target.Health.ChangeValue(-Attack.Value);
            Destroy(_intent.gameObject);
        }
    }
}
