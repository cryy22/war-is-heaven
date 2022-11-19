using Crysc.Initialization;
using UnityEngine;
using WarIsHeaven.Killables;
using WarIsHeaven.StatusEffects;

namespace WarIsHeaven.Units
{
    public class Unit : InitializationBehaviour<UnitConfig>
    {
        [SerializeField] private Transform StatsContainer;
        [SerializeField] private Killable HealthComponent;

        public Killable Health => HealthComponent;
        public PoisonedStatus PoisonedStatus { get; private set; }

        public override void Initialize(UnitConfig initParams)
        {
            base.Initialize(initParams);
            HealthComponent.Initialize(initParams.InitialHealth);
        }

        public void AddPoisonedStatus(PoisonedStatus status)
        {
            PoisonedStatus = status;
            status.transform.SetParent(parent: StatsContainer, worldPositionStays: false);
        }
    }
}
