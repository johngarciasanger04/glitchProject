using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnInterval = 2f;
    public Vector3 spawnOffset = Vector3.zero;
    public Vector3 randomRange = Vector3.zero; // Random variation
    
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnInterval)
        {
            SpawnPlatform();
            timer = 0;
        }
    }

    void SpawnPlatform()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-randomRange.x, randomRange.x),
            Random.Range(-randomRange.y, randomRange.y),
            Random.Range(-randomRange.z, randomRange.z)
        );
        
        Instantiate(platformPrefab, transform.position + spawnOffset + randomPos, Quaternion.identity);
    }
}