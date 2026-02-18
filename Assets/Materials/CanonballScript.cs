using UnityEngine;

public class CannonballScript : MonoBehaviour
{
    public float pushForce = 10f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var push = collision.collider.GetComponent<PlayerImpact>();
            if (push != null)
            {
                Vector3 dir = -collision.contacts[0].normal;
                push.AddImpact(dir, pushForce);
            }
        }
    }
}
