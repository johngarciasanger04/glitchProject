using UnityEngine;
using System.Collections;

public class BoxPickUp : MonoBehaviour
{
    private Rigidbody boxRigidbody;
    public GameObject player;
    public GameObject box;
    public float thrust; 
    bool enter = false;
    bool holding = false;
    bool canHoldBall = false;
    float x, y, z; 


    void Start()
    {
        boxRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        x = player.transform.position.x;
        y = player.transform.position.y;
        z = player.transform.position.z;
        if (Input.GetKeyDown(KeyCode.F) && canHoldBall == true && holding == false)
        {
            holding = true; 

            //Makes the GameObject "newParent" the parent of the GameObject "player".
            box.transform.parent = player.transform;


            boxRigidbody.useGravity = false; 
            boxRigidbody.linearVelocity = Vector3.zero;
            boxRigidbody.angularVelocity = Vector3.zero;
            box.transform.localPosition = new Vector3(x, 3, 2);

            // box.position += Vector3.up * 10.0f; 

            //Display the parent's name in the console.
            Debug.Log("Player's Parent: " + box.transform.parent.name);

            // Check if the new parent has a parent GameObject.
            if (player.transform.parent != null)
            {
                //Display the name of the grand parent of the player.
                Debug.Log("Player's Grand parent: " + box.transform.parent.parent.name);
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && holding == true)
        {
            DetachFromParent(); // drops the object
            boxRigidbody.AddForce(player.transform.forward * thrust, ForceMode.Impulse); // meant to throw the object 
            boxRigidbody.useGravity = true;
        }
    } 
    void OnGUI()
    {
        if (enter)
        {
            canHoldBall = true;
            if (holding == true)
                {
                    GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 155, 30), "Press 'G' to drop the box.");
                 
                }
            else
            {
                // lets the player know they can pick it up
                GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 155, 30), "Press 'F' to hold the box.");
            }

        }
    }

    public void DetachFromParent()
    {
        // Detaches the transform from its parent.
        transform.parent = null;
        enter = false;
        canHoldBall = false;
        holding = false; 
    }

    // Activate the Main function when Player enter the trigger area
    void OnTriggerEnter(Collider box)
    {
        if (box.CompareTag("Player"))
        {
            enter = true;
        }
    }

    // Deactivate the Main function when Player exit the trigger area
    void OnTriggerExit(Collider box)
    {
        if (box.CompareTag("Player") && holding == false)
        {
            enter = false;
            canHoldBall = false; 
            transform.parent = null;
        }
    }
}