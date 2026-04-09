using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Transform holdPosition;
    public Pickupable currentItem;
    public Camera playerCamera;
    public InputActionReference dropAction;
    private float saved3DRotation;

    public AudioSource ambient3D;
    public AudioSource ambient2D;
    
    [Header("Audio")]
    public AudioClip pickupSound;
    public AudioClip dropSound;
    private AudioSource audioSource;

    void Start()
    {
        // Grab the AudioSource attached to the Player
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (dropAction.action.WasPressedThisFrame() && currentItem != null)
        {
            DropItem();
        }
    }

    public void PickupItem(Pickupable item)
    {
        currentItem = item;
        item.isPickedUp = true;

        // Disable animator if it has one
        Animator anim = item.GetComponent<Animator>();
        if (anim != null) anim.enabled = false;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider col = item.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        // SWITCH TO 2D FIRST
        SwitchTo2D();

        // Parent to PLAYER, not camera
        item.transform.SetParent(transform); // transform = player
        item.transform.localPosition = new Vector3(0, 1, 2); // In front of player
        item.transform.localRotation = Quaternion.identity;

        Debug.Log("Picked up: " + item.name);

        // Play Pickup Sound
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }

    void DropItem()
    {
        if (currentItem == null) return;

        // Save world position BEFORE unparenting
        Vector3 itemWorldPos = currentItem.transform.position;

        // Unparent
        currentItem.transform.SetParent(null);
        
        // Restore world position
        currentItem.transform.position = itemWorldPos;
        
        currentItem.isPickedUp = false;

        // Re-enable animator
        Animator anim = currentItem.GetComponent<Animator>();
        if (anim != null) anim.enabled = true;

        // Re-enable physics
        Rigidbody rb = currentItem.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        Collider col = currentItem.GetComponent<Collider>();
        if (col != null) col.enabled = true;

        currentItem = null;

        // Switch back to 3D
        SwitchTo3D();

        Debug.Log("Dropped item");

        // Play Drop Sound
        if (audioSource != null && dropSound != null)
        {
            audioSource.PlayOneShot(dropSound);
        }
    }

    void SwitchTo2D()
    {
        if (playerCamera != null)
        {
            // Save current rotation
            saved3DRotation = transform.eulerAngles.y;
            
            // Lock player to face one direction (0, 90, 180, or 270 degrees)
            transform.eulerAngles = new Vector3(0, 0, 0); // Face forward in world space
            
            playerCamera.orthographic = true;
            playerCamera.orthographicSize = 10;
            
            // Side view
            playerCamera.transform.localEulerAngles = new Vector3(0, 90, 0);
            
            // Move camera to the side
            playerCamera.transform.localPosition = new Vector3(-10, 0.6f, 0);
            
            // Disable look controls
            PlayerController pc = GetComponent<PlayerController>();
            if (pc != null) pc.disable2DLook = true;

            // Switch ambient
            if (ambient3D != null) ambient3D.Stop();
            if (ambient2D != null) ambient2D.Play();
            
            Debug.Log("Switched to 2D");
        }
    }

    void SwitchTo3D()
    {
        if (playerCamera != null)
        {
            Vector3 playerWorldPos = transform.position;
            
            playerCamera.orthographic = false;
            
            // Reset camera position
            playerCamera.transform.localPosition = new Vector3(0, 0.6f, 0);
            
            // Restore player rotation
            transform.eulerAngles = new Vector3(0, saved3DRotation, 0);
            
            transform.position = playerWorldPos;
            
            // Re-enable look controls
            PlayerController pc = GetComponent<PlayerController>();
            if (pc != null) pc.disable2DLook = false;

            // Switch ambient
            if (ambient2D != null) ambient2D.Stop();
            if (ambient3D != null) ambient3D.Play();
            
            Debug.Log("Switched to 3D");
        }
    }
}