using UnityEngine;

namespace Cards.CardActions
{
    [CreateAssetMenu(fileName = "New Card Action", menuName = "Scriptable Objects/Card Actions/Damage Target")]
    public class DamageTarget : CardAction
    {
        [SerializeField] private int Amount;

        public override void Apply(Context context) { context.Target.TakeDamage(Amount); }
    }
}
