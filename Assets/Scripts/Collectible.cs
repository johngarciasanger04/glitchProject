using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 10;
    
    void Update()
    {
        // Rotate for effect
        transform.Rotate(Vector3.up, 100f * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collected! +" + points);
            Destroy(gameObject);
        }
    }
}