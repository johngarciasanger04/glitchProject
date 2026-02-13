using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2.0f;
    public float waitTime = 1.0f;

    private Vector3 targetPosition;
    private float nextMoveTime;
    private bool movingToEnd = true;

    void Start()
    {
        transform.position = startPoint.position;
        targetPosition = endPoint.position;
    }

    void FixedUpdate()
    {
        // Don't move if the game is paused (PauseMenuController handles this by setting isKinematic)
        // but we add an extra check here for safety.
        if (PauseMenuController.isPaused) return;

        if (Time.time >= nextMoveTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                // Swap targets
                movingToEnd = !movingToEnd;
                targetPosition = movingToEnd ? endPoint.position : startPoint.position;
                nextMoveTime = Time.time + waitTime;
            }
        }
    }

    // This handles "sticking" the player to the platform
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}