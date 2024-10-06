using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform draggableObject; // UI GameObject mà bạn muốn di chuyển
    public RectTransform panel; // UI Panel xác định vùng nhấn giữ

    private Vector2 originalPosition; // Vị trí ban đầu của UI GameObject
    private Vector2 pressPosition; // Vị trí ban đầu của UI GameObject

    private void Start()
    {
        // Lưu vị trí ban đầu của UI GameObject
        originalPosition = draggableObject.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Khi nhấn giữ, di chuyển UI GameObject đến vị trí nhấn
        pressPosition = eventData.position;
        MoveToTouchPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Khi kéo, di chuyển UI GameObject theo vị trí ngón tay
        MoveToTouchPosition(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Khi thả tay, quay lại vị trí ban đầu
        draggableObject.anchoredPosition = originalPosition;
    }

    private void MoveToTouchPosition(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, eventData.position, eventData.pressEventCamera, out localPoint);

        // Di chuyển UI GameObject đến vị trí nhấn
        draggableObject.anchoredPosition = localPoint;
    }
}
