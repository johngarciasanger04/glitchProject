using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    public int pointValue = 1;
    public bool rotateInPlace = true;
    public float rotationSpeed = 90f;
    
    [Header("Audio")]
    public string soundName = "collectSound"; // Name in SoundLib
    
    [Header("Effects (Optional)")]
    public GameObject collectEffectPrefab;
    
    private bool collected = false;
    
    private void Update()
    {
        if (rotateInPlace)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }
    
    private void Collect(GameObject player)
    {
        collected = true;
        
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.AddPoints(pointValue);
        }
        
        if (LevelManager.instance != null)
        {
            LevelManager.instance.CollectibleCollected();
        }
        
        // Play sound through SoundsManager
        if (SoundsManager.Instance != null)
        {
            SoundsManager.Instance.PlaySound2D(soundName);
        }
        
        if (collectEffectPrefab != null)
        {
            Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
}