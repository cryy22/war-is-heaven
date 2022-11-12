using System.Collections.Generic;
using Cards;
using UnityEngine;

namespace UI
{
    [ExecuteAlways]
    public class UICardSpreader : MonoBehaviour
    {
        [SerializeField] private float MaxWidth = 8f;
        [SerializeField] private float MaxSpacing = 2f;

        private static readonly Vector3 _zMod = new(x: 0, y: 0, z: -0.001f);

        private void OnTransformChildrenChanged()
        {
            List<Card> cards = new();

            foreach (Transform child in transform)
            {
                var card = child.GetComponent<Card>();
                if (card != null) cards.Add(card);
            }

            int spacingCount = Mathf.Max(a: cards.Count - 1, b: 0);
            float spacing = spacingCount > 0 ? Mathf.Min(a: MaxSpacing, b: MaxWidth / spacingCount) : 0f;
            float width = spacing * spacingCount;

            transform.localPosition = Vector3.left * (width / 2);
            for (var i = 0; i < cards.Count; i++)
            {
                Transform cardTransform = cards[i].transform;

                cardTransform.localPosition = (Vector3.right * (i * spacing)) + (_zMod * i);
                cardTransform.localRotation = Quaternion.identity;
                cardTransform.localScale = Vector3.one;
            }
        }
    }
}
