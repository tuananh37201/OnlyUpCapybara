using UnityEngine;
using UnityEngine.InputSystem;

public class GetInput : MonoBehaviour
{
    public Vector2 moveInput;
    public bool onJumpButton;


    private void Awake()
    {
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onJumpButton = true;
        }
        else
        {
            onJumpButton = false;
        }
    }
}
