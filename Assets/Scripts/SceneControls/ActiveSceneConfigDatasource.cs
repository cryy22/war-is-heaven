using System;
using System.Collections.Generic;
using UnityEngine;

namespace WarIsHeaven.SceneControls
{
    [CreateAssetMenu(fileName = "ActiveSceneConfigDatasource", menuName = "Datasources/Active Scene Config")]
    public class ActiveSceneConfigDatasource : ScriptableObject
    {
        [SerializeField] private List<CombatSceneConfig> CombatSceneConfigs;

        [NonSerialized] private int _combatIndex;

        public int Count => CombatSceneConfigs.Count;

        public CombatSceneConfig ActiveCombatConfig =>
            _combatIndex < CombatSceneConfigs.Count ? CombatSceneConfigs[_combatIndex] : null;

        public void IncrementCombatConfig() { _combatIndex++; }
        public void SetCombatConfigIndex(int index) { _combatIndex = index; }
    }
}
