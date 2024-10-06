using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Touch myTouch;
    private int touchID;

    private Vector2 lastTouchPosition;

    public static Action<Vector2> OnChangeMoveInput;

    public void OnPointerUp(PointerEventData onPointerUpData)
    {

    }

    public void OnPointerDown(PointerEventData onPointerDownData)
    {
        lastTouchPosition = onPointerDownData.position;
        OnDrag(onPointerDownData);
        touchID = myTouch.fingerId;
    }

    public void OnDrag(PointerEventData onDragData)
    {
        var touchVectorOutput = onDragData.position - lastTouchPosition;
        OnChangeMoveInput?.Invoke(touchVectorOutput);
        lastTouchPosition = onDragData.position;
    }
}
