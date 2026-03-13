using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Transform holdPosition;
    public Pickupable currentItem;
    public Camera playerCamera;
    public InputActionReference dropAction;

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

        // DON'T parent to holdPosition, parent directly to camera
        item.transform.SetParent(playerCamera.transform);
        
        // Position in front of camera (local to camera)
        item.transform.localPosition = new Vector3(0, 0, 2); // 2 units in front of camera
        item.transform.localRotation = Quaternion.identity;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider col = item.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        SwitchTo2D();

        Debug.Log("Picked up: " + item.name);
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
    }

    void SwitchTo2D()
    {
        if (playerCamera != null)
        {
            playerCamera.orthographic = true;
            playerCamera.orthographicSize = 10;
            
            // Side view
            playerCamera.transform.localEulerAngles = new Vector3(0, 90, 0);
            
            // Move camera to the side
            playerCamera.transform.localPosition = new Vector3(-10, 0.6f, 0);
            
            // Disable look controls
            PlayerController pc = GetComponent<PlayerController>();
            if (pc != null) pc.disable2DLook = true;
            
            Debug.Log("Switched to 2D");
        }
    }

    void SwitchTo3D()
    {
        if (playerCamera != null)
        {
            // Save player world position
            Vector3 playerWorldPos = transform.position;
            
            playerCamera.orthographic = false;
            
            // Reset camera position
            playerCamera.transform.localPosition = new Vector3(0, 0.6f, 0);
            
            // Restore player position
            transform.position = playerWorldPos;
            
            // Re-enable look controls
            PlayerController pc = GetComponent<PlayerController>();
            if (pc != null) pc.disable2DLook = false;
            
            Debug.Log("Switched to 3D");
        }
    }
}