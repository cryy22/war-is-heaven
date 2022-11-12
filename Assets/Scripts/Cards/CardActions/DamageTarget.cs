using UnityEngine;

namespace WarIsHeaven.Cards.CardActions
{
    [CreateAssetMenu(fileName = "New Card Action", menuName = "Scriptable Objects/Card Actions/Damage Target")]
    public class DamageTarget : CardAction
    {
        [SerializeField] private int Amount;

        public override void Invoke(Context context) { context.Target.TakeDamage(Amount); }
    }
}
