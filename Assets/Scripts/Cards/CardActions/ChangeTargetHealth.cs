using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Cards.CardActions
{
    [CreateAssetMenu(
        fileName = "New ChangeTargetHealth",
        menuName = "Scriptable Objects/Card Actions/Change Target Health"
    )]
    public class ChangeTargetHealth : CardAction
    {
        [SerializeField] private int HealthChange;

        public override void Invoke(Context context)
        {
            if (HealthChange > 0) FXPlayer.Instance.PlayHealSound();
            else if (HealthChange < 0) FXPlayer.Instance.PlayGunshot();

            context.Target.ChangeHealth(HealthChange);
        }
    }
}
