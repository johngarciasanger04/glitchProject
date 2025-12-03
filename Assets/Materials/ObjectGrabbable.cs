using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    public GameObject player;

    private void Awake() {
        objectRigidbody = GetComponent<Rigidbody>();
    }
    public void Grab (Transform objectGrabPointTransform) {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;
        Debug.Log("object grabbed!");
    }

    public void Drop () {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        Debug.Log("object dropped.");
    }
    private void FixedUpdate() {
        if (objectGrabPointTransform != null) {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp (transform.position, objectGrabPointTransform.position, Time.fixedDeltaTime * lerpSpeed); 
            objectRigidbody.MovePosition(newPosition);
        }
        
    }
}
