using UnityEngine;
using WarIsHeaven.Intents;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Units
{
    public class EnemyUnit : Unit
    {
        [SerializeField] private Killable AttackValueComponent;
        [SerializeField] private IntentFactory IntentFactory;
        [SerializeField] private Transform IntentContainer;

        private Intent _intent;

        public Killable AttackValue => AttackValueComponent;

        public void CreateIntent()
        {
            if (_intent != null) Destroy(_intent.gameObject);

            _intent = IntentFactory.Create();
            _intent.transform.SetParent(parent: IntentContainer, worldPositionStays: false);
        }

        public void TakeTurn(Unit target)
        {
            if (_intent == null) return;

            target.TakeDamage(AttackValue.Value);
            Destroy(_intent.gameObject);
        }
    }
}
