using UnityEngine;

public class TwoDOnlyPlatform : MonoBehaviour
{
    private Renderer platformRenderer;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Find player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        
        // Show platform only when holding 2D item
        if (inventory != null && inventory.currentItem != null)
        {
            platformRenderer.enabled = true;
        }
        else
        {
            platformRenderer.enabled = false;
        }
    }
}