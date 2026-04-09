using UnityEngine;

public class TeleportScript : MonoBehaviour
{
    public Transform teleEndPoint;   // Where the player will be teleported
    public GameObject player;        // The player object

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.position = teleEndPoint.position;
            player.transform.rotation = teleEndPoint.rotation;
        }
    }
}
