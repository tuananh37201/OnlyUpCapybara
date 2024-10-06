using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody playerRb;

    [SerializeField] private GameObject jumpEffectPrefab;
    [SerializeField] private Transform jumpEffectPosition;
    [SerializeField] private float jumpForce;
    [SerializeField] private float coyoteTime = 0.2f; // Thời gian coyote time
    [SerializeField] private float jumpCooldown = 0.1f; // Cooldown time between jumps
    private bool isGrounded;
    private bool isJumpingInput;
    private int jumpCount = 0;
    private const int maxJumpCount = 1;
    private float coyoteTimeCounter;
    private float lastJumpTime;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    public void Initialize(PlayerInput inputActions)
    {
        var jumpAction = inputActions.actions["Jump"];
        jumpAction.performed += ctx => isJumpingInput = true;
        jumpAction.canceled += ctx => isJumpingInput = false;
    }

    void FixedUpdate()
    {

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Reset coyote time khi chạm đất
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime; // Giảm thời gian coyote time khi không chạm đất
        }
    }

    public void Jump()
    {
        if (isJumpingInput && Time.time - lastJumpTime > jumpCooldown)
        {
            if (isGrounded || coyoteTimeCounter > 0f)
            {
                AudiosManager.Instance.PlaySFX("Jump");
                GameManager.Ins.player.animator.SetTrigger("Jump");

                jumpCount = 0;
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                coyoteTimeCounter = 0f; // Reset coyote time sau khi nhảy
                isJumpingInput = false;
                lastJumpTime = Time.time; // Update last jump time
            }
            else if (jumpCount < maxJumpCount && GameManager.Ins.player.isDoubleJumpActicved)
            {
                jumpCount++;
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJumpingInput = false;

                AudiosManager.Instance.PlaySFX("Jump");
                lastJumpTime = Time.time; // Update last jump time
            }
        }

        if (isGrounded)
        {
            jumpCount = 0;
        }
    }
}
