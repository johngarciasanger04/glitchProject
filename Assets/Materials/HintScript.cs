using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class HintScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private IJLNarrator narrator;

    [Header("Input Actions")]
    public InputActionReference nextLineInput;

    float elapsedTime = 0f;
    float hintTime = 5f;
    bool measureTime = false;

    // Call to start measuring time in Update() method
    void StartTimer()
    {
        measureTime = true;
    }
    // Call to stop measuring time in Update() method
    void StopTimer()
    {
        measureTime = false;
    }
    // Call to reset timer
    void ResetTimer()
    {
        elapsedTime = 0f;
    }

    // Call to get measured time
    float GetMeasuredTime()
    {
        return elapsedTime;
    }

    private string[] nothing =
    {
        "Yo aren't you not supposed to see this?"
    };

    private string[] hintJump = {
        "Maybe you should...",
        "Jump on the box?",
        "Like, hold it and jump. Idk bro we in this together!"
    };

    private string[] currentLines;
    private int currentIndex = 0;

    void Awake()
    {
        currentLines = nothing;
        currentIndex = 0;
    }

    void Update()
    {
        // Start timer only once when throwing is finished
        if (narrator.finishedThrowing && measureTime == false)
        {
            ResetTimer();
            StartTimer();
        }

        if (measureTime)
        {
            elapsedTime += Time.unscaledDeltaTime;

            if (elapsedTime >= hintTime)
            {
                StopTimer();
                narrator.SwitchLines(hintJump);
            }
        }
    }



}

