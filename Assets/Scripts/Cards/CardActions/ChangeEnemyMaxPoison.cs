using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Cards.CardActions
{
    [CreateAssetMenu(
        fileName = "ChangeEnemyMaxPoison",
        menuName = "Card Actions/Change Enemy Max Poison"
    )]
    public class ChangeEnemyMaxPoison : CardAction
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
