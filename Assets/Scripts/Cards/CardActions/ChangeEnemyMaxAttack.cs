using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Cards.CardActions
{
    [CreateAssetMenu(
        fileName = "ChangeEnemyMaxAttack",
        menuName = "Scriptable Objects/Card Actions/Change Enemy Max Attack"
    )]
    public class ChangeEnemyMaxAttack : CardAction
    {
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
