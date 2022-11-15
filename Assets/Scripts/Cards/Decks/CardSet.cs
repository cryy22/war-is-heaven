using System.Collections.Generic;
using UnityEngine;

namespace WarIsHeaven.Cards.Decks
{
    [CreateAssetMenu(fileName = "New CardSet", menuName = "Configs/Card Set")]
    public class CardSet : ScriptableObject
    {
        public List<CardQuantity> CardQuantities;
    }
}
