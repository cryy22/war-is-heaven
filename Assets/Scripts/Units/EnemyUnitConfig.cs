using UnityEngine;

namespace WarIsHeaven.Units
{
    [CreateAssetMenu(fileName = "New EnemyUnitConfig", menuName = "Scriptable Objects/Configs/Enemy Unit")]
    public class EnemyUnitConfig : UnitConfig
    {
        public int InitialAttack;
        public int InitialPoisonousness;
    }
}
