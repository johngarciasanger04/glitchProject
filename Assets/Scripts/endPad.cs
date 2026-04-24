using UnityEngine;
using UnityEngine.SceneManagement;

public class endPad : MonoBehaviour
{
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

            if (sceneName == "Level 1")
            {
                SceneManager.LoadScene("Pressure plate level");
            }
            else if (sceneName == "Pressure plate level")
            {
                SceneManager.LoadScene("Maze Level");
            }
            else if (sceneName == "Maze Level")
            {
                SceneManager.LoadScene("CityLevel");
            }
            else if (sceneName == "CityLevel")
            {
                SceneManager.LoadScene("Perspective_Scene");
            }
            else if (sceneName == "Perspective_Scene")
            {
                SceneManager.LoadScene("MainMenu_Scene");
            }
        }
    }
}