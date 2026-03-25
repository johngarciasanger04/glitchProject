using UnityEngine;

public class platform_Waypoints : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform getWaypoints(int i)
    {
        return transform.GetChild(i);
    }
    public int getnextWaypoint(int i)
    {
        int nextWaypoint = i + 1;
        if (nextWaypoint >= transform.childCount)
        {
            nextWaypoint = 0;
        }
        return nextWaypoint;
    }
}
