using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Actions
{
    [CreateAssetMenu(
        fileName = "ChangeEnemyHealth",
        menuName = "Actions/Change Enemy Health"
    )]
    public class ChangeEnemyHealth : Action
    {
        public override void Invoke(Context context, int magnitude = 1)
        {
            if (magnitude > 0) FXPlayer.I.PlayHealSound();
            else if (magnitude < 0) FXPlayer.I.PlayGunshot();

            context.EnemyUnit.Health.ChangeValue(magnitude);
        }
    }
}
