using UnityEngine;

namespace WarIsHeaven.Units
{
    [CreateAssetMenu(fileName = "New UnitConfig", menuName = "Configs/Unit")]
    public class UnitConfig : ScriptableObject
    {
        public int InitialHealth;
    }
}
