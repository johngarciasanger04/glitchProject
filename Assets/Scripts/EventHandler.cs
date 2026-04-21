using UnityEngine;
using  UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class EventHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicManager.Instance.PlayMusic("Level1Theme");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
