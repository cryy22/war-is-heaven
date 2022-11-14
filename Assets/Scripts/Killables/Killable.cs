using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WarIsHeaven.Killables
{
    [RequireComponent(typeof(Collider2D))]
    public class Killable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private GameObject Indicator;

        [SerializeField] private int InitialValueConfig = 1;

        public string DisplayNameOverride;

        private bool _isInitialized;

        public event EventHandler Hovered;
        public event EventHandler Unhovered;
        public event EventHandler Clicked;
        public event EventHandler<ChangedEventArgs> Changed;
        public event EventHandler Killed;

        public string DisplayName => string.IsNullOrEmpty(DisplayNameOverride) ? name : DisplayNameOverride;

        public int InitialValue { get; private set; }
        public int Value { get; private set; }

        private void Awake()
        {
            DisplayIndicator(false);
            InitialValue = InitialValueConfig;
            Value = InitialValue;

            if (KillableRegistry.Instance != null) KillableRegistry.Instance.Register(this);
        }

        private void OnDisable() { Unhovered?.Invoke(sender: this, e: EventArgs.Empty); }

        public void Initialize(int initialValue)
        {
            if (_isInitialized) throw new InvalidOperationException("Killable is already initialized");

            InitialValue = initialValue;
            Value = InitialValue;

            _isInitialized = true;
        }

        public void UpdateMaxValue(int delta)
        {
            InitialValue += delta;
            ChangeValue(delta);
        }

        public void DisplayIndicator(bool display)
        {
            if (Indicator != null) Indicator.SetActive(display);
        }

        public void ChangeValue(int delta)
        {
            Value = Mathf.Min(a: Value + delta, b: InitialValue);

            Changed?.Invoke(sender: this, e: new ChangedEventArgs(delta));
            if (Value <= 0) Killed?.Invoke(sender: this, e: EventArgs.Empty);
        }

        public void OnPointerDown(PointerEventData _) { Clicked?.Invoke(sender: this, e: EventArgs.Empty); }
        public void OnPointerEnter(PointerEventData _) { Hovered?.Invoke(sender: this, e: EventArgs.Empty); }
        public void OnPointerExit(PointerEventData _) { Unhovered?.Invoke(sender: this, e: EventArgs.Empty); }
    }
}
