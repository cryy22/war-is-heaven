using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Cards.CardActions
{
    [CreateAssetMenu(
        fileName = "ChangePlayerHealth",
        menuName = "Scriptable Objects/Card Actions/Change Player Health"
    )]
    public class ChangePlayerHealth : CardAction
    {
        public override void Invoke(Context context, int magnitude = 1)
        {
            if (magnitude > 0) FXPlayer.Instance.PlayHealSound();
            else if (magnitude < 0) FXPlayer.Instance.PlayGunshot();

            context.PlayerUnit.Health.ChangeValue(magnitude);
        }
    }
}
