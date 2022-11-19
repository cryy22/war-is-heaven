using Crysc.Initialization;
using UnityEngine;

namespace WarIsHeaven.Cards
{
    [CreateAssetMenu(fileName = "Card Factory", menuName = "Factories/Card")]
    public class CardFactory : InitializationFactory<Card, CardConfig>
    { }
}
