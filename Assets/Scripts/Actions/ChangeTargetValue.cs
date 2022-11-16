using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Actions
{
    [CreateAssetMenu(
        fileName = "New ChangeTargetValue",
        menuName = "Actions/Change Target Value"
    )]
    public class ChangeTargetValue : Action
    {
        public override void Invoke(Context context, int magnitude = 1)
        {
            if (magnitude > 0) FXPlayer.Instance.PlayHealSound();
            else if (magnitude < 0) FXPlayer.Instance.PlayGunshot();

            context.Target.ChangeValue(magnitude);
        }
    }
}
