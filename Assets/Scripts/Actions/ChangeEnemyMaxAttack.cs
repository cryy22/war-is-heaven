using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Actions
{
    [CreateAssetMenu(
        fileName = "ChangeEnemyMaxAttack",
        menuName = "Actions/Change Enemy Max Attack"
    )]
    public class ChangeEnemyMaxAttack : Action
    {
        public override bool CanBeInvoked(Context context, int magnitude = 1)
        {
            return context.EnemyUnit != null && context.EnemyUnit.Attack != null;
        }

        public override void Invoke(Context context, int magnitude = 1)
        {
            if (magnitude > 0)
                FXPlayer.Instance.PlayHealSound();
            else if (magnitude < 0)
                FXPlayer.Instance.PlayGunshot();

            context.EnemyUnit.Attack.UpdateMaxValue(magnitude);
        }
    }
}
