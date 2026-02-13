using UnityEngine;

public class PlatformSticker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag("Player"))
        {
            // Set the player's parent to the platform
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Remove the player from the platform's hierarchy
            other.transform.SetParent(null);
        }
    }
}