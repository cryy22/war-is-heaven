using System;
using UnityEngine;

namespace Manna
{
    [CreateAssetMenu(fileName = "New MannaPool", menuName = "Scriptable Objects/Resources/Manna Pool")]
    public class MannaPool : ScriptableObject
    {
        [field: SerializeField] public int MannaPerTurn { get; private set; }

        [field: NonSerialized] public int Amount { get; private set; }

        public void ResetManna() { Amount = MannaPerTurn; }
        public void SpendManna(int amount) { Amount -= amount; }

        public string GetMannaString() { return $"{Amount.ToString()} / {MannaPerTurn.ToString()}"; }
    }
}
