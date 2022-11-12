using System.Collections.Generic;
using UnityEngine;
using WarIsHeaven.Cards.CardActions;

namespace WarIsHeaven.Cards
{
    [CreateAssetMenu(fileName = "New Card Config", menuName = "Scriptable Objects/Configs/Card")]
    public class CardConfig : ScriptableObject
    {
        public string Title;
        public int MannaCost;
        [TextArea] public string Description;
        public List<CardAction> Actions;
    }
}
