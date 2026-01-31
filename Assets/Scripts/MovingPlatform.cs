using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.forward;
    public float speed = 5f;
    public float destroyDistance = 50f;
    
    private Vector3 startPosition;
    private Vector3 lastPosition;

    void Start()
    {
        startPosition = transform.position;
        lastPosition = transform.position;
    }

    void Update()
    {
        // Move platform
        transform.position += moveDirection.normalized * speed * Time.deltaTime;
        
        // Destroy when too far
        if (Vector3.Distance(startPosition, transform.position) > destroyDistance)
        {
            Destroy(gameObject);
        }
        
        lastPosition = transform.position;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Move player with platform
            Vector3 platformMovement = transform.position - lastPosition;
            CharacterController controller = collision.gameObject.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.Move(platformMovement);
            }
        }
    }
}