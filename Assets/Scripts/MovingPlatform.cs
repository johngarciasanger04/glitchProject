using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 moveDirection = Vector3.right; 
    public float moveDistance = 5.0f;
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

        // Move the platform
        if (Time.time >= nextMoveTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.fixedDeltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                movingToEnd = !movingToEnd;
                targetPos = movingToEnd ? endPos : startPos;
                nextMoveTime = Time.time + waitTime;
            }
        }

        // --- SAFETY CHECK ---
        // If the player is a child but somehow drifted too far (missed OnTriggerExit)
        if (playerTransform != null && playerTransform.parent == transform)
        {
            float dist = Vector3.Distance(transform.position, playerTransform.position);
            // If the player is more than 5 units away from the platform center, let them go!
            if (dist > (moveDistance + 5f)) 
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
        Gizmos.DrawWireCube(previewEnd, transform.localScale);
    }
}