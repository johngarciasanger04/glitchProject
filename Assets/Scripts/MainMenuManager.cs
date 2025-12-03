using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [Header("Setup")]
    [SerializeField] private string level1ToLoad;
    [SerializeField] private string level2ToLoad;
    [SerializeField] private string level3ToLoad;
    [SerializeField] private string level4ToLoad;
    [SerializeField] private string level5ToLoad;
    [SerializeField] private bool debugging = false;
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject levelsUI;
    [SerializeField] GameObject settingsUI;
    [SerializeField] GameObject creditsUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance already exists, destroying duplicate.");
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        mainMenuUI.SetActive(true);
        levelsUI.SetActive(false);
        settingsUI.SetActive(false);
        creditsUI.SetActive(false);

    }

    // Start Game Button
    public void OnStartButton()
    {
        Debug.Log("Start button clicked");
        if (debugging)
            Debug.Log("Loading level: " + level1ToLoad);

        SceneManager.LoadScene(level1ToLoad);
    }
    // Level Selector Button
    public void OnLevelsButton()
    {
        mainMenuUI.SetActive(false);
        levelsUI.SetActive(true);
        Debug.Log("Levels button clicked");
    }
    // Settings Button
    public void OnSettingsButton()
    {
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
        Debug.Log("Settings button clicked");
    }
    // Credits Button
    public void OnCreditsButton()
    {
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        creditsUI.SetActive(true);
        Debug.Log("Credits button clicked");
    }
    public void OnExitButton()
    {
        Debug.Log("Exit button clicked");
        QuitGame();
    }
    public void OnBackToMainMenuFromLevels()
    {
        levelsUI.SetActive(false);
        mainMenuUI.SetActive(true);
        Debug.Log("Back to Main Menu from Levels clicked");
    }
    public void OnBackToMainMenuFromSettings()
    {
        settingsUI.SetActive(false);
        mainMenuUI.SetActive(true);
        Debug.Log("Back to Main Menu from Settings clicked");
    }
    public void OnBacktoSettingsfromCredits()
    {
        creditsUI.SetActive(false);
        settingsUI.SetActive(true);
        Debug.Log("Back to Settings from Credits clicked");
    }
    public void OnLevel1Button()
    {
        Debug.Log("Level 1 button clicked");
        if (debugging)
            Debug.Log("Loading level: " + level1ToLoad);

        SceneManager.LoadScene(level1ToLoad);
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        if (debugging)
            Debug.Log("Quit Game called");
    }



}
