using UnityEngine;
using UnityEngine.SceneManagement;

public class endPad : MonoBehaviour
{
    // for storing the name of the current scene
    string sceneName = "";
    
    void Start() 
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        sceneName = SceneManager.GetActiveScene().name;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Complete!");    
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // check the name of the current scene and load the next one accordingly
            if (sceneName == "Level 1")
            {
                MusicManager.Instance.StopMusic();
                SceneManager.LoadScene("Pressure plate level");
            }
            else if (sceneName == "Pressure plate level")
            {
                MusicManager.Instance.StopMusic();
                SceneManager.LoadScene("Maze Level");
            }
            else if (sceneName == "Maze Level")
            {
                MusicManager.Instance.StopMusic();
                SceneManager.LoadScene("CityLevel");
            }
            else if (sceneName == "CityLevel")
            {
                MusicManager.Instance.StopMusic();
                SceneManager.LoadScene("Perspective_Scene");
            }
            else if (sceneName == "Perspective_Scene")
            {
                MusicManager.Instance.StopMusic();
                SceneManager.LoadScene("InfiniteJumpLevel");
            }
            else if (sceneName == "InfiniteJumpLevel")
            {
                MusicManager.Instance.StopMusic();
                SceneManager.LoadScene("MainMenu_Scene");
            }
        }
    }
}