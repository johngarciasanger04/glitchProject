using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Level Settings")]
    public int totalCollectibles = 0;
    public string menuSceneName = "MainMenu"; // Change this to match your menu scene name
    public float delayBeforeMenu = 2f; // Wait 2 seconds before going to menu

    [Header("UI (Optional)")]
    public GameObject levelCompleteUI; // Optional UI to show "Level Complete!"

    private int collectiblesRemaining;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Count all collectibles in the scene
        if (totalCollectibles == 0)
        {
            totalCollectibles = FindObjectsOfType<Collectible>().Length;
        }
        
        collectiblesRemaining = totalCollectibles;
        
        Debug.Log($"Level started with {totalCollectibles} collectibles");

        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(false);
        }
    }

    public void CollectibleCollected()
    {
        collectiblesRemaining--;
        
        Debug.Log($"Collectibles remaining: {collectiblesRemaining}/{totalCollectibles}");

        if (collectiblesRemaining <= 0)
        {
            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        Debug.Log("LEVEL COMPLETE!");

        // Show completion UI if you have one
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
        }

        // Unlock cursor for menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Go back to menu after delay
        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(delayBeforeMenu);
        SceneManager.LoadScene(menuSceneName);
    }

    // Optional: Manual return to menu (for a button)
    public void ReturnToMenuNow()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}