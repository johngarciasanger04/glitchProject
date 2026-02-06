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

    private string[] currentLines;
    private int currentIndex = 0;

    public bool canThrow;

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
            canThrow = false; // reset so it only switches once
            
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

    void SwitchLines(string[] newLines)
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
