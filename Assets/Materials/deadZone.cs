using UnityEngine;

public class deadZone : MonoBehaviour
{
    public Transform recoverPoint;
    public GameObject player;
    public GameObject box; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.position = recoverPoint.position;
            player.transform.rotation = recoverPoint.rotation;
        }
        if (other.gameObject == box)
        {
            box.transform.position = recoverPoint.position;
            box.transform.rotation = recoverPoint.rotation;
        }
        if (other.gameObject != player && box)
        {
            Destroy(other.gameObject);
        }
    }
}
