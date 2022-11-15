using UnityEngine;

namespace WarIsHeaven.Units
{
    [CreateAssetMenu(fileName = "New UnitConfig", menuName = "Scriptable Objects/Configs/Unit")]
    public class UnitConfig : ScriptableObject
    {
        public int InitialHealth;
    }
}
