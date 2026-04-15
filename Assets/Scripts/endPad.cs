using UnityEngine;
using UnityEngine.SceneManagement;

public class endPad : MonoBehaviour
{
    string sceneName = "";
    void Start() {
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        sceneName = SceneManager.GetActiveScene().name;
    }
    // Script to detect when the player reaches the end pad and trigger the end of the level
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the end of the level
            Debug.Log("Level Complete!");    
            Cursor.visible = true; // Make the cursor visible    
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor

            //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu_Scene");
                if (sceneName == "Level 1")
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Pressure plate level");
                }
                else if (sceneName == "Pressure plate level")
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("Maze Level");
                }
                else if (sceneName == "Maze Level")
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu_Scene");
                }

        }
    }
    
}
