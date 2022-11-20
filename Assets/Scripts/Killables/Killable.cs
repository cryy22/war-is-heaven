using System;
using Crysc.Initialization;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WarIsHeaven.Killables
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(KillableRegistrar))]
    public class Killable : InitializationBehaviour<int>,
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private int DefaultInitialValue = 1;
        [SerializeField] private UIKillableIndicator Indicator;

        public string DisplayNameOverride;

        private bool _isHovered;
        private bool _isIndicatorActive;
        private bool _hasIndicator;

        public event EventHandler<ChangedEventArgs> Changed;
        public event EventHandler Killed;

        public string DisplayName => string.IsNullOrEmpty(DisplayNameOverride) ? name : DisplayNameOverride;

        public int InitialValue { get; private set; }
        public int Value { get; private set; }

        private void Awake()
        {
            _hasIndicator = Indicator != null;
            SetIndicatorActive(false);

            if (!IsInitialized)
            {
                InitialValue = DefaultInitialValue;
                Value = InitialValue;
                if (_hasIndicator) Indicator.SetValue(InitialValue);
            }
        }

        private void Update()
        {
            if (_hasIndicator) Indicator.gameObject.SetActive(_isIndicatorActive || _isHovered);
        }

        public override void Initialize(int initialValue)
        {
            base.Initialize(initialValue);

            InitialValue = initialValue;
            Value = InitialValue;

            if (_hasIndicator) Indicator.SetValue(InitialValue);
        }

        public void UpdateMaxValue(int delta)
        {
            InitialValue += delta;
            ChangeValue(delta);
        }

        public void SetIndicatorActive(bool isActive) { _isIndicatorActive = isActive; }

        public void ChangeValue(int delta)
        {
            Value = Mathf.Min(a: Value + delta, b: InitialValue);
            if (_hasIndicator) Indicator.SetValue(Value);

            Changed?.Invoke(sender: this, e: new ChangedEventArgs(delta));
            if (Value <= 0) Killed?.Invoke(sender: this, e: EventArgs.Empty);
        }

        public void OnPointerEnter(PointerEventData _) { _isHovered = true; }
        public void OnPointerExit(PointerEventData _) { _isHovered = false; }
    }
}
