using UnityEngine;
using TMPro;

public class IJLNarrator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI narratorText;

    public bool canThrow = false;
    bool seenThrowText = false; 

    // Different narration sets
    private string[] introLines = {
        "Welcome in!",
        "I see you finally got to this level.",
        "There's no bugs, I promise, I just need you to throw the object to the red button."
    };

    private string[] throwLines = {
        "Oh, you can't throw it?",
        "...",
        "I don't know how you're getting past the wall, then."
    };

    private string[] firstJump = {
        "So that's...also intentional! I'm sure you can get through the levels if you just do that.",
        "Jump to your hearts content. You can die though, so be careful. I didn't program in a checkpoint...",
        "Just kidding! I know how to do that, at least."
    };

    // Active set
    private string[] currentLines;
    private int currentIndex = 0;

    void Start()
    {
        // Start with intro lines
        currentLines = introLines;
        ShowLine(currentIndex);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }

        // Example: switch to throw lines when pressing T
        if (canThrow == true && seenThrowText == false)
        {
            SwitchLines(throwLines);
            seenThrowText = true; 

        }
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

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
            currentIndex++;
            ShowLine(currentIndex);
        }
    }
}
