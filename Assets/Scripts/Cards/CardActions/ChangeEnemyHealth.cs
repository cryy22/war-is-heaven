using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Cards.CardActions
{
    [CreateAssetMenu(
        fileName = "New ChangeEnemyHealth",
        menuName = "Scriptable Objects/Card Actions/Change Enemy Health"
    )]
    public class ChangeEnemyHealth : CardAction
    {
        public override void Invoke(Context context, int magnitude = 1)
        {
            if (magnitude > 0) FXPlayer.Instance.PlayHealSound();
            else if (magnitude < 0) FXPlayer.Instance.PlayGunshot();

            context.EnemyUnit.Health.ChangeValue(magnitude);
        }
    }
}
