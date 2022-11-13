using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Cards.CardActions
{
    [CreateAssetMenu(
        fileName = "New ChangeTargetValue",
        menuName = "Scriptable Objects/Card Actions/Change Target Value"
    )]
    public class ChangeTargetValue : CardAction
    {
        public override void Invoke(Context context, int magnitude = 1)
        {
            if (magnitude > 0) FXPlayer.Instance.PlayHealSound();
            else if (magnitude < 0) FXPlayer.Instance.PlayGunshot();

            context.Target.ChangeValue(magnitude);
        }
    }
}
