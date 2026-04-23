using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Level Settings")]
    public int totalCollectibles = 0;
    public float delayBeforeNextLevel = 2f;

    [Header("UI (Optional)")]
    public GameObject levelCompleteUI;

    private int collectiblesRemaining;
    private string currentSceneName;

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
        currentSceneName = SceneManager.GetActiveScene().name;
        
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

        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(delayBeforeNextLevel);
        
        string nextScene = GetNextScene(currentSceneName);
        SceneManager.LoadScene(nextScene);
    }

    string GetNextScene(string sceneName)
    {
        // Map current scene to next scene
        switch (sceneName)
        {
            case "Level 1":
                return "Pressure plate level";
            case "Pressure plate level":
                return "Maze Level";
            case "Maze Level":
                return "CityLevel";
            case "CityLevel":
                return "Perspective_Scene";
            case "Perspective_Scene":
                return "MainMenu_Scene";
            default:
                return "MainMenu_Scene"; // Fallback
        }
    }

    public void ReturnToMenuNow()
    {
        SceneManager.LoadScene("MainMenu_Scene");
    }
}