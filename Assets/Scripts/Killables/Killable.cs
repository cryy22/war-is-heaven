using System;
using UnityEngine;
using UnityEngine.EventSystems;
using WarIsHeaven.Audio;

namespace WarIsHeaven.Killables
{
    [RequireComponent(typeof(Collider2D))]
    public class Killable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private GameObject Indicator;
        [SerializeField] private int InitialValue = 1;
        public string DisplayNameOverride;

        public event EventHandler Hovered;
        public event EventHandler Unhovered;
        public event EventHandler Clicked;
        public event EventHandler Damaged;
        public event EventHandler Killed;

        public string DisplayName => string.IsNullOrEmpty(DisplayNameOverride) ? name : DisplayNameOverride;

        public int Value { get; private set; }

        private void Awake()
        {
            DisplayIndicator(false);
            Value = InitialValue;

            if (KillableRegistry.Instance != null) KillableRegistry.Instance.Register(this);
        }

        private void OnDisable() { Unhovered?.Invoke(sender: this, e: EventArgs.Empty); }

        public void DisplayIndicator(bool display)
        {
            if (Indicator != null) Indicator.SetActive(display);
        }

        public void TakeDamage(int damage)
        {
            Value -= damage;
            FXPlayer.Instance.PlayGunshot();

            Damaged?.Invoke(sender: this, e: EventArgs.Empty);
            if (Value <= 0) Killed?.Invoke(sender: this, e: EventArgs.Empty);
        }

        public void OnPointerDown(PointerEventData _) { Clicked?.Invoke(sender: this, e: EventArgs.Empty); }
        public void OnPointerEnter(PointerEventData _) { Hovered?.Invoke(sender: this, e: EventArgs.Empty); }
        public void OnPointerExit(PointerEventData _) { Unhovered?.Invoke(sender: this, e: EventArgs.Empty); }
    }
}
