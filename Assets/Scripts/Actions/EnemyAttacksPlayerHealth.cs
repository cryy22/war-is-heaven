using System.Collections;
using Crysc.Helpers;
using UnityEngine;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Actions
{
    [CreateAssetMenu(
        fileName = "EnemyAttacksPlayerHealth",
        menuName = "Actions/Enemy Attacks Player Health"
    )]
    public class EnemyAttacksPlayerHealth : Action
    {
        public override void Invoke(Context context, int magnitude = 1)
        {
            magnitude *= context.EnemyUnit.Attack.Value * -1;

            if (magnitude > 0) FXPlayer.I.PlayHealSound();
            else if (magnitude < 0) FXPlayer.I.PlayGunshot();

            context.PlayerUnit.Health.ChangeValue(magnitude);
        }

        public override IEnumerator Animate(Context context)
        {
            yield return base.Animate(context);

            Transform enemyTransform = context.EnemyUnit.transform;
            Vector3 initialPosition = enemyTransform.position;

            yield return Mover.Move(
                transform: enemyTransform,
                end: context.PlayerUnit.Health.transform.position,
                duration: 0.125f
            );
            enemyTransform.position = initialPosition;
        }
    }
}
