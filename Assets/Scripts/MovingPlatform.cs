using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.forward;
    public float speed = 5f;
    public float destroyDistance = 50f;
    
    private Vector3 startPosition;
    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }

    void FixedUpdate()
    {
        // Move using MovePosition for kinematic rigidbodies
        if (rb != null)
        {
            Vector3 newPos = rb.position + (moveDirection.normalized * speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
        
        // Destroy when too far
        if (Vector3.Distance(startPosition, transform.position) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}