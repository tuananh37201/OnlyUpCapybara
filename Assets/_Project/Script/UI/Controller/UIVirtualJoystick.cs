using UnityEngine;
using UnityEngine.EventSystems;

public class UIVirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Header("Joysticks Images RectTransform Reference")]
    [SerializeField] RectTransform joystickContainerRectTransform;
    [SerializeField] RectTransform joystickHandleRectTransform;
    [SerializeField] GameObject joystickHandleObject;

    [Header("Joystick Settings")]
    [SerializeField] float joystickMovementRange = 50f;
    [SerializeField] float magnitudeMultiplier = 1f;
    [SerializeField] bool isXOutputValueInverted;
    [SerializeField] bool isYOutputValueInverted;

    [Header("Position Reference For The Joystick")]
    private Vector2 touchPosition;
    private Vector2 clampedPosition;
    private Vector2 outputPosition;
    public Vector2 playerVectorOutput;

    private void Start()
    {
        SetupJoystickHandle();
        //joystickHandleObject.SetActive(false);
    }
    private void SetupJoystickHandle()
    {
        if (joystickHandleRectTransform)
        {
            UpdateJoystickHandleRectTransformPosition(Vector2.zero);
        }
    }
    public void OnPointerUp(PointerEventData onPointerUpEventData)
    {
        OutputVectorValue(Vector2.zero);
        //joystickHandleObject.SetActive(false);
        if (joystickHandleRectTransform)
        {
            UpdateJoystickHandleRectTransformPosition(Vector2.zero);
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
    public void OnPointerDown(PointerEventData onPointerDownEventData)
    {
        OnDrag(onPointerDownEventData);
        //joystickHandleObject.SetActive(true);
    }

    public void OnDrag(PointerEventData onDragEvenData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickContainerRectTransform, onDragEvenData.position, onDragEvenData.pressEventCamera, out touchPosition);
        touchPosition = ApplySizeDelta(touchPosition);
        outputPosition = ApplyInversionFilter(touchPosition);
        clampedPosition = ClampValuesToMagnitude(touchPosition);

        OutputVectorValue(outputPosition * magnitudeMultiplier);

        if (joystickHandleRectTransform)
        {
            UpdateJoystickHandleRectTransformPosition(clampedPosition * joystickMovementRange);
        }
    }

    Vector2 ApplySizeDelta(Vector2 position)
    {
        float x = (position.x / joystickContainerRectTransform.sizeDelta.x) * 5.5f;
        float y = (position.y / joystickContainerRectTransform.sizeDelta.y) * 5.5f;
        return new Vector2(x, y);
    }

    Vector2 ClampValuesToMagnitude(Vector2 position)
    {
        return Vector2.ClampMagnitude(position, 1);
    }

    Vector2 ApplyInversionFilter(Vector2 position)
    {
        if (isXOutputValueInverted)
        {
            position.x = InverValue(position.x);
        }

        if (isYOutputValueInverted)
        {
            position.y = InverValue(position.y);
        }

        return position;
    }

    float InverValue(float valueToInvert)
    {
        return -valueToInvert;
    }
    private void UpdateJoystickHandleRectTransformPosition(Vector2 newPosition)
    {
        joystickHandleRectTransform.anchoredPosition = newPosition;
    }
}


