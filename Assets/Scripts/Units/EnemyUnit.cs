using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using WarIsHeaven.Audio;
using WarIsHeaven.Cards.CardActions;
using WarIsHeaven.Intents;
using WarIsHeaven.Killables;
using Random = UnityEngine.Random;

namespace WarIsHeaven.Units
{
    public class EnemyUnit : Unit
    {
        [SerializeField] private Killable AttackComponent;
        [SerializeField] private Killable PoisonousComponent;

        [SerializeField] private IntentFactory IntentFactory;
        [SerializeField] private Transform IntentContainer;

        [SerializeField] private List<IntentConfig> AttackIntentConfigs;
        [SerializeField] private List<IntentConfig> PoisonousIntentConfigs;
        [SerializeField] private List<IntentConfig> OtherIntentConfigs;

        private Intent _intent;

        public event EventHandler Neutralized;

        public Killable Attack => AttackComponent;
        public Killable Poisonous => PoisonousComponent;

        private bool IsNeutralized => AttackComponent == null && PoisonousComponent == null;

        private void OnEnable()
        {
            if (Attack != null) Attack.Killed += KilledEventHandler;
            if (Poisonous != null) Poisonous.Killed += KilledEventHandler;
        }

        private void OnDisable()
        {
            if (Attack != null) Attack.Killed -= KilledEventHandler;
            if (Poisonous != null) Poisonous.Killed -= KilledEventHandler;
        }

        public void CreateIntent()
        {
            if (_intent != null) Destroy(_intent.gameObject);
            IntentConfig config = GetRandomAvailableIntentConfig();

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

        private IntentConfig GetRandomAvailableIntentConfig()
        {
            HashSet<IntentConfig> availableConfigs = new();
            if (AttackComponent != null) availableConfigs.AddRange(AttackIntentConfigs);
            if (PoisonousComponent != null) availableConfigs.AddRange(PoisonousIntentConfigs);
            availableConfigs.AddRange(OtherIntentConfigs);

            return availableConfigs.ElementAt(Random.Range(minInclusive: 0, maxExclusive: availableConfigs.Count));
        }

        private void KilledEventHandler(object sender, EventArgs _)
        {
            var killable = (Killable) sender;
            Destroy(killable.gameObject);

            StartCoroutine(CheckForNeutralized());
        }

        private IEnumerator CheckForNeutralized()
        {
            yield return new WaitForEndOfFrame();
            if (IsNeutralized) Neutralized?.Invoke(sender: this, e: EventArgs.Empty);
        }
    }
}
