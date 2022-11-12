using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WarIsHeaven.Killables
{
    public class Killable : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject Indicator;
        [SerializeField] private int InitialValue = 1;

        public event EventHandler Clicked;
        public event EventHandler Damaged;
        public event EventHandler Killed;

        public int Value { get; private set; }

        private void Awake()
        {
            Indicator.SetActive(false);
            Value = InitialValue;

            if (KillableRegistry.Instance != null) KillableRegistry.Instance.Register(this);
        }

        public void DisplayIndicator(bool display)
        {
            if (Indicator != null) Indicator.SetActive(display);
        }

        public void TakeDamage(int damage)
        {
            Value -= damage;
            Damaged?.Invoke(sender: this, e: EventArgs.Empty);

            if (Value <= 0) Killed?.Invoke(sender: this, e: EventArgs.Empty);
        }

        public void OnPointerDown(PointerEventData _) { Clicked?.Invoke(sender: this, e: EventArgs.Empty); }
    }
}
