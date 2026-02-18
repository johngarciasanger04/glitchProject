using UnityEngine;

public class respawn : MonoBehaviour
{
    public GameObject flag;
    Vector3 spawnPoint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spawnPoint = gameObject.transform.position;
        Debug.Log("spawn set, id = " + gameObject.GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("respawn update running, id = " + gameObject.GetInstanceID());
        if(gameObject.transform.position.y < -20f)
        {
            Debug.Log("Fallen past death plane");
            Respawn();
        }
    }

    void Respawn()
    {
        CharacterController controller = gameObject.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            transform.position = spawnPoint;
            controller.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        flag = other.gameObject;
        if(other.gameObject.CompareTag("Respawn"))
        {
            spawnPoint = flag.transform.position;
            Debug.Log("New spawn set");
            Destroy(flag);
        }
    }
}
