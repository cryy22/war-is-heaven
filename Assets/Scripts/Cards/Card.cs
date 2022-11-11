using System;
using TMPro;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private GameObject Content;
        [SerializeField] private TMP_Text TitleText;
        [SerializeField] private TMP_Text DescriptionText;

        [SerializeField] private Sprite CardFront;
        [SerializeField] private Sprite CardFrontSelected;
        [SerializeField] private Sprite CardBack;

        private bool _isInitialized;
        private SpriteRenderer _renderer;
        private bool _isSelected;

        public event EventHandler MouseEntered;
        public event EventHandler MouseExited;

        private SideType Side { get; set; } = SideType.Back;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            UpdateSidePresentation();
        }

        public void OnMouseEnter() { MouseEntered?.Invoke(sender: this, e: EventArgs.Empty); }
        public void OnMouseExit() { MouseExited?.Invoke(sender: this, e: EventArgs.Empty); }

        public void Initialize(CardConfig config)
        {
            if (_isInitialized) throw new Exception("Card is already initialized");

            TitleText.text = config.Title;
            DescriptionText.text = config.Description;

            _isInitialized = true;
        }

        public void Flip()
        {
            Side = Side == SideType.Front ? SideType.Back : SideType.Front;
            UpdateSidePresentation();
        }

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            UpdateSidePresentation();
        }

        private void UpdateSidePresentation()
        {
            Content.SetActive(Side == SideType.Front);
            _renderer.sprite = Side == SideType.Front
                ? _isSelected ? CardFrontSelected : CardFront
                : CardBack;
        }

        private enum SideType
        {
            Front,
            Back,
        }
    }
}
