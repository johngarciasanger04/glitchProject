using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement")]
    public Vector3 moveDirection = Vector3.forward;
    public float speed = 5f;
    
    [Header("Despawn")]
    public float destroyDistance = 50f;
    public float despawnPushMultiplier = 1.5f; 
    public float despawnGraceTime = 0.25f; 
    
    private Vector3 startPosition;
    private Rigidbody rb;
    private bool isDespawning = false;

    void Start()
    {
        startPosition = transform.position;
        
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        if (isDespawning) return;
        
        // Consistent physics movement
        Vector3 velocity = moveDirection.normalized * speed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        
        if (Vector3.Distance(startPosition, transform.position) > destroyDistance)
        {
            StartDespawn();
        }
    }

    private void StartDespawn()
    {
        isDespawning = true;
        
        // 1. Give momentum to the player so they don't stop dead
        PushRiders(); 
        
        // 2. CRITICAL: Detach player so they don't get deleted with the platform
        DetachRiders();

        Destroy(gameObject, despawnGraceTime);
    }

    private void DetachRiders()
    {
        // Check if player is a child of this platform and release them
        var player = GetComponentInChildren<PlayerController>();
        if (player != null)
        {
            player.transform.SetParent(null);
        }
    }

    private void PushRiders()
    {
        Collider col = GetComponent<Collider>();
        if (col == null) return;
        
        // Detect player nearby to give them that final push
        Collider[] nearby = Physics.OverlapBox(transform.position, col.bounds.extents * 1.2f);
        foreach (var hit in nearby)
        {
            if (hit.CompareTag("Player"))
            {
                PlayerController player = hit.GetComponent<PlayerController>();
                if (player != null)
                {
                    Vector3 momentum = moveDirection.normalized * speed * despawnPushMultiplier;
                    player.AddMomentum(momentum);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDespawning) return;
        MountPlayer(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        if (isDespawning) return;
        MountPlayer(collision);
    }

    void OnCollisionExit(Collision collision)
    {
        if (isDespawning) return;

        // If player steps off, unparent them
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.parent == transform)
            {
                collision.transform.SetParent(null);
            }
        }
    }

    private void MountPlayer(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        // Only parent if the player is actually ON TOP of the platform
        bool onTop = false;
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                onTop = true;
                break;
            }
        }

        if (onTop)
        {
            // Parenting is the smoothest way to "match speed"
            if (collision.transform.parent != transform)
            {
                collision.transform.SetParent(transform);
            }
        }
    }
}