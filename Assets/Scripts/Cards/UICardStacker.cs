using UnityEngine;

namespace WarIsHeaven.Cards
{
    [ExecuteAlways]
    public class UICardStacker : MonoBehaviour
    {
        private void OnTransformChildrenChanged()
        {
            foreach (Transform child in transform)
            {
                var card = child.GetComponent<Card>();
                if (card == null) return;

                child.localPosition = Vector3.zero;
                child.localRotation = Quaternion.identity;
                child.localScale = Vector3.one;
            }
        }
    }
}
