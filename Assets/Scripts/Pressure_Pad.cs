using UnityEngine;
using UnityEngine.Events;

public class Pressure_Pad : MonoBehaviour
{
    public UnityEvent objectOn;
    public UnityEvent objectOff;
    private int pressCount = 0;
    
    private void OnTriggerEnter(Collider other)
    {
        
        //Debug.Log("object on plate");
        pressCount++;
        if (pressCount == 1)
        {
            objectOn.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Object removed from plate");
        pressCount = Mathf.Max(0, pressCount - 1);
        if (pressCount == 0)
        {
            objectOff.Invoke();
        }
    }
}
