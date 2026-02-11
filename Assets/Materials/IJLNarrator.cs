using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class IJLNarrator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI narratorText;

    [Header("Input Actions")]
    public InputActionReference nextLineInput;

    private string[] introLines = {
        "Welcome in!",
        "I see you finally got to this level.",
        "There's no bugs, I promise, I just need you to throw the object to the red button."
    };

    private string[] throwLines = {
        "Oh, you can't throw it?",
        "...how else are you gonna get over the wall?",
        "Maybe try jumping...on what? I don't know."
    };

    private string[] firstJump = {
        "So that's...also intentional! I'm sure you can get through the levels if you just do that.",
        "Jump to your hearts content. You can die though, so be careful. I didn't program in a checkpoint...",
        "Just kidding! I know how to do that, at least."
    };

    private string[] hintJump = {
        "Maybe you should...",
        "Jump on the box?",
        "Like, hold it and jump. Idk bro we in this together!"
    };


    private string[] currentLines;
    private int currentIndex = 0;

    public bool canThrow;
    public bool finishedThrowing;


    float elapsedTime = 0f;
    float hintTime = 5f;
    bool measureTime = false;
    bool hintForJump = false; 

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

    void Awake()
    {
        currentLines = introLines;
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
        if (canThrow)
        {
            SwitchLines(throwLines);
            finishedThrowing = true; // made to disable canThrow
            canThrow = false;
        }
        if (finishedThrowing && measureTime == false)
        {
            StartTimer();
        }

        if (measureTime)
        {
            elapsedTime += Time.unscaledDeltaTime;

            if (elapsedTime >= hintTime && hintForJump == false)
            {
                StopTimer();
                SwitchLines(hintJump);
                hintForJump = true; 
            }
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
            narratorText.text = currentLines[index];
            narratorText.gameObject.SetActive(true);
        }
        else
        {
            narratorText.gameObject.SetActive(false);
        }
    }

    void NextLine()
    {
        currentIndex++;
        ShowLine(currentIndex);
    }

    public void SwitchLines(string[] newLines)
    {
        currentLines = newLines;
        currentIndex = 0;
        ShowLine(currentIndex);
    }
    public void FirstJump ()
        {
            currentLines = firstJump;
            currentIndex = 0;
            ShowLine(currentIndex);
        }
}
