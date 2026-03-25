using UnityEngine;
public class PlatformCollision : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.transform.SetParent(null);
        }
    }
}
