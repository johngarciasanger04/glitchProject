using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    public int pointValue = 1;
    public bool rotateInPlace = true;
    public float rotationSpeed = 90f;
    
    [Header("Audio (Optional)")]
    public AudioClip collectSound;
    
    [Header("Effects (Optional)")]
    public GameObject collectEffectPrefab; // Particle effect or something
    
    private void Update()
    {
        // Optional: rotate the collectible to make it obvious
        if (rotateInPlace)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Check if player touched us
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }
    
    private void Collect(GameObject player)
    {
        // Add points to player
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.AddPoints(pointValue);
        }
        
        // Play sound if we have one
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
        
        // Spawn effect if we have one
        if (collectEffectPrefab != null)
        {
            Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
        }
        
        // Destroy the collectible
        Destroy(gameObject);
    }
}