using System;
using UnityEngine;

namespace WarIsHeaven.GameResources
{
    [CreateAssetMenu(fileName = "New MannaPool", menuName = "Resources/Manna Pool")]
    public class MannaPool : ScriptableObject
    {
        public event EventHandler MannaChanged;

        [field: SerializeField] public int MannaPerTurn { get; private set; }
        [field: NonSerialized] public int Manna { get; private set; }

        public void ResetManna()
        {
            Manna = MannaPerTurn;
            MannaChanged?.Invoke(sender: this, e: EventArgs.Empty);
        }

        public void SpendManna(int amount)
        {
            Manna -= amount;
            MannaChanged?.Invoke(sender: this, e: EventArgs.Empty);
        }
    }
}
