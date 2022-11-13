using System.Collections.Generic;
using UnityEngine;

namespace WarIsHeaven.Cards
{
    using CardsPositions = Dictionary<Card, Vector3>;

    [CreateAssetMenu(fileName = "CardStackDatasource", menuName = "Scriptable Objects/Datasources/Card Stack")]
    public class UICardStackDatasource : UICardPositionDatasource
    {
        public override CardsPositions CalculateCardsPositions(List<Card> cards)
        {
            var cardsPositions = new CardsPositions();
            for (var i = 0; i < cards.Count; i++) cardsPositions[cards[i]] = LayeringModifier * -i;
            return cardsPositions;
        }
    }
}
