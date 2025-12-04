using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private IJLNarrator narrator;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            narrator.FirstJump();
        }
    }
}
