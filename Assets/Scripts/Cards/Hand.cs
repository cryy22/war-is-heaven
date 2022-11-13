using System;
using UnityEngine;
using WarIsHeaven.GameResources;

namespace WarIsHeaven.Cards
{
    public class Hand : Deck
    {
        [SerializeField] private MannaPool MannaPool;

        private static readonly Vector3 _hoverCardPositionDelta = new(x: 0, y: 0.66f, z: -0.1f);
        private static readonly Vector3 _selectedCardPositionDelta = new(x: 0, y: 2.66f, z: -0.1f);
        private static readonly Vector3 _selectedCardContainerPositionDelta = new(x: 0, y: -2f, z: 0);

        private Card _activeCard;
        private Vector3 _activeCardNormalPosition;
        private Vector3 _containerNormalPosition;

        public event EventHandler CardSelected;
        public event EventHandler CardDeselected;

        public Card SelectedCard { get; private set; }

        private void OnEnable()
        {
            foreach (Card card in Cards) card.MouseEntered += MouseEnteredEventHandler;
            if (_activeCard) SubscribeToActiveCard();
        }

        private void OnDisable()
        {
            foreach (Card card in Cards) card.MouseEntered -= MouseEnteredEventHandler;
            if (_activeCard) UnsubscribeFromActiveCard();
        }

        public override void AddCard(Card card)
        {
            base.AddCard(card);
            card.MouseEntered += MouseEnteredEventHandler;
        }

        public override bool RemoveCard(Card card)
        {
            bool result = base.RemoveCard(card);
            if (!result) return false;

            if (card == _activeCard) ResetSelections();
            card.MouseEntered -= MouseEnteredEventHandler;
            return true;
        }

        public void ResetSelections()
        {
            if (SelectedCard != null) UnsetSelectedCard();
            if (_activeCard != null) UnsetActiveCard();
        }

        private void MouseEnteredEventHandler(object sender, EventArgs _)
        {
            if (SelectedCard != null) return;
            SetActiveCard((Card) sender);
        }

        private void MouseClickedEventHandler(object sender, EventArgs _)
        {
            if (SelectedCard != null) UnsetSelectedCard();
            else if (_activeCard.MannaCost <= MannaPool.Amount) SetSelectedCard();
        }

        private void MouseExitedEventHandler(object sender, EventArgs _)
        {
            if (SelectedCard != null) return;
            UnsetActiveCard();
        }

        private void SetActiveCard(Card card)
        {
            _activeCard = card;

            SubscribeToActiveCard();
            _activeCardNormalPosition = _activeCard.transform.localPosition;
            _activeCard.transform.localPosition += _hoverCardPositionDelta;
        }

        private void UnsetActiveCard()
        {
            UnsubscribeFromActiveCard();
            _activeCard.transform.localPosition = _activeCardNormalPosition;

            _activeCard = null;
        }

        private void SetSelectedCard()
        {
            SelectedCard = _activeCard;
            SelectedCard.SetSelected(true);
            SelectedCard.transform.localPosition = _activeCardNormalPosition + _selectedCardPositionDelta;

            _containerNormalPosition = Container.transform.localPosition;
            Container.transform.localPosition += _selectedCardContainerPositionDelta;

            CardSelected?.Invoke(sender: this, e: EventArgs.Empty);
        }

        private void UnsetSelectedCard()
        {
            SelectedCard.SetSelected(false);
            SelectedCard.transform.localPosition = _activeCardNormalPosition + _hoverCardPositionDelta;

            Container.localPosition = _containerNormalPosition;

            SelectedCard = null;

            CardDeselected?.Invoke(sender: this, e: EventArgs.Empty);
        }

        private void SubscribeToActiveCard()
        {
            _activeCard.MouseExited += MouseExitedEventHandler;
            _activeCard.MouseClicked += MouseClickedEventHandler;
        }

        private void UnsubscribeFromActiveCard()
        {
            _activeCard.MouseExited -= MouseExitedEventHandler;
            _activeCard.MouseClicked -= MouseClickedEventHandler;
        }
    }
}
