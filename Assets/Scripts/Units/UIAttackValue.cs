using System;
using TMPro;
using UnityEngine;

namespace Units
{
    [RequireComponent(typeof(Killable.Killable))]
    public class UIAttackValue : MonoBehaviour
    {
        [SerializeField] private TMP_Text AttackText;
        private Killable.Killable _killable;

        private void Awake() { _killable = GetComponent<Killable.Killable>(); }

        private void OnEnable() { _killable.Damaged += DamagedEventHandler; }
        private void OnDisable() { _killable.Damaged -= DamagedEventHandler; }

        private void DamagedEventHandler(object sender, EventArgs e) { UpdateAttackText(); }
        private void UpdateAttackText() { AttackText.text = $"Attack: {_killable.Value.ToString()} dmg"; }
    }
}
