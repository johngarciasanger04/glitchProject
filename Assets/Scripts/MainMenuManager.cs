using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public AudioMixer audioMixer;

    [Header("Setup")]
    [SerializeField] private string level1ToLoad;
    [SerializeField] private string level2ToLoad;
    [SerializeField] private string level3ToLoad;
    [SerializeField] private string level4ToLoad;
    [SerializeField] private string level5ToLoad;
    [SerializeField] private string level6ToLoad;
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
        MusicManager.Instance.PlayMusic("MainMenuTheme");

    }

    public void UpdateMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void UpdateMusicVolume(float volume)
    {
        if (volume <= 0.0001f)
        audioMixer.SetFloat("MusicVolume", -80f);
        else
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
    }

    public void UpdateSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }
    public void saveSettings()
    {
        PlayerPrefs.Save();
        Debug.Log("Settings saved");
    }

    // Start Game Button
    public void OnStartButton()
    {
        Debug.Log("Start button clicked");
        //playsound
        SoundsManager.Instance.PlaySound2D("ButtonClick");
        if (debugging)
            Debug.Log("Loading level: " + level1ToLoad);
        MusicManager.Instance.StopMusic();
        SceneManager.LoadScene(level1ToLoad);
    }
    // Level Selector Button
    public void OnLevelsButton()
    {
        mainMenuUI.SetActive(false);
        levelsUI.SetActive(true);
        Debug.Log("Levels button clicked");
        SoundsManager.Instance.PlaySound2D("ButtonClick");
    }
    // Settings Button
    public void OnSettingsButton()
    {
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(true);
        Debug.Log("Settings button clicked");
        SoundsManager.Instance.PlaySound2D("ButtonClick");
    }
    // Credits Button
    public void OnCreditsButton()
    {
        mainMenuUI.SetActive(false);
        settingsUI.SetActive(false);
        creditsUI.SetActive(true);
        Debug.Log("Credits button clicked");
        SoundsManager.Instance.PlaySound2D("ButtonClick");
    }
    public void OnExitButton()
    {
        Debug.Log("Exit button clicked");
        SoundsManager.Instance.PlaySound2D("ButtonClick");
        QuitGame();
    }
    public void OnBackToMainMenuFromLevels()
    {
        levelsUI.SetActive(false);
        mainMenuUI.SetActive(true);
        Debug.Log("Back to Main Menu from Levels clicked");
        SoundsManager.Instance.PlaySound2D("ButtonClick");
    }
    public void OnBackToMainMenuFromSettings()
    {
        settingsUI.SetActive(false);
        mainMenuUI.SetActive(true);
        Debug.Log("Back to Main Menu from Settings clicked");
        SoundsManager.Instance.PlaySound2D("ButtonClick");
    }
    public void OnBacktoSettingsfromCredits()
    {
        creditsUI.SetActive(false);
        settingsUI.SetActive(true);
        Debug.Log("Back to Settings from Credits clicked");
        SoundsManager.Instance.PlaySound2D("ButtonClick");
    }
    public void OnLevel1Button()
    {
        Debug.Log("Level 1 button clicked");
        if (debugging)
            Debug.Log("Loading level: " + level1ToLoad);

        SoundsManager.Instance.PlaySound2D("ButtonClick");
        MusicManager.Instance.StopMusic();
        SceneManager.LoadScene(level1ToLoad);
        
    }
    public void OnLevel2Button()
    {
        Debug.Log("Level 2 button clicked");
        if (debugging)
            Debug.Log("Loading level: " + level2ToLoad);

        SoundsManager.Instance.PlaySound2D("ButtonClick");
        MusicManager.Instance.StopMusic();
        SceneManager.LoadScene(level2ToLoad);
    }
    public void OnLevel3Button()
    {
        Debug.Log("Level 3 button clicked");
        if (debugging)
            Debug.Log("Loading level: " + level3ToLoad);

        SoundsManager.Instance.PlaySound2D("ButtonClick");
        MusicManager.Instance.StopMusic();
        SceneManager.LoadScene(level3ToLoad);
    }
    public void OnLevel4Button()
    {
        Debug.Log("Level 4 button clicked");
        if (debugging)
            Debug.Log("Loading level: " + level4ToLoad);

        SoundsManager.Instance.PlaySound2D("ButtonClick");
        MusicManager.Instance.StopMusic();
        SceneManager.LoadScene(level4ToLoad);
    }
    public void onLevel5Button()
    {
        Debug.Log("Level 5 button clicked");
        if (debugging)
            Debug.Log("Loading level: " + level5ToLoad);

        SoundsManager.Instance.PlaySound2D("ButtonClick");
        MusicManager.Instance.StopMusic();
        SceneManager.LoadScene(level5ToLoad);
    }
    public void onLevel6Button()
    {
        Debug.Log("Level 6 button clicked");
        if (debugging)
            Debug.Log("Loading level: " + level6ToLoad);

        SoundsManager.Instance.PlaySound2D("ButtonClick");
        MusicManager.Instance.StopMusic();
        SceneManager.LoadScene(level6ToLoad);
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
