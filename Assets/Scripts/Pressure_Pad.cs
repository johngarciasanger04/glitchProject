using UnityEngine;
using UnityEngine.Events;

public class Pressure_Pad : MonoBehaviour
{
    public UnityEvent objectOn;
    public UnityEvent objectOff;
    
    private void OnTriggerEnter(Collider other)
    {
        //open door (message?)
        Debug.Log("object on plate");
        objectOn.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        //stop opening door (message?)
        Debug.Log("Object removed from plate");
        objectOff.Invoke();
    }
}
