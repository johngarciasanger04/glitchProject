using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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

    private string[] checkpointP = {
        "Wow, you made it! I uh...didn't expect you'd do it like that...",
        "Which isn't really surprising as I did not expect this at all.",
        "Good thing I programmed in a checkpoint!",
        "You should be able to do this next part with ease! Good luck!"

   };

    private string[] checkpointC =
    {
        "Wow, you made it! I uh...didn't expect you'd do it like that...",
        "All those balls...were they not balls? Whatever they were, it was wild.",
        "Good thing I made this checkpoint! I totally didn't just create it right now for you.",
        "Anyways, this next part is super easy! Even a baby could do it, probably..."

    };

    private string[] beforeFinish =
    {
        "You did it! Good job, buddy!",
        "Me and you make a great team...",
        "We BOTH earned this victory."
    };

    private string[] easterEgg =
    {
        "Oh...how did you end up here?",
        "You're asking me? Come on, don't ask me. I don't know.",
        "Okay, so maybe the object is a bit longer than intended, but we didn't have a lot of time.",
        "How are you going to get back? Uh...good question. You still have the box right?",
        "Well, how about we give you an ending anyways. I say you worked hard enough!"
    };

    private string[] finale = {
        "You did it!",
        "...yes, this is it. This view is truly beautiful.",
        "Anyways, thanks for playing!",
        "Also, I hope you didn't cheat and get this the easy way! I worked...hard on this!"
    };

    private string[] currentLines;
    private int currentIndex = 0;

    public bool canThrow;
    public bool finishedThrowing;


    float elapsedTime = 0f;
    float hintTime = 5f;
    bool measureTime = false;
    bool hintForJump = false;
    bool easterEggTrigger = false;
    bool endingTrigger = false; 

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
        if (easterEggTrigger && currentIndex >= currentLines.Length )
        {
            SceneManager.LoadScene("MainMenu_Scene");
        }
    }

    void ShowLine(int index)
    {
        if (index < currentLines.Length)
        {
            narratorText.text = currentLines[index];
            narratorText.gameObject.SetActive(true);
        }
        else if (endingTrigger && index >= currentLines.Length)
        {
            SceneManager.LoadScene("Credits");
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
    public void checkpointPA ()
    {
        currentLines = checkpointP;
        currentIndex = 0;
        ShowLine(currentIndex);
    }
    public void checkpointBA()
    {
        currentLines = checkpointC;
        currentIndex = 0;
        ShowLine(currentIndex);
    }

    public void beforeFinishText()
    {
        currentLines = beforeFinish;
        currentIndex = 0;
        ShowLine(currentIndex);
    }
    public void easterEgg1()
    {
        easterEggTrigger = true;
        currentLines = easterEgg;
        currentIndex = 0;
        ShowLine(currentIndex);
    }

    public void endingText()
    {
        endingTrigger = true;
        currentLines = finale;
        currentIndex = 0;
        ShowLine(currentIndex);
    }
}
