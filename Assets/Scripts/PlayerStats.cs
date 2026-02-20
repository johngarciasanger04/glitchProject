using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Stats")]
    public int totalPoints = 0;
    public int itemsCollected = 0;
    
    [Header("UI (Optional)")]
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI itemsText;
    
    private void Start()
    {
        UpdateUI();
    }
    
    public void AddPoints(int points)
    {
        totalPoints += points;
        itemsCollected++;
        
        Debug.Log($"Collected item! Total: {itemsCollected} | Points: {totalPoints}");
        
        UpdateUI();
    }
    
    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {totalPoints}";
        }
        
        if (itemsText != null)
        {
            itemsText.text = $"Items: {itemsCollected}";
        }
    }
    
    // Optional: Get stats from other scripts
    public int GetScore() => totalPoints;
    public int GetItemCount() => itemsCollected;
}