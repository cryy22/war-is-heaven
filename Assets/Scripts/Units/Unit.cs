using UnityEngine;
using WarIsHeaven.Killables;
using WarIsHeaven.StatusEffects;

namespace WarIsHeaven.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Killable HealthComponent;
        [SerializeField] private Transform StatsContainer;

        public Killable Health => HealthComponent;
        public PoisonedStatus PoisonedStatus { get; private set; }

        public void AddPoisonedStatus(PoisonedStatus status)
        {
            PoisonedStatus = status;
            status.transform.SetParent(parent: StatsContainer, worldPositionStays: false);
        }
    }
}
