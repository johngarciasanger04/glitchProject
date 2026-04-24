using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AreaText : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI areaText;

    [Header("Settings")]
    public float fadeInTime = 2f;
    public float displayTime = 3f;
    public float fadeOutTime = 2f;

    void Start()
    {
        if (areaText != null)
        {
            // Auto-detect scene name and set display text
            string sceneName = SceneManager.GetActiveScene().name;
            string displayName = GetDisplayName(sceneName);
            
            areaText.text = displayName;
            
            // Ensure the text starts completely transparent
            Color startColor = areaText.color;
            startColor.a = 0f;
            areaText.color = startColor;

            // Kick off the fading sequence
            StartCoroutine(RevealSequence());
        }
    }

    string GetDisplayName(string sceneName)
    {
        // Map scene names to pretty display names
        switch (sceneName)
        {
            case "Level 1":
                return "Testing Grounds";
            case "Pressure plate level":
                return "Pressure Plates";
            case "Maze Level":
                return "The Maze";
            case "CityLevel":
                return "The City";
            case "Perspective_Scene":
                return "Perspective Shift";
            default:
                return sceneName; // Fallback to scene name
        }
    }

    IEnumerator RevealSequence()
    {
        Color textColor = areaText.color;
        float elapsedTime = 0f;

        // 1. FADE IN
        while (elapsedTime < fadeInTime)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(elapsedTime / fadeInTime);
            areaText.color = textColor;
            yield return null;
        }

        // 2. HOLD ON SCREEN
        yield return new WaitForSeconds(displayTime);

        // 3. FADE OUT
        elapsedTime = 0f;
        while (elapsedTime < fadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(1f - (elapsedTime / fadeOutTime));
            areaText.color = textColor;
            yield return null;
        }

        // 4. CLEANUP
        gameObject.SetActive(false);
    }
}