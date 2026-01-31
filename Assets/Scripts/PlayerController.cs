using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputActions playerInput;
    CharacterController characterController;

    //looking
    public InputActionReference lookAction;
    public float moveSpeed = 2.5f;

    private Vector2 rotStore;
    public float lookSpeed;
    public Camera theCam;

    public float camZoomNormal = 60f;
    public float camZoomOut = 75f;
    public float camZoomSpeed = 5f;

    //move plus run
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    public float runMultiplier = 0.05f;

    public InputActionReference sprintAction;

    //gravity
    float groundedGravity = -0.05f;
    float gravity = -9.8f;

    //jumping
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 1.0f;
    float maxJumpTime = 0.5f;
    bool isJumping = false;

    [Header("Push Settings")]
    [SerializeField] float pushStrength = 2.5f;
    [SerializeField] float maxPushMass = 200f;
    [SerializeField] bool onlyHorizontalPush = true;
    [SerializeField] float wallDamping = 0.6f;

    //Momentum for platform despawn inheritance
    private Vector3 extraMomentum = Vector3.zero;
    public float momentumDecayRate = 5f; 

    //Pause menu
    public InputActionReference pauseAction;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    float CurrentPlanarSpeed()
    {
        Vector3 v = characterController.velocity;
        v.y = 0f;
        return v.magnitude;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Push objects logic
        var rb = hit.rigidbody;
        if (!rb || rb.isKinematic) return;
        if (rb.mass > maxPushMass) return;

        Vector3 pushDir = hit.moveDirection;

        if (onlyHorizontalPush && pushDir.y > -0.2f) 
            pushDir.y = 0f;

        float speed = CurrentPlanarSpeed();
        float facingWall = 1f - Mathf.Clamp01(Mathf.Abs(hit.normal.y) * 5f);
        float damping = Mathf.Lerp(1f, wallDamping, facingWall);

        Vector3 impulse = pushDir.normalized * pushStrength * speed * damping;
        rb.AddForceAtPosition(impulse, hit.point, ForceMode.Impulse);
    }

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();
        setupJumpVariables();

        playerInput.PlayerControls.Move.started += onMovementInput;
        playerInput.PlayerControls.Move.canceled += onMovementInput;
        playerInput.PlayerControls.Move.performed += onMovementInput;
        playerInput.PlayerControls.Run.started += onRun;
        playerInput.PlayerControls.Run.canceled += onRun;
        playerInput.PlayerControls.Jump.started += onJump;
        playerInput.PlayerControls.Jump.canceled += onJump;
    }

    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            // IMPORTANT: Detach from platform when jumping
            transform.SetParent(null);
            
            isJumping = true;
            currentMovement.y = initialJumpVelocity;
            currentRunMovement.y = initialJumpVelocity;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }
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

    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            if (currentMovement.y < 0)
                currentMovement.y = groundedGravity;
            if (currentRunMovement.y < 0)
                currentRunMovement.y = groundedGravity;

            // Decay extra momentum on ground
            extraMomentum = Vector3.Lerp(extraMomentum, Vector3.zero, Time.deltaTime * momentumDecayRate);
        }
        else
        {
            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
        }
    }

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    public void AddMomentum(Vector3 momentum)
    {
        momentum.y = 0f;
        extraMomentum += momentum;
    }

    void Update()
    {
        // 1) Safety: Ensure scale stays uniform if platform is scaled
        if (transform.parent != null && transform.localScale != Vector3.one) {
            transform.localScale = Vector3.one;
        }

        // 2) Handle gravity and jumping
        handleGravity();
        handleJump();

        // 3) Build horizontal movement relative to player facing
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 horizontalMove = (forward * currentMovementInput.y + right * currentMovementInput.x).normalized;

        // 4) Determine speed
        bool isSprinting = sprintAction.action.IsPressed() && isMovementPressed;
        float currentSpeed = isSprinting ? (moveSpeed * runMultiplier) : moveSpeed;

        // 5) Apply speed + extra momentum
        Vector3 velocity = horizontalMove * currentSpeed + extraMomentum;

        // 6) Preserve vertical velocity
        velocity.y = currentMovement.y;

        // 7) Move the character
        characterController.Move(velocity * Time.deltaTime);

        // 8) Camera Zoom
        float targetFOV = (isSprinting && isMovementPressed) ? camZoomOut : camZoomNormal;
        theCam.fieldOfView = Mathf.Lerp(theCam.fieldOfView, targetFOV, Time.deltaTime * camZoomSpeed);

        // 9) Camera look
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        lookInput.y = -lookInput.y;
        rotStore = rotStore + (lookInput * lookSpeed * Time.deltaTime);
        rotStore.y = Mathf.Clamp(rotStore.y, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(0f, rotStore.x, 0f);
        theCam.transform.localRotation = Quaternion.Euler(rotStore.y, 0f, 0f);

        // 10) Ensure Camera stays with player
        if (theCam != null && theCam.transform.parent != transform)
        {
            theCam.transform.SetParent(transform);
            theCam.transform.localPosition = new Vector3(0, 0.6f, 0); 
            theCam.transform.localRotation = Quaternion.identity;
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