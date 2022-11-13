using System;
using TMPro;
using UnityEngine;

namespace WarIsHeaven.Killables
{
    [RequireComponent(typeof(Killable))]
    public class UIKillableText : MonoBehaviour
    {
        private const string _valuePlaceholder = "K_VALUE";
        private const string _maxValuePlaceholder = "K_MAX_VALUE";

        [SerializeField] private string TextTemplate = "Value: K_VALUE / K_MAX_VALUE";

        private TMP_Text _text;
        private Killable _killable;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
            _killable = GetComponent<Killable>();
        }

        private void Start() { UpdateText(); }

        private void OnEnable() { _killable.Changed += ChangedEventHandler; }
        private void OnDisable() { _killable.Changed -= ChangedEventHandler; }

        private void ChangedEventHandler(object sender, EventArgs _) { UpdateText(); }

        private void UpdateText()
        {
            _text.text = TextTemplate
                .Replace(oldValue: _valuePlaceholder, newValue: _killable.Value.ToString())
                .Replace(oldValue: _maxValuePlaceholder, newValue: _killable.InitialValue.ToString());
        }
    }
}
