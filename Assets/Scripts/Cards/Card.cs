using System;
using System.Collections.Generic;
using Cards.CardActions;
using TMPro;
using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private GameObject MannaBadge;
        [SerializeField] private GameObject Content;
        [SerializeField] private TMP_Text MannaCostText;
        [SerializeField] private TMP_Text TitleText;
        [SerializeField] private TMP_Text DescriptionText;

        [SerializeField] private Sprite CardFront;
        [SerializeField] private Sprite CardFrontSelected;
        [SerializeField] private Sprite CardBack;

        private bool _isInitialized;
        private SpriteRenderer _renderer;
        private bool _isSelected;
        private List<CardAction> _actions;

        public event EventHandler MouseEntered;
        public event EventHandler MouseExited;
        public event EventHandler MouseClicked;

        public int MannaCost { get; private set; }

        private SideType Side { get; set; } = SideType.Back;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            UpdateSidePresentation();
        }

        public void OnMouseDown() { MouseClicked?.Invoke(sender: this, e: EventArgs.Empty); }
        public void OnMouseEnter() { MouseEntered?.Invoke(sender: this, e: EventArgs.Empty); }
        public void OnMouseExit() { MouseExited?.Invoke(sender: this, e: EventArgs.Empty); }

        public void Initialize(CardConfig config)
        {
            if (_isInitialized) throw new Exception("Card is already initialized");

            _actions = config.Actions;
            MannaCost = config.MannaCost;

            TitleText.text = config.Title;
            DescriptionText.text = config.Description;
            MannaCostText.text = config.MannaCost.ToString();

            _isInitialized = true;
        }

        public void Play(CardAction.Context context)
        {
            foreach (CardAction action in _actions) action.Invoke(context);
        }

        public void Flip(SideType side)
        {
            Side = side;
            UpdateSidePresentation();
        }

        public void SetSelected(bool isSelected)
        {
            _isSelected = isSelected;
            UpdateSidePresentation();
        }

        private void UpdateSidePresentation()
        {
            switch (Side)
            {
                case SideType.Front:
                    _renderer.sprite = _isSelected ? CardFrontSelected : CardFront;
                    MannaBadge.SetActive(true);
                    Content.SetActive(true);
                    break;
                case SideType.Back:
                default:
                    _renderer.sprite = CardBack;
                    MannaBadge.SetActive(false);
                    Content.SetActive(false);
                    break;
            }
        }

        public enum SideType
        {
            Front,
            Back,
        }
    }
}
