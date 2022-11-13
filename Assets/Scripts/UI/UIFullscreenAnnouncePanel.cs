using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WarIsHeaven.Constants;

namespace WarIsHeaven.UI
{
    public class UIFullscreenAnnouncePanel : MonoBehaviour
    {
        [SerializeField] private GameObject Container;
        [SerializeField] private Image Background;
        [SerializeField] private TMP_Text ContentText;

        private static readonly Color _goodColor = Colors.Green;
        private static readonly Color _badColor = Colors.Red;

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
