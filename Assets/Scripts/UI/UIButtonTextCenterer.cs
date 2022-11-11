using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonTextCenterer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TMP_Text Text;
    [SerializeField] private Vector3 OnPressOffset = new Vector3(0, -2, 0);
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Text.transform.position += OnPressOffset;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        Text.transform.position -= OnPressOffset;
    }
}
