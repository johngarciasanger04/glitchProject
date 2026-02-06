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
    float hintTime = 60f;
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

    private string[] hintJump = {
        "Maybe you should...",
        "Jump on the box?",
        "Like, hold it and jump. Idk bro I'm trash at coding."
    };
    private string[] currentLines;
    private int currentIndex = 0;

    void Awake()
    {
        currentLines = hintJump;
        currentIndex = 0;
        ShowLine(currentIndex);
    }

    void OnEnable()
    {
        // Subscribe to input action
        nextLineInput.action.performed += OnNextLine;
        nextLineInput.action.Enable();
    }

    void OnDisable()
    {
        nextLineInput.action.performed -= OnNextLine;
        nextLineInput.action.Disable();
    }

    void Update()
    {
        if (narrator.canThrow == false)
        {
            StartTimer();
        }
        if(measureTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
        }
        if (elapsedTime >= hintTime)
        {
            SwitchLines(hintJump);
        }
    }

    private void OnNextLine(InputAction.CallbackContext ctx)
    {
        NextLine();
    }

    void ShowLine(int index)
    {
        if (index < currentLines.Length)
        {
            hintText.text = currentLines[index];
            hintText.gameObject.SetActive(true);
        }
        else
        {
            hintText.gameObject.SetActive(false);
        }
    }

    void NextLine()
    {
        currentIndex++;
        ShowLine(currentIndex);
    }

    void SwitchLines(string[] newLines)
    {
        currentLines = newLines;
        currentIndex = 0;
        ShowLine(currentIndex);
    }
    public void HintJump()
    {
        currentLines = hintJump;
        currentIndex = 0;
        ShowLine(currentIndex);
    }
}

