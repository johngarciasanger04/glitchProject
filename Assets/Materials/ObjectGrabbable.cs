using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    public GameObject player;
    private Vector3 moveOffset = Vector3.zero; // legacy code from old version, 99% sure does nothing

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        moveOffset = Vector3.zero;
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        objectRigidbody.rotation = Quaternion.Euler(0, 0, 0);
        objectRigidbody.constraints = RigidbodyConstraints.FreezeRotation; // prevents box from rotation during grab
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        Debug.Log("object grabbed!"); // debug log to confirm if object has been grabbed
    }

    public void Drop()
    {
        objectRigidbody.constraints = RigidbodyConstraints.None; // removes rotation constraint for normal object activity
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        Debug.Log("object dropped.");
    }

    public void objectMove(Vector3 direction)
    {
        float moveSpeed = 2f;
        moveOffset += direction.normalized * moveSpeed * Time.deltaTime;
    }
    private void FixedUpdate() {
        if (objectGrabPointTransform != null)
        {
            objectRigidbody.MovePosition(objectGrabPointTransform.position);
        }
    }
}

