using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2.0f;
    private Vector3 target;

    void Start()
    {
        target = endPoint.position;
    }

    void Update()
    {
        // Move the platform
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Switch targets when reaching a point
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == startPoint.position) ? endPoint.position : startPoint.position;
        }
    }
}