using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarIsHeaven.Audio;
using WarIsHeaven.Cards.CardActions;
using WarIsHeaven.Intents;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Units
{
    public class EnemyUnit : Unit
    {
        [SerializeField] private Killable AttackComponent;
        [SerializeField] private IntentFactory IntentFactory;
        [SerializeField] private Transform IntentContainer;
        [SerializeField] private List<IntentConfig> IntentConfigs;

        private Intent _intent;

        public Killable Attack => AttackComponent;

        public void CreateIntent()
        {
            if (_intent != null) Destroy(_intent.gameObject);
            IntentConfig config = IntentConfigs[Random.Range(minInclusive: 0, maxExclusive: IntentConfigs.Count)];

            _intent = IntentFactory.Create(config);
            _intent.transform.SetParent(parent: IntentContainer, worldPositionStays: false);
        }

        public IEnumerator TakeTurn(Context context)
        {
            if (_intent == null) yield break;

            FXPlayer.Instance.PlayMonsterAttack();

            yield return _intent.Animate(context);
            _intent.Play(context);

            Destroy(_intent.gameObject);
        }
    }
}
