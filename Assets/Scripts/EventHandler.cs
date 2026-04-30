using UnityEngine;
using  UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class EventHandler : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic(scene.name);
    }

    void PlaySceneMusic(string sceneName)
    {
        if (sceneName == "MainMenu_Scene")
        {
            MusicManager.Instance.PlayMusic("MainMenuTheme");
        }
        else if (sceneName == "Level 1")
        {
            MusicManager.Instance.PlayMusic("Level1Theme");
        }
        else if (sceneName == "Pressure plate level")
        {
            MusicManager.Instance.PlayMusic("PressurePlateTheme");
        }
        else if (sceneName == "Maze Level")
        {
            // do something else
            MusicManager.Instance.PlayMusic("MazeLevelTheme");

        }
        else if (sceneName == "CityLevel")
        {
            // do something else
            MusicManager.Instance.PlayMusic("CityLevelTheme");
        }
        else if (sceneName == "Perspective_Scene")
        {
            // do something else
            MusicManager.Instance.PlayMusic("ambient3D");
        }
        else if (sceneName == "InfiniteJumpLevel")
        {
            // do something else
            MusicManager.Instance.PlayMusic("InfiniteJumpTheme");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
