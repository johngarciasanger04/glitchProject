using UnityEngine;
using UnityEngine.UIElements;

public class checkpointMaker : MonoBehaviour
{
    public Transform recoverPoint;
    public Transform checkpoint;
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            recoverPoint.transform.position = checkpoint.position;
            recoverPoint.transform.rotation = checkpoint.rotation;
        }
    }
}
