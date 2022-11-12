using System;
using TMPro;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private TMP_Text HealthText;

        [SerializeField] public int AttackDamage = 1;
        [SerializeField] private int MaxHealth = 5;

        private int _health;

        public event EventHandler MouseClicked;

        private void Awake()
        {
            _health = MaxHealth;
            UpdateHealthText();
        }

        private void OnMouseDown() { MouseClicked?.Invoke(sender: this, e: EventArgs.Empty); }

        public void Attack(Unit target) { target.TakeDamage(AttackDamage); }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            UpdateHealthText();
        }

        private void UpdateHealthText() { HealthText.text = $"Health: {_health.ToString()} / {MaxHealth.ToString()}"; }
    }
}
