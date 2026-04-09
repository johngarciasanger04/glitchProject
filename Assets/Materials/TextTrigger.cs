using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private IJLNarrator narrator;
    [SerializeField] private string eventName;

    bool firstJumpA = false;
    bool checkpointPA = false;
    bool beforeFinish = false; 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (eventName == "firstJump" && firstJumpA == false)
            {
                narrator.FirstJump();
                firstJumpA = true;
            }
            if (eventName == "checkpointP" && checkpointPA == false)
            {
                narrator.checkpointPA();
                checkpointPA = true;
            }

            if (eventName == "beforeFinish" && beforeFinish == false)
            {
                narrator.beforeFinishText();
                beforeFinish = true;
            }
        }
    }
}
