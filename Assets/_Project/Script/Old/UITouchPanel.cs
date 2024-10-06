using UnityEngine;
using UnityEngine.EventSystems;

public class UITouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector2 playerVectorOutput;
    private Touch myTouch;
    private int touchID;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                myTouch = Input.GetTouch(i);
                if (myTouch.fingerId == touchID)
                {
                    if (myTouch.phase != TouchPhase.Moved)
                        OutputVectorValue(Vector2.zero);
                }
            }
        }
    }
    private void OutputVectorValue(Vector2 outputValue)
    {
        playerVectorOutput = outputValue;
    }
    public Vector2 VectorOutput()
    {
        return playerVectorOutput;
    }

    public void OnPointerUp(PointerEventData onPointerUpData)
    {
        OutputVectorValue(Vector2.zero);
    }

    public void OnPointerDown(PointerEventData onPointerDownData)
    {
        OnDrag(onPointerDownData);
        touchID = myTouch.fingerId;
    }

    public void OnDrag(PointerEventData onDragData)
    {
        OutputVectorValue(new Vector2(onDragData.delta.normalized.x, onDragData.delta.normalized.y));
    }
}

