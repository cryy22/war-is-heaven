using TMPro;
using UnityEngine;

namespace WarIsHeaven.Killables
{
    public class UIKillableIndicator : MonoBehaviour
    {
        [SerializeField] private TMP_Text ValueText;

        public void SetValue(int value) { ValueText.text = value.ToString(); }
    }
}
