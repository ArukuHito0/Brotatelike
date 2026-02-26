using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SelectableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent pointerEnter;
    [SerializeField] private UnityEvent pointerExit;
    [SerializeField] private UnityEvent pointerClick;
    [SerializeField] private UnityEvent pointerDown;
    [SerializeField] private UnityEvent pointerUp;

    public void OnPointerEnter(PointerEventData eventData) { pointerEnter?.Invoke(); }
    public void OnPointerExit(PointerEventData eventData) { pointerExit?.Invoke(); }
    public void OnPointerClick(PointerEventData eventData) { pointerClick?.Invoke(); }
    public void OnPointerDown(PointerEventData eventData) { pointerDown?.Invoke(); }
    public void OnPointerUp(PointerEventData eventData) { pointerUp?.Invoke(); }
}
