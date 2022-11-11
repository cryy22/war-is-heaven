using UnityEngine;

namespace Cards
{
    public class PlayerHand : MonoBehaviour
    {
        [SerializeField] private Transform Container;

        public void AddCard(Card card) { card.transform.SetParent(parent: Container, worldPositionStays: false); }
    }
}
