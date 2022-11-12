using UnityEngine;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Units
{
    public class EnemyUnit : Unit
    {
        [SerializeField] private Killable AttackValue;
        public void Attack(Unit target) { target.TakeDamage(AttackValue.Value); }
    }
}
