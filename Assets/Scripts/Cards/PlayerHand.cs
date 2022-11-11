using System;
using UnityEngine;

namespace Cards
{
    public class PlayerHand : MonoBehaviour
    {
        [SerializeField] private Transform Container;

        private static readonly Vector3 _hoverCardPositionDelta = new(x: 0, y: 0.66f, z: -0.1f);
        private static readonly Vector3 _selectedCardPositionDelta = new(x: 0, y: 2.66f, z: -0.1f);
        private static readonly Vector3 _selectedCardContainerPositionDelta = new(x: 0, y: -2f, z: 0);

        private Card _activeCard;
        private Card _selectedCard;
        private Vector3 _activeCardNormalPosition;
        private Vector3 _containerNormalPosition;

        public void AddCard(Card card)
        {
            card.MouseEntered += MouseEnteredEventHandler;
            card.transform.SetParent(parent: Container, worldPositionStays: false);
        }

        private void MouseEnteredEventHandler(object sender, EventArgs _)
        {
            if (_selectedCard != null) return;

            _activeCard = (Card) sender;

            _activeCard.MouseExited += MouseExitedEventHandler;
            _activeCard.MouseClicked += MouseClickedEventHandler;
            _activeCardNormalPosition = _activeCard.transform.localPosition;
            _activeCard.transform.localPosition += _hoverCardPositionDelta;
        }

        private void MouseClickedEventHandler(object sender, EventArgs _)
        {
            if (_selectedCard != null) UnsetSelectedCard();
            else SetSelectedCard();
        }

        private void MouseExitedEventHandler(object sender, EventArgs _)
        {
            if (_selectedCard != null) return;

            _activeCard.MouseExited -= MouseExitedEventHandler;
            _activeCard.MouseClicked -= MouseClickedEventHandler;
            _activeCard.transform.localPosition = _activeCardNormalPosition;
            _activeCard = null;
        }

        private void SetSelectedCard()
        {
            _selectedCard = _activeCard;
            _selectedCard.SetSelected(true);
            _selectedCard.transform.localPosition = _activeCardNormalPosition + _selectedCardPositionDelta;

            _containerNormalPosition = Container.transform.localPosition;
            Container.transform.localPosition += _selectedCardContainerPositionDelta;
        }

        private void UnsetSelectedCard()
        {
            _selectedCard.SetSelected(false);
            _selectedCard.transform.localPosition = _activeCardNormalPosition + _hoverCardPositionDelta;

            Container.localPosition = _containerNormalPosition;

            _selectedCard = null;
        }
    }
}
