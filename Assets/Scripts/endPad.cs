using UnityEngine;

public class endPad : MonoBehaviour
{
    // Script to detect when the player reaches the end pad and trigger the end of the level
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the end of the level
            Debug.Log("Level Complete!");    
            Cursor.visible = true; // Make the cursor visible    
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu_Scene");

        }
    }
    
}
