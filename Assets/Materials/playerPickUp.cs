using UnityEngine;
using UnityEngine.SceneManagement;

public class playerPickUp : MonoBehaviour
{
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private IJLNarrator narrator;

    private ObjectGrabbable objectGrabbable;

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (objectGrabbable == null) {
                // not carrying an object
                float pickUpDistance = 3f;
                if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                        {
                        objectGrabbable.Grab(objectGrabPointTransform);

                    }
                }
            }
            else
            {
                // carrying something
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && currentScene.name == "InfiniteJumpLevel")
        {
            narrator.canThrow = true;
        }
    }
}
