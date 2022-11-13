using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WarIsHeaven.UI
{
    public class UIFullscreenAnnouncePanel : MonoBehaviour
    {
        [SerializeField] private GameObject Container;
        [SerializeField] private Image Background;
        [SerializeField] private TMP_Text ContentText;

        private static readonly Color _goodColor = new Color32(r: 0x32, g: 0x73, b: 0x45, a: 0xDD);
        private static readonly Color _badColor = new Color32(r: 0xE5, g: 0x3B, b: 0x44, a: 0xDD);

        private void Awake() { Container.SetActive(false); }

        public IEnumerator DisplayMessage(string content, bool isGood, float duration = 2f)
        {
            Background.color = isGood ? _goodColor : _badColor;
            ContentText.text = content;
            Container.SetActive(true);

            yield return new WaitForSeconds(duration);

            Container.SetActive(false);
        }
    }
}
