using UnityEngine;
using UnityEngine.UI;

public class JettpackBoost : MonoBehaviour
{
    public GameObject jettpackPanel;
    private BoostManager boostManager;

    public bool isFalling = false;
    public float fallTime = 0.0f;
    private float cooldownTime = 0.0f;
    public bool canUse = true;

    public float fallThreshold = 2.0f; // Thời gian người chơi rời khỏi mặt đất trước khi hiển thị Panel
    public float cooldownDuration = 20.0f; // Thời gian cooldown
    public float boosterDuration = 10f;

    private Transform playerTransform;
    private float lastYPosition;
    private bool wasOnGround;

    public bool isFallDetectionDisabled = false;
    private float disableDuration = 5.0f; // Thời gian vô hiệu hóa kiểm tra rơi
    private float disableTimer = 0.0f;
    bool isGrounded;

    public Button getNowButton;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        boostManager = GetComponent<BoostManager>();
    }

    void Update()
    {
        if (isFallDetectionDisabled)
        {
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0)
            {
                isFallDetectionDisabled = false;
            }
        }

        if (!canUse || playerTransform == null || isFallDetectionDisabled) return;

        if (cooldownTime > 0)
        {
            cooldownTime -= Time.deltaTime;
            return;
        }
    }

    public void CheckFallingStatus()
    {
        float currentYPosition = playerTransform.position.y;

        // Kiểm tra việc rời khỏi mặt đất với isGrounded

        if (!isGrounded)
        {
            if (!wasOnGround)
            {
                // Đã rời khỏi mặt đất
                wasOnGround = true;
                fallTime = 0.0f;
            }

            isFalling = currentYPosition < lastYPosition;
            fallTime += Time.deltaTime;
        }
        else
        {
            // Trở lại mặt đất
            wasOnGround = false;
            isFalling = false;
            fallTime = 0.0f;
        }

        lastYPosition = currentYPosition;
    }

    public void ActivateJettpackBoostPanel()
    {
        jettpackPanel.SetActive(true);
    }

    public void OnButtonClick()
    {
        jettpackPanel.SetActive(false);
        Time.timeScale = 1.0f;
        fallTime = 0.0f;
        cooldownTime = cooldownDuration;
    }


    public void DisableFallDetection(float duration)
    {
        isFallDetectionDisabled = true;
        disableTimer = duration;
    }
}