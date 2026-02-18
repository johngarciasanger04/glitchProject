using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 moveDirection = Vector3.right; 
    public float moveDistance = 10f;
    public float speed = 2.0f;
    public float waitTime = 1.0f;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 targetPos;
    private float nextMoveTime;
    private bool movingToEnd = true;

    private Transform playerTransform;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + (moveDirection.normalized * moveDistance);
        targetPos = endPos;
    }

    void FixedUpdate()
    {
        if (PauseMenuController.isPaused) return;

        // Movement Logic
        if (Time.time >= nextMoveTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.001f)
            {
                movingToEnd = !movingToEnd;
                targetPos = movingToEnd ? endPos : startPos;
                nextMoveTime = Time.time + waitTime;
            }
        }

        // SAFETY: If the player is still a child but drifted away, force detach
        if (playerTransform != null && playerTransform.parent == transform)
        {
            // If the player is further than the platform's width + a small buffer
            float dist = Vector3.Distance(transform.position, playerTransform.position);
            if (dist > 10f) 
            {
                playerTransform.SetParent(null);
                playerTransform = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            if (playerTransform.parent != transform)
            {
                playerTransform.SetParent(transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.parent == transform)
            {
                other.transform.SetParent(null);
                playerTransform = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 previewStart = Application.isPlaying ? startPos : transform.position;
        Vector3 previewEnd = previewStart + (moveDirection.normalized * moveDistance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(previewStart, previewEnd);
    }
}