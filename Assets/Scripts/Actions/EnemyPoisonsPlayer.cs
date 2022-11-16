using UnityEngine;
using WarIsHeaven.StatusEffects;

namespace WarIsHeaven.Actions
{
    [CreateAssetMenu(
        fileName = "EnemyPoisonsPlayer",
        menuName = "Actions/Enemy Poisons Player"
    )]
    public class EnemyPoisonsPlayer : Action
    {
        [SerializeField] private PoisonedStatus PoisonedStatusPrefab;

        public override void Invoke(Context context, int magnitude = 1)
        {
            magnitude *= context.EnemyUnit.Poisonous.Value;
            if (context.PlayerUnit.PoisonedStatus != null)
            {
                context.PlayerUnit.PoisonedStatus.ValueProvider.UpdateMaxValue(magnitude);
                return;
            }

            PoisonedStatus poisoned = Instantiate(PoisonedStatusPrefab);
            poisoned.Initialize(
                new StatusEffectConfig
                {
                    Target = context.PlayerUnit.Health,
                    InitialValue = magnitude,
                }
            );
            context.PlayerUnit.AddPoisonedStatus(poisoned);
        }
    }
}
