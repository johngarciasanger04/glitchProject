using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions playerInput;
    CharacterController characterController;

    //looking
    public InputActionReference lookAction;
    public float moveSpeed = 8f;

    private Vector2 rotStore;
    public float lookSpeed = 30f;
    public Camera theCam;

    public float camZoomNormal = 60f;
    public float camZoomOut = 120f;
    public float camZoomSpeed = 5f;

    //move plus run
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    public float runMultiplier = 2f; // Sprinting = 2x faster

    public InputActionReference sprintAction;

    //gravity
    float groundedGravity = -0.05f;
    float gravity = -9.8f;

    //jumping
    bool isJumpPressed = false;
    float initialJumpVelocity;
    public float maxJumpHeight = 1.0f;
    public float maxJumpTime = 0.5f;
    bool isJumping = false;

    // Platform detection
    private Transform currentPlatform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();
        setupJumpVariables();

        playerInput.PlayerControls.Move.started += onMovementInput;
        playerInput.PlayerControls.Move.canceled += onMovementInput;
        playerInput.PlayerControls.Move.performed += onMovementInput;
        playerInput.PlayerControls.Jump.started += onJump;
        playerInput.PlayerControls.Jump.canceled += onJump;
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            // Detach from platform when jumping
            if (currentPlatform != null)
            {
                transform.SetParent(null);
                currentPlatform = null;
            }
            
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            if (currentMovement.y < 0)
                currentMovement.y = groundedGravity;
        }
        else
        {
            currentMovement.y += gravity * Time.deltaTime;
        }
    }

    void CheckPlatform()
    {
        if (!characterController.isGrounded)
        {
            if (currentPlatform != null)
            {
                transform.SetParent(null);
                currentPlatform = null;
            }
            return;
        }

        // Shoot a ray down from player center
        RaycastHit hit;
        float rayDistance = (characterController.height / 2f) + 0.3f;
        
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayDistance))
        {
            // Check if we hit a platform
            MovingPlatform platform = hit.collider.GetComponent<MovingPlatform>();
            
            if (platform != null)
            {
                // Parent to platform
                if (currentPlatform != platform.transform)
                {
                    transform.SetParent(platform.transform);
                    currentPlatform = platform.transform;
                    Debug.Log("Attached to platform!");
                }
            }
            else
            {
                // Not on platform, detach
                if (currentPlatform != null)
                {
                    transform.SetParent(null);
                    currentPlatform = null;
                    Debug.Log("Detached from platform");
                }
            }
        }
    }

    void Update()
    {
        // Keep scale normal when parented
        if (transform.parent != null)
        {
            transform.localScale = Vector3.one;
        }

        // Check what we're standing on
        CheckPlatform();

        // Handle physics
        handleGravity();
        handleJump();

        // Movement
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 horizontalMove = (forward * currentMovementInput.y + right * currentMovementInput.x).normalized;

        // Speed
        bool isSprinting = sprintAction.action.IsPressed() && isMovementPressed;
        float currentSpeed = isSprinting ? (moveSpeed * runMultiplier) : moveSpeed;

        // Apply movement
        Vector3 velocity = horizontalMove * currentSpeed;
        velocity.y = currentMovement.y;
        characterController.Move(velocity * Time.deltaTime);

        // Camera
        bool sprint = isSprinting && isMovementPressed;
        float targetFOV = sprint ? camZoomOut : camZoomNormal;
        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV, Time.deltaTime * camZoomSpeed);

        // Look
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        lookInput.y = -lookInput.y;
        rotStore = rotStore + (lookInput * lookSpeed * Time.deltaTime);
        rotStore.y = Mathf.Clamp(rotStore.y, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(0f, rotStore.x, 0f);
        theCam.transform.localRotation = Quaternion.Euler(rotStore.y, 0f, 0f);

        // Keep camera attached
        if (theCam != null && theCam.transform.parent != transform)
        {
            theCam.transform.SetParent(transform);
            theCam.transform.localPosition = new Vector3(0, 0.6f, 0);
        }
    }

    private void OnEnable()
    {
        playerInput.PlayerControls.Enable();
    }

    private void OnDisable()
    {
        playerInput.PlayerControls.Disable();
    }
}