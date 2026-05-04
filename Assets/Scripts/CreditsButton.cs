using UnityEngine;
using UnityEngine.SceneManagement;


public class CreditsButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
public void LoadMainMenu()
    {
        Debug.Log(" button clicked");
        //if (debugging)
            //Debug.Log("Loading level: ");
        SceneManager.LoadScene("MainMenu_Scene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
