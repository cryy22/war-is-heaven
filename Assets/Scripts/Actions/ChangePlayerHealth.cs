using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Actions
{
    [CreateAssetMenu(
        fileName = "ChangePlayerHealth",
        menuName = "Actions/Change Player Health"
    )]
    public class ChangePlayerHealth : Action
    {
        public override void Invoke(Context context, int magnitude = 1)
        {
            if (magnitude > 0) FXPlayer.I.PlayHealSound();
            else if (magnitude < 0) FXPlayer.I.PlayGunshot();

            context.PlayerUnit.Health.ChangeValue(magnitude);
        }
    }
}
