using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFly : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    private float flyDirection;

    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void Initialize(PlayerInput inputActions)
    {
        var flyAction = inputActions.actions["Fly"];
        flyAction.performed += ctx => flyDirection = ctx.ReadValue<float>();
        flyAction.canceled += ctx => flyDirection = 0;
    }

    public void Fly()
    {
        playerRb.velocity = new Vector3(playerRb.velocity.x, flyDirection * flySpeed, playerRb.velocity.z);
    }
}
