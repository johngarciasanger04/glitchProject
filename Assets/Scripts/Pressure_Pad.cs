using UnityEngine;

public class Pressure_Pad : MonoBehaviour
{
    
    
    private void OnTriggerEnter(Collider other)
    {
        //open door (message?)
        Debug.Log("object on plate");
    }

    private void OnTriggerExit(Collider other)
    {
        //stop opening door (message?)
        Debug.Log("Object removed from plate");
    }
}
