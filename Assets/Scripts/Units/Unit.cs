using TMPro;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private TMP_Text HealthText;

        public int MaxHealth = 5;

        private int _health;

        private void Awake()
        {
            _health = MaxHealth;
            UpdateHealthText();
        }

        public void Attack(Unit target) { target.TakeDamage(); }

        private void TakeDamage()
        {
            _health -= 1;
            UpdateHealthText();
        }

        private void UpdateHealthText() { HealthText.text = $"Health: {_health.ToString()} / {MaxHealth.ToString()}"; }
    }
}
