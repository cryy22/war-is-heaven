using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WarIsHeaven.Actions;
using WarIsHeaven.Audio;
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
        private IntentConfig _lastIntentConfig;

        public event EventHandler Neutralized;

        public Killable Attack => AttackComponent;
        public Killable Poisonous => PoisonousComponent;

        private bool IsNeutralized => AttackComponent == null && PoisonousComponent == null;

        private void OnEnable()
        {
            if (Attack != null) Attack.Killed += ValueProviderKilledEventHandler;
            if (Poisonous != null) Poisonous.Killed += ValueProviderKilledEventHandler;

            if (_intent != null) _intent.Killable.Killed += IntentKilledEventHandler;
        }

        private void OnDisable()
        {
            if (Attack != null) Attack.Killed -= ValueProviderKilledEventHandler;
            if (Poisonous != null) Poisonous.Killed -= ValueProviderKilledEventHandler;

            if (_intent != null) _intent.Killable.Killed -= IntentKilledEventHandler;
        }

        public override void Initialize(UnitConfig config)
        {
            base.Initialize(config);

            var enemyConfig = config as EnemyUnitConfig;
            if (enemyConfig == null) return;

            if (enemyConfig.InitialAttack > 0)
                AttackComponent.Initialize(new KillableConfig(enemyConfig.InitialAttack));
            else Destroy(AttackComponent.gameObject);

            if (enemyConfig.InitialPoisonousness > 0)
                PoisonousComponent.Initialize(new KillableConfig(enemyConfig.InitialPoisonousness));
            else Destroy(PoisonousComponent.gameObject);
        }

        public void CreateIntent(bool excludeLastConfig = false)
        {
            if (_intent != null) Destroy(_intent.gameObject);
            IntentConfig config = GetRandomAvailableIntentConfig(excludeLastConfig);

            _intent = IntentFactory.Create(config);
            _intent.transform.SetParent(parent: IntentContainer, worldPositionStays: false);

            _intent.Killable.Killed += IntentKilledEventHandler;
            _lastIntentConfig = config;
        }

        public IEnumerator TakeTurn(Context context)
        {
            if (_intent == null) yield break;

            FXPlayer.I.PlayMonsterAttack();

            yield return _intent.Animate(context);
            _intent.Play(context);

            Destroy(_intent.gameObject);
        }

        private IntentConfig GetRandomAvailableIntentConfig(bool excludeLastConfig)
        {
            HashSet<IntentConfig> availableConfigs = new();
            if (AttackComponent != null) availableConfigs.UnionWith(AttackIntentConfigs);
            if (PoisonousComponent != null) availableConfigs.UnionWith(PoisonousIntentConfigs);
            availableConfigs.UnionWith(OtherIntentConfigs);

            if (excludeLastConfig && _lastIntentConfig != null) availableConfigs.Remove(_lastIntentConfig);

            return availableConfigs.ElementAt(Random.Range(minInclusive: 0, maxExclusive: availableConfigs.Count));
        }

        private void IntentKilledEventHandler(object sender, EventArgs _) { CreateIntent(excludeLastConfig: true); }

        private void ValueProviderKilledEventHandler(object sender, EventArgs _)
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
