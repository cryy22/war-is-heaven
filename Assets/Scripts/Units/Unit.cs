using System;
using UnityEngine;
using WarIsHeaven.Killables;
using WarIsHeaven.StatusEffects;

namespace WarIsHeaven.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Killable HealthComponent;
        [SerializeField] private Transform StatsContainer;

        private bool _isInitialized;

        public Killable Health => HealthComponent;
        public PoisonedStatus PoisonedStatus { get; private set; }

        public void Initialize(UnitConfig config)
        {
            if (_isInitialized) throw new InvalidOperationException("Unit is already initialized");
            _isInitialized = true;

            HealthComponent.Initialize(config.InitialHealth);
        }

        public void AddPoisonedStatus(PoisonedStatus status)
        {
            PoisonedStatus = status;
            status.transform.SetParent(parent: StatsContainer, worldPositionStays: false);
        }
    }
}
