using UnityEngine;
using UnityEngine.Events;

public class MultiPlateController : MonoBehaviour
{
    public int requiredPressed;
    private int currentlyPressed = 0;

    public UnityEvent shouldOpen;
    public UnityEvent shouldClose;

    public void PlatePressed()
    {
        currentlyPressed++;
        UpdateDoor();
    }

    public void PlateReleased()
    {
        currentlyPressed = Mathf.Max(0, currentlyPressed - 1);
        UpdateDoor();
    }

    private void UpdateDoor()
    {
        if (currentlyPressed >= requiredPressed)
        {
            shouldOpen.Invoke();
        }

        if (currentlyPressed < requiredPressed)
        {
            shouldClose.Invoke();
        }
    }
}
