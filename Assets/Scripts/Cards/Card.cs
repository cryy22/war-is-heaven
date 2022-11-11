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
        [SerializeField] private Sprite CardBack;

        private static readonly Vector3 _hoverPositionDelta = new(x: 0, y: 0.66f, z: -0.1f);

        private bool _isInitialized;
        private SpriteRenderer _renderer;
        private Vector3 _normalPosition;

        private SideType Side { get; set; } = SideType.Back;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            UpdateSidePresentation();
        }

        public void OnMouseEnter()
        {
            _normalPosition = transform.position;
            transform.position += _hoverPositionDelta;
        }

        public void OnMouseExit() { transform.position = _normalPosition; }

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

        private void UpdateSidePresentation()
        {
            _renderer.sprite = Side == SideType.Front ? CardFront : CardBack;
            Content.SetActive(Side == SideType.Front);
        }

        private enum SideType
        {
            Front,
            Back,
        }
    }
}
