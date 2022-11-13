using System;
using TMPro;
using UnityEngine;
using WarIsHeaven.Killables;

namespace WarIsHeaven.Units
{
    [RequireComponent(typeof(Killable))]
    public class UIAttackValue : MonoBehaviour
    {
        [SerializeField] private TMP_Text AttackText;
        private Killable _killable;

        private void Awake()
        {
            _killable = GetComponent<Killable>();
            UpdateAttackText();
        }

        private void OnEnable() { _killable.Damaged += DamagedEventHandler; }
        private void OnDisable() { _killable.Damaged -= DamagedEventHandler; }

        private void DamagedEventHandler(object sender, EventArgs e) { UpdateAttackText(); }
        private void UpdateAttackText() { AttackText.text = $"Attack: {_killable.Value.ToString()} dmg"; }
    }
}
