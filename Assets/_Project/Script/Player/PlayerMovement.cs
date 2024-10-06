using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float smoothTime = 0.05f;
    [SerializeField] private float gravityMultiplier;

    private Rigidbody playerRb;
    private float targetRotation;
    private float currentVelocity;
    bool isGrounded;

    [SerializeField] private Transform main;

    public ParticleSystem runEffect;

    private Animator animator;

    private bool isPlayingFootstep; // Biến theo dõi trạng thái âm thanh bước chân
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GameManager.Ins.player.animator;
    }

    public void Movement(Vector2 directionInput, Camera mainCamera)
    {
        var isMoving = directionInput.sqrMagnitude != 0;

        // Animation and Effect
        animator.SetBool(Walk, isMoving && isGrounded);
        animator.SetBool(Idle, !isMoving && isGrounded);

        if (isGrounded)
        {
            if (!runEffect.isPlaying)
            {
                runEffect.Play();
            }

            if (isMoving && !isPlayingFootstep)
            {
                StartCoroutine(PlayFootstep());
            }
            else if (!isMoving && isPlayingFootstep)
            {
                StopCoroutine(PlayFootstep());
                isPlayingFootstep = false;
            }
        }
        else
        {
            if (runEffect.isPlaying)
            {
                runEffect.Stop();
            }

            if (isPlayingFootstep)
            {
                StopCoroutine(PlayFootstep());
                isPlayingFootstep = false;
            }
        }

        if (isMoving)
        {
            var mainCameraY = mainCamera.transform.eulerAngles.y;
            targetRotation = Mathf.Atan2(directionInput.x, directionInput.y) * Mathf.Rad2Deg + mainCameraY;

            var rotationAngle = Mathf.SmoothDampAngle(main.eulerAngles.y, targetRotation, ref currentVelocity, smoothTime);
            main.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

            var targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            var movement = targetDirection * speed + new Vector3(0f, playerRb.velocity.y, 0f);
            playerRb.velocity = movement;

        }
        else
        {
            playerRb.velocity = new Vector3(0f, playerRb.velocity.y, 0f);
        }
    }

    public void ApplyGravity()
    {
        playerRb.velocity += Physics.gravity * (gravityMultiplier * Time.fixedDeltaTime);
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator PlayFootstep()
    {
        isPlayingFootstep = true;
        while (isPlayingFootstep)
        {
            AudiosManager.Instance.PlaySFX("Footstep");
            yield return new WaitForSeconds(0.5f); // Điều chỉnh thời gian giữa các bước chân
        }
    }

}