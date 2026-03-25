using UnityEngine;

public class p_Func : MonoBehaviour
{
    [SerializeField] private platform_Waypoints path;
    [SerializeField] private float spd;
    [SerializeField] private string playerTag = "Player";

    private int targetWaypointindex;
    private Transform previousWaypoint;
    private Transform targetWaypoint;
    private float timeToWaypoint;
    private float elapsedTime;

    void Start()
    {
        TargetNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0f, 1f, elapsedPercentage);
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        if (elapsedPercentage >= 1f)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        previousWaypoint = path.getWaypoints(targetWaypointindex);
        targetWaypointindex = path.getnextWaypoint(targetWaypointindex);
        targetWaypoint = path.getWaypoints(targetWaypointindex);
        timeToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position) / spd;
        elapsedTime = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.transform.SetParent(null);
        }

    }

}
