using Crysc.UI;
using TMPro;
using UnityEngine;

namespace WarIsHeaven.Intents
{
    public class UIIntentTooltip : UITooltip<Intent>
    {
        [SerializeField] private TMP_Text TitleText;
        [SerializeField] private TMP_Text DescriptionText;

        protected override void ShowTooltip(Intent intent)
        {
            base.ShowTooltip(intent);

            TitleText.text = intent.Title;
            DescriptionText.text = intent.Description;
        }
    }
}
