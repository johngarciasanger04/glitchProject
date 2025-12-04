using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerPickUp : MonoBehaviour
{
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private IJLNarrator narrator;

    [Header("Input Actions")]
    public InputActionReference pickUpInput;    
    public InputActionReference enableThrowInput;
    public InputActionReference moveUp;
    public InputActionReference moveDown;
    public InputActionReference moveLeft;
    public InputActionReference moveRight;
    public InputActionReference moveForward;
    public InputActionReference moveBack;

    private ObjectGrabbable objectGrabbable;

    private void OnEnable()
    {
        if (pickUpInput != null)
        {
            pickUpInput.action.Enable();
            pickUpInput.action.performed += OnPickUp;
        }

        if (enableThrowInput != null)
        {
            enableThrowInput.action.Enable();
            enableThrowInput.action.performed += OnEnableThrow;
        }
        moveUp.action.Enable();
        moveDown.action.Enable();
        moveLeft.action.Enable();
        moveRight.action.Enable();
        moveForward.action.Enable();
        moveBack.action.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid leaks
        if (pickUpInput != null)
            pickUpInput.action.performed -= OnPickUp;

        if (enableThrowInput != null)
            enableThrowInput.action.performed -= OnEnableThrow;
    }

    private void OnPickUp(InputAction.CallbackContext context)
    {
        Debug.Log("PickUp pressed!");
        if (objectGrabbable == null)
        {
            // not carrying an object
            float pickUpDistance = 3f;
            if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                {
                    objectGrabbable.Grab(objectGrabPointTransform);
                }
            }
        }
        else
        {
            // carrying something
            objectGrabbable.Drop();
            objectGrabbable = null;
        }
    }
    private void Update()
    {
        if (objectGrabbable != null)
        {
            Vector3 moveDir = Vector3.zero;

            if (moveForward.action.IsPressed()) moveDir += CameraTransform.forward;
            if (moveBack.action.IsPressed()) moveDir -= CameraTransform.forward;
            if (moveUp.action.IsPressed()) moveDir += Vector3.up;
            if (moveDown.action.IsPressed()) moveDir += Vector3.down;
            if (moveLeft.action.IsPressed()) moveDir -= CameraTransform.right;
            if (moveRight.action.IsPressed()) moveDir += CameraTransform.right;
            objectGrabbable.objectMove(moveDir);
        }
    }
    private void OnEnableThrow(InputAction.CallbackContext context)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "InfiniteJumpLevel")
        {
            narrator.canThrow = true;
        }
    }
}
