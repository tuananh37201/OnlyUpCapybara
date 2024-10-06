using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{

    public PlayerInput inputActions;
    private InputAction moveAction;
    private InputAction lookAction;

    [Header("Animation")]
    public Animator animator;

    [Header("Camera Reference")]
    [SerializeField] private GameObject cinemachineTargetObject;
    [SerializeField] private float sensitive;
    public Camera mainCamera;
    private Vector2 cameraMovement;
    private float cinemachineTargetX;
    private float cinemachineTargetY;


    [Header("Booster")]
    public bool isDoubleJumpActicved;
    public bool isFlyActicved;
    private bool previousIsFlyActivated;


    public bool canMove = true;
    public bool canJump = true;

    //JettpackBoost jettpackBoost;

    // NEW
    public Transform modelRoot;
    
    private Transform tr;
    private Mover mover;
    private Vector2 directionInput;

    private float currentVerticalSpeed = 0f;
    private float targetRotation;
    private float currentVelocity;
    private float smoothTime = 0.05f;

    private float flyDirection;

    private bool isGrounded;
    private bool isJumpingInput;


    public float movementSpeed = 7f;
    public float jumpSpeed = 15f;
    public float flySpeed;
    public float gravity = 10f;

    Vector3 lastVelocity = Vector3.zero;


    #region Unity Func
    private void Awake()
    {
        mover = GetComponent<Mover>();
        tr = transform;

        ControlPositionJoyStick.OnChangeMoveInput += ChangeInput;
        FixedTouchField.OnChangeMoveInput += ChangeCameraInput;
    }

    private void Start()
    {
        //jettpackBoost = FindObjectOfType<JettpackBoost>();

        mainCamera = Camera.main;
        previousIsFlyActivated = isFlyActicved;


        inputActions = GetComponent<PlayerInput>();

        if (isFlyActicved)
        {
            FlyMode();
        }
        else
        {
            FootMode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            animator.SetTrigger("Die");
            canMove = false;
            canJump = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            //jettpackBoost.DisableFallDetection(3f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            //jettpackBoost.DisableFallDetection(3f);
        }
    }

    private void Update()
    {
        CameraRotation();

        if (isFlyActicved != previousIsFlyActivated)
        {
            ClearAction();
            if (isFlyActicved)
            {
                FlyMode();
            }
            else
            {
                FootMode();
            }
            previousIsFlyActivated = isFlyActicved;

        }
    }

    private void FixedUpdate()
    {
        mover.CheckForGround();
        if (isGrounded == false && mover.IsGrounded() == true)
        {
            OnGroundContactRegained(lastVelocity);
        }

        //Check whether the character is grounded and store result
        isGrounded = mover.IsGrounded();

        Vector3 velocity = Vector3.zero;
        
        // Caculate final velocity for this frame
        velocity += CalculateMovementDirection() * movementSpeed;

        // Handle gravity
        if (!isGrounded)
        {
            currentVerticalSpeed -= gravity * Time.deltaTime;
        }
        else
        {
            if (currentVerticalSpeed <= 0f)
                currentVerticalSpeed = 0f;
        }

        // Handle jumping
        if (isJumpingInput && isGrounded)
        {
            OnJumpStart();
            currentVerticalSpeed = jumpSpeed;
            isGrounded = false;
        }

        // Add vertical velocity
        velocity += tr.up * currentVerticalSpeed;
        // If player in ground then Extend sensor range
        mover.SetExtendSensorRange(isGrounded);
        // Set mover velocity
        mover.SetVelocity(velocity);

        // Save current velocity for next frame
        lastVelocity = velocity;
    }
    #endregion


    //Calculate and return movement direction based on player input;
    //This function can be overridden by inheriting scripts to implement different player controls;
    protected virtual Vector3 CalculateMovementDirection()
    {
        Vector3 direction = Vector3.zero;

        direction += tr.right * directionInput.x;
        direction += tr.forward * directionInput.y;

        var isMoving = directionInput.sqrMagnitude != 0;
        if (isMoving)
        {
            var mainCameraY = mainCamera.transform.eulerAngles.y;
            targetRotation = Mathf.Atan2(directionInput.x, directionInput.y) * Mathf.Rad2Deg + mainCameraY;

            // Rotate the character model towards the direction it is moving;
            var rotationAngle = Mathf.SmoothDampAngle(modelRoot.eulerAngles.y, targetRotation, ref currentVelocity, smoothTime);
            modelRoot.rotation = Quaternion.Euler(0f, rotationAngle, 0f);

            // Calculate movement direction based on camera rotation;
            direction = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
        }
        else
        {
            direction.x = 0f;
            direction.z = 0f;
        }

        if (direction.magnitude > 1f)
        {
            direction.Normalize();
        }


        return direction;
    }


    //This function is called when the controller has landed on a surface after being in the air;
    void OnGroundContactRegained(Vector3 _collisionVelocity)
    {
        //Call 'OnLand' delegate function;
        if (OnLand != null)
            OnLand(_collisionVelocity);
    }

    //This function is called when the controller has started a jump;
    void OnJumpStart()
    {
        //Call 'OnJump' delegate function;
        if (OnJump != null)
            OnJump(lastVelocity);
    }

    //Return the current velocity of the character;
    public override Vector3 GetVelocity()
    {
        return lastVelocity;
    }

    //Return only the current movement velocity (without any vertical velocity);
    public override Vector3 GetMovementVelocity()
    {
        return lastVelocity;
    }

    //Return whether the character is currently grounded;
    public override bool IsGrounded()
    {
        return isGrounded;
    }

    #region Input Actions
    private void FootMode()
    {
        inputActions.SwitchCurrentActionMap("Foot");
        moveAction = inputActions.actions["Move"];
        lookAction = inputActions.actions["Look"];
        DoubleJumpAction(inputActions);

    }

    private void FlyMode()
    {
        inputActions.SwitchCurrentActionMap("Fly");
        moveAction = inputActions.actions["Move"];
        lookAction = inputActions.actions["Look"];
        FlyAction(inputActions);

    }

    private void ClearAction()
    {
        moveAction = null;
        lookAction = null;
    }

    public void DoubleJumpAction(PlayerInput inputActions)
    {
        var jumpAction = inputActions.actions["Jump"];
        jumpAction.performed += ctx => isJumpingInput = true;
        jumpAction.canceled += ctx => isJumpingInput = false;
    }

    public void FlyAction(PlayerInput inputActions)
    {
        var flyAction = inputActions.actions["Fly"];
        flyAction.performed += ctx => flyDirection = ctx.ReadValue<float>();
        flyAction.canceled += ctx => flyDirection = 0;

    }


    #endregion

    #region GetInput
    private void ChangeInput(Vector2 input)
    {
        directionInput = input;
    }

    private void ChangeCameraInput(Vector2 input)
    {
        cameraMovement = input;
        cameraMovement = sensitive * input;

        cinemachineTargetX += cameraMovement.x;
        cinemachineTargetY -= cameraMovement.y;

        cinemachineTargetX = CameraClampAngle(cinemachineTargetX, float.MinValue, float.MaxValue);
        cinemachineTargetY = CameraClampAngle(cinemachineTargetY, -30.0f, 90.0f);

    }

    #endregion

    #region Camera Rotation Section
    private void CameraRotation()
    {
        var rotation = Quaternion.Euler(cinemachineTargetY, cinemachineTargetX, 0.0f);
        cinemachineTargetObject.transform.rotation = Quaternion.Lerp(cinemachineTargetObject.transform.rotation, rotation, Time.deltaTime * 15);
    }

    private float CameraClampAngle(float angle, float angleMin, float angleMax)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;

        return Mathf.Clamp(angle, angleMin, angleMax);
    }
    #endregion

}
