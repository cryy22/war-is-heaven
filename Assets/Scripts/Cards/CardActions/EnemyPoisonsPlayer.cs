using UnityEngine;
using WarIsHeaven.StatusEffects;

namespace WarIsHeaven.Cards.CardActions
{
    [CreateAssetMenu(
        fileName = "EnemyPoisonsPlayer",
        menuName = "Scriptable Objects/Card Actions/Enemy Poisons Player"
    )]
    public class EnemyPoisonsPlayer : CardAction
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
            poisoned.Initialize(target: context.PlayerUnit.Health, initialValue: magnitude);
            context.PlayerUnit.AddPoisonedStatus(poisoned);
        }
    }
}
