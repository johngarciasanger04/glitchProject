using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform teleportDestination;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; // disable before moving
            other.transform.position = teleportDestination.position;
            controller.enabled = true;  // re-enable after
        }
        else
        {
            // fallback if not using CharacterController
            other.transform.position = teleportDestination.position;
        }
    }
}