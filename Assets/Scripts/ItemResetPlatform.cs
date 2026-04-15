using UnityEngine;

public class ItemResetPlatform : MonoBehaviour
{
    [Header("Target Item")]
    public Pickupable cylinder;
    
    [Header("Progression")]
    public GameObject[] platformsToReveal;

    private Vector3 originalPos;
    private Quaternion originalRot;

    void Start()
    {
        if (cylinder != null)
        {
            // Store where the cylinder started the level
            originalPos = cylinder.transform.position;
            originalRot = cylinder.transform.rotation;
        }

        // Hide progression platforms at start
        foreach (GameObject platform in platformsToReveal)
        {
            if (platform != null) platform.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetMechanism(other.gameObject);
        }
    }

    void ResetMechanism(GameObject player)
    {
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();

        // 1. If player is currently holding THIS cylinder, make them drop it first
        if (inventory != null && inventory.currentItem == cylinder)
        {
            // This triggers your DropItem logic (physics, unparenting, 3D switch)
            inventory.Invoke("DropItem", 0); 
        }

        // 2. Move cylinder back to start
        if (cylinder != null)
        {
            cylinder.transform.position = originalPos;
            cylinder.transform.rotation = originalRot;
            
            // Ensure the item knows it's available to be picked up again
            cylinder.isPickedUp = false;

            // Kill any physics momentum it had
            Rigidbody rb = cylinder.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        // 3. Reveal the new platforms
        foreach (GameObject platform in platformsToReveal)
        {
            if (platform != null) platform.SetActive(true);
        }

        Debug.Log("Cylinder Reset & Platforms Revealed");
    }
}