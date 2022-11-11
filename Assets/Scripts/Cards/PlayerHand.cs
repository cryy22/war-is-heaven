using System;
using UnityEngine;

namespace Cards
{
    public class PlayerHand : MonoBehaviour
    {
        [SerializeField] private Transform Container;

        private static readonly Vector3 _hoverCardPositionDelta = new(x: 0, y: 0.66f, z: -0.1f);
        private Vector3 _hoverCardNormalPosition;

        public void AddCard(Card card)
        {
            card.MouseEntered += MouseEnteredEventHandler;
            card.transform.SetParent(parent: Container, worldPositionStays: false);
        }

        private void MouseEnteredEventHandler(object sender, EventArgs _)
        {
            var card = (Card) sender;
            card.MouseExited += MouseExitedEventHandler;

            _hoverCardNormalPosition = card.transform.localPosition;
            card.transform.localPosition += _hoverCardPositionDelta;
        }

        private void MouseExitedEventHandler(object sender, EventArgs _)
        {
            var card = (Card) sender;
            card.MouseExited -= MouseExitedEventHandler;

            card.transform.localPosition = _hoverCardNormalPosition;
        }
    }
}
