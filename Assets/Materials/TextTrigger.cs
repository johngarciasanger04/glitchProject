using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private IJLNarrator narrator;
    bool firstJumpA = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && firstJumpA == false)
        {
            narrator.FirstJump();
            firstJumpA = true;
        }
    }
}
