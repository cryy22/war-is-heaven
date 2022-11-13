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

        private void OnEnable() { _killable.Changed += ChangedEventHandler; }
        private void OnDisable() { _killable.Changed -= ChangedEventHandler; }

        private void ChangedEventHandler(object sender, EventArgs e) { UpdateAttackText(); }
        private void UpdateAttackText() { AttackText.text = $"Attack: {_killable.Value.ToString()} dmg"; }
    }
}
