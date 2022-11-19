using System;
using TMPro;
using UnityEngine;

namespace WarIsHeaven.Intents
{
    public class UIIntentTooltip : MonoBehaviour
    {
        [SerializeField] private GameObject Container;
        [SerializeField] private TMP_Text TitleText;
        [SerializeField] private TMP_Text DescriptionText;

        private Intent _activeIntent;

        private void OnEnable()
        {
            IntentRegistry.I.Hovered += HoveredEventHandler;
            IntentRegistry.I.Unhovered += UnhoveredEventHandler;
        }

        private void OnDisable()
        {
            IntentRegistry.I.Hovered -= HoveredEventHandler;
            IntentRegistry.I.Unhovered -= UnhoveredEventHandler;
        }

        private void ShowTooltip(Intent intent)
        {
            Container.SetActive(true);
            TitleText.text = intent.Title;
            DescriptionText.text = intent.Description;
        }

        private void HideTooltip() { Container.SetActive(false); }

        private void HoveredEventHandler(object sender, EventArgs _)
        {
            var intent = (Intent) sender;
            ShowTooltip(intent);
        }

        private void UnhoveredEventHandler(object sender, EventArgs _) { HideTooltip(); }
    }
}
