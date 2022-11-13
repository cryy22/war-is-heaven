using System.Collections.Generic;
using UnityEngine;

namespace WarIsHeaven.Cards
{
    using CardsPositions = Dictionary<Card, Vector3>;

    public abstract class UICardPositionDatasource : ScriptableObject
    {
        protected static readonly Vector3 LayeringModifier = new(x: 0, y: 0, z: -0.01f);

        public abstract CardsPositions CalculateCardsPositions(List<Card> cards);
    }
}
