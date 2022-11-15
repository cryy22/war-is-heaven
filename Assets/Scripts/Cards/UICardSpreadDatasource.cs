using System.Collections.Generic;
using UnityEngine;

namespace WarIsHeaven.Cards
{
    using CardsPositions = Dictionary<Card, Vector3>;

    [CreateAssetMenu(fileName = "CardSpreadDatasource", menuName = "Datasources/Card Spread")]
    public class UICardSpreadDatasource : UICardPositionDatasource
    {
        [SerializeField] private float MaxWidth = 8f;
        [SerializeField] private float MaxSpacing = 2f;

        public override CardsPositions CalculateCardsPositions(List<Card> cards)
        {
            CardsPositions cardsPositions = new();
            if (cards.Count == 0) return cardsPositions;

            int spacingCount = Mathf.Max(a: cards.Count - 1, b: 0);
            float spacing = spacingCount > 0
                ? Mathf.Min(a: MaxSpacing, b: MaxWidth / spacingCount)
                : 0f;

            Vector3 initialPosition = Vector3.left * ((spacing * spacingCount) / 2);
            Vector3 positionDelta = (Vector3.right * spacing) + LayeringModifier;

            for (var i = 0; i < cards.Count; i++) cardsPositions[cards[i]] = initialPosition + (positionDelta * i);

            return cardsPositions;
        }
    }
}
