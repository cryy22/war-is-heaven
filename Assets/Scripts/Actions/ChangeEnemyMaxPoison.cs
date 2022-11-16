using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Actions
{
    [CreateAssetMenu(
        fileName = "ChangeEnemyMaxPoison",
        menuName = "Actions/Change Enemy Max Poison"
    )]
    public class ChangeEnemyMaxPoison : Action
    {
        public override void Invoke(Context context, int magnitude = 1)
        {
            if (magnitude > 0)
                FXPlayer.Instance.PlayHealSound();
            else if (magnitude < 0)
                FXPlayer.Instance.PlayGunshot();

            context.EnemyUnit.Poisonous.UpdateMaxValue(magnitude);
        }
    }
}
