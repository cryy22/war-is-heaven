using TMPro;
using UnityEngine;

namespace Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private TMP_Text HealthText;
        [SerializeField] private int MaxHealth = 5;

        private int _health;

        private void Awake()
        {
            _health = MaxHealth;
            UpdateHealthText();
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            UpdateHealthText();
        }

        private void UpdateHealthText() { HealthText.text = $"Health: {_health.ToString()} / {MaxHealth.ToString()}"; }
    }
}
