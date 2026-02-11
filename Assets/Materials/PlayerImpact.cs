using UnityEngine;

public class PlayerImpact : MonoBehaviour
{
    CharacterController controller;
    Vector3 impact = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void AddImpact(Vector3 direction, float force)
    {
        direction.Normalize();
        impact += direction * force;
    }

    void Update()
    {
        // used for impact
        if (impact.magnitude > 0.2f)
        {
            controller.Move(impact * Time.deltaTime);
        }

        // slows down the impact realistically
        impact = Vector3.Lerp(impact, Vector3.zero, Time.deltaTime * 5f);
    }
}
