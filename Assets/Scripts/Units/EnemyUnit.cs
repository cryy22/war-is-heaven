using UnityEngine;

namespace Units
{
    public class EnemyUnit : Unit
    {
        [SerializeField] private Killable.Killable AttackValue;
        public void Attack(Unit target) { target.TakeDamage(AttackValue.Value); }
    }
}
