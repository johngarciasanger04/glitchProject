using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public Transform holdPosition;
    public Pickupable currentItem;
    public Camera playerCamera;
    public InputActionReference dropAction;
    public PlayerController playerController; // Add reference to player controller

    private Vector3 saved3DRotation;

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

        item.transform.SetParent(holdPosition);
        item.transform.localPosition = Vector3.zero;
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

        currentItem.transform.SetParent(null);
        currentItem.isPickedUp = false;

        Rigidbody rb = currentItem.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        Collider col = currentItem.GetComponent<Collider>();
        if (col != null) col.enabled = true;

        currentItem = null;

        SwitchTo3D();

        Debug.Log("Dropped item");
    }

    void SwitchTo2D()
    {
        if (playerCamera != null)
        {
            // Save current rotation
            saved3DRotation = playerCamera.transform.localEulerAngles;
            
            playerCamera.orthographic = true;
            playerCamera.orthographicSize = 5;
            
            // Force side view
            playerCamera.transform.localEulerAngles = new Vector3(0, 90, 0);
            
            // Disable look controls
            if (playerController != null)
            {
                playerController.enabled = false;
            }
            
            Debug.Log("Switched to 2D");
        }
    }

    void SwitchTo3D()
    {
        if (playerCamera != null)
        {
            playerCamera.orthographic = false;
            
            // Restore rotation
            playerCamera.transform.localEulerAngles = saved3DRotation;
            
            // Re-enable look controls
            if (playerController != null)
            {
                playerController.enabled = true;
            }
            
            Debug.Log("Switched to 3D");
        }
    }
}