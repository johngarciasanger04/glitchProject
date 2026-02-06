using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    public GameObject player;
    private Vector3 moveOffset = Vector3.zero;

    private void Awake() {
        objectRigidbody = GetComponent<Rigidbody>();
        //objectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        moveOffset = Vector3.zero;
    }
    public void Grab (Transform objectGrabPointTransform) {
        objectRigidbody.rotation = Quaternion.Euler(0, 0, 0);
        objectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        Debug.Log("object grabbed!");
    }

    public void Drop () {
        objectRigidbody.constraints = RigidbodyConstraints.None;
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        Debug.Log("object dropped.");
    }

    public void objectMove(Vector3 direction)
    {
        float moveSpeed = 2f;
        moveOffset += direction.normalized * moveSpeed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 targetPosition = objectGrabPointTransform.position + moveOffset;
            Vector3 newPosition = Vector3.Lerp(objectRigidbody.position, targetPosition, Time.fixedDeltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
