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

        private static readonly Color _activeColor = Color.white;
        private static readonly Color _inactiveColor = new(r: 0.6862745f, g: 0.7490196f, b: 0.8235294f, a: 1f);

        private Killable _activeKillable;

        private void Awake() { UpdateTooltip(); }

        private void OnEnable()
        {
            KillableRegistry.Instance.Hovered += HoveredEventHandler;
            KillableRegistry.Instance.Unhovered += UnhoveredEventHandler;

            if (_activeKillable) _activeKillable.Damaged += DamagedEventHandler;
        }

        private void OnDisable()
        {
            KillableRegistry.Instance.Hovered -= HoveredEventHandler;
            KillableRegistry.Instance.Unhovered -= UnhoveredEventHandler;

            if (_activeKillable) _activeKillable.Damaged -= DamagedEventHandler;
        }

        private void HoveredEventHandler(object sender, EventArgs _)
        {
            _activeKillable = (Killable) sender;
            _activeKillable.Damaged += DamagedEventHandler;

            UpdateTooltip();
        }

        private void UnhoveredEventHandler(object sender, EventArgs _)
        {
            _activeKillable.Damaged -= DamagedEventHandler;
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
    }
}
