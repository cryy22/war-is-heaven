using TMPro;
using UnityEngine;

namespace WarIsHeaven.Units
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private TMP_Text HealthText;
        [SerializeField] private int MaxHealth = 5;

        public int Health { get; private set; }

        private void Awake()
        {
            Health = MaxHealth;
            UpdateHealthText();
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            UpdateHealthText();
        }

        private void UpdateHealthText() { HealthText.text = $"Health: {Health.ToString()} / {MaxHealth.ToString()}"; }
    }
}
