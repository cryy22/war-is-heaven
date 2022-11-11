using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "New Card Factory", menuName = "Scriptable Objects/Factories/Card")]
    public class CardFactory : ScriptableObject
    {
        [SerializeField] private Card CardPrefab;

        public Card Create(CardConfig config)
        {
            Card card = Instantiate(CardPrefab);
            card.Initialize(config);

            return card;
        }
    }
}
