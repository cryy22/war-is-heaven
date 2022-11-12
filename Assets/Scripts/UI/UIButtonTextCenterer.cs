using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WarIsHeaven.UI
{
    [RequireComponent(typeof(Button))]
    public class UIButtonTextCenterer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private TMP_Text Text;
        [SerializeField] private Vector3 OnPressOffset = new(x: 0, y: -2, z: 0);

        public void OnPointerDown(PointerEventData eventData) { Text.transform.position += OnPressOffset; }

        public void OnPointerUp(PointerEventData eventData) { Text.transform.position -= OnPressOffset; }
    }
}
