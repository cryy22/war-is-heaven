using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WarIsHeaven.Killables
{
    public class KillableTooltip : MonoBehaviour
    {
        [SerializeField] private TMP_Text KillableTitleText;
        [SerializeField] private TMP_Text KillableNameText;
        [SerializeField] private TMP_Text HealthText;
        [SerializeField] private Image Background;

        private static readonly Color _activeColor = new Color32(r: 0xFF, g: 0xE7, b: 0x62, a: 0xFF);
        private static readonly Color _inactiveColor = Color.white;

        private Killable _activeKillable;

        private void Awake() { UpdateTooltip(); }

        private void OnEnable()
        {
            KillableRegistry.Instance.Hovered += HoveredEventHandler;
            KillableRegistry.Instance.Unhovered += UnhoveredEventHandler;
            SubscribeToActiveKillable();
        }

        private void OnDisable()
        {
            KillableRegistry.Instance.Hovered -= HoveredEventHandler;
            KillableRegistry.Instance.Unhovered -= UnhoveredEventHandler;
            UnsubscribeFromActiveKillable();
        }

        private void HoveredEventHandler(object sender, EventArgs _)
        {
            _activeKillable = (Killable) sender;
            SubscribeToActiveKillable();
            UpdateTooltip();
        }

        private void UnhoveredEventHandler(object sender, EventArgs _)
        {
            UnsubscribeFromActiveKillable();
            _activeKillable = null;
            UpdateTooltip();
        }

        private void DamagedEventHandler(object sender, EventArgs _) { UpdateTooltip(); }

        private void UpdateTooltip()
        {
            if (_activeKillable != null)
            {
                KillableTitleText.text = "Killable!!";
                KillableNameText.text = _activeKillable.DisplayName;
                HealthText.text = $"Health: {_activeKillable.Value.ToString()}";
                Background.color = _activeColor;
            }
            else
            {
                KillableTitleText.text = "Killable...?";
                KillableNameText.text = "---";
                HealthText.text = "---";
                Background.color = _inactiveColor;
            }
        }

        private void SubscribeToActiveKillable()
        {
            if (_activeKillable == null) return;
            _activeKillable.Damaged += DamagedEventHandler;
        }

        private void UnsubscribeFromActiveKillable()
        {
            if (_activeKillable == null) return;
            _activeKillable.Damaged -= DamagedEventHandler;
        }
    }
}
