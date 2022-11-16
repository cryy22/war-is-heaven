using System.Collections.Generic;
using UnityEngine;
using WarIsHeaven.Actions;

namespace WarIsHeaven.Cards
{
    [CreateAssetMenu(fileName = "New Card Config", menuName = "Configs/Card")]
    public class CardConfig : ScriptableObject
    {
        public string Title;
        public int MannaCost;
        [TextArea] public string Description;
        public List<ActionMagnitude> ActionMagnitudes;
    }
}
