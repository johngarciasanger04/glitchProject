using UnityEngine;
using UnityEngine.Events;

public class BrokenPlatformFix : MonoBehaviour
{
    public UnityEvent fixPlatform;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Whoops!");
        fixPlatform.Invoke();
    }
}
