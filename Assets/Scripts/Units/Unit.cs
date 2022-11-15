using UnityEngine;
using WarIsHeaven.Common;
using WarIsHeaven.Killables;
using WarIsHeaven.StatusEffects;

namespace WarIsHeaven.Units
{
    public class Unit : InitializedBehaviour<UnitConfig>
    {
        [SerializeField] private Killable HealthComponent;
        [SerializeField] private Transform StatsContainer;

        public Killable Health => HealthComponent;
        public PoisonedStatus PoisonedStatus { get; private set; }

        public override void Initialize(UnitConfig config) { HealthComponent.Initialize(config.InitialHealth); }

        public void AddPoisonedStatus(PoisonedStatus status)
        {
            PoisonedStatus = status;
            status.transform.SetParent(parent: StatsContainer, worldPositionStays: false);
        }
    }
}
