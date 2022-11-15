using System.Collections.Generic;
using UnityEngine;
using WarIsHeaven.Cards.Decks;
using WarIsHeaven.Units;

namespace WarIsHeaven.SceneControls
{
    [CreateAssetMenu(fileName = "New CombatSceneConfig", menuName = "Configs/Combat Scene")]
    public class CombatSceneConfig : ScriptableObject
    {
        public List<CardSet> CardSets;
        public UnitConfig PlayerUnitConfig;
        public EnemyUnitConfig EnemyUnitConfig;
    }
}
