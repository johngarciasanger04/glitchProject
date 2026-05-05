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
            if (CompareTag("Wall"))
            {
                return;
            }
            else
            {
                player.transform.position = recoverPoint.position;
            }
        }
        else if (other.gameObject == box)
        {
            if (CompareTag("Wall"))
            {
                return;
            }
            else
            {
                box.transform.position = recoverPoint.position;
            }
        }
        else if (other.gameObject != player || box)
        {
            Destroy(other.gameObject);
        }
    }
}
