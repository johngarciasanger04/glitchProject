using System.Collections;
using UnityEngine;
using TMPro; // Required for TextMeshPro!

public class AreaTextReveal : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI areaText;

    [Header("Settings")]
    public string areaName = "New Area";
    public float fadeInTime = 2f;
    public float displayTime = 3f;
    public float fadeOutTime = 2f;

    void Start()
    {
        if (areaText != null)
        {
            // Set the specific text for this level
            areaText.text = areaName;
            
            // Ensure the text starts completely transparent
            Color startColor = areaText.color;
            startColor.a = 0f;
            areaText.color = startColor;

            // Kick off the fading sequence
            StartCoroutine(RevealSequence());
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
            yield return null; // Wait for the next frame
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

        // 4. CLEANUP (Turn off the text object to save performance)
        gameObject.SetActive(false);
    }
}