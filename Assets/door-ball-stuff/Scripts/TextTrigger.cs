using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    [SerializeField] private IJLNarrator narrator;
    [SerializeField] private string eventName;

    bool firstJumpA = false;
    bool checkpointPA = false;
    bool checkpointBA = false;
    bool beforeFinish = false;
    bool easterEgg1 = false;
    bool ending = false; 

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


            if (eventName == "checkpointC" && checkpointBA == false)
            {
                narrator.checkpointBA();
                checkpointBA = true;
            }

            if (eventName == "easterEgg" && easterEgg1 == false)
            {
                narrator.easterEgg1();
                easterEgg1 = true;
            }

            if (eventName == "beforeFinish" && beforeFinish == false)
            {
                narrator.beforeFinishText();
                beforeFinish = true;
            }
            
            if (eventName == "ending" && ending == false)
            {
                narrator.endingText();
                beforeFinish = true;
            }

        }
    }
}
