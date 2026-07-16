using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;

    public float playTime;
    public int cluesCollected;
    public int levelCompleted;
    public int playerDeaths;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        playTime += Time.deltaTime;
    }

    public void ClueCollected()
    {
        cluesCollected++;
    }

    public void LevelCompleted()
    {
        levelCompleted++;
    }

    public void PlayerDied()
    {
        playerDeaths++;
    }

    public void ShowAnalytics()
    {
        Debug.Log("========== ANALYTICS ==========");
        Debug.Log("Play Time : " + playTime.ToString("F1") + " Seconds");
        Debug.Log("Clues Collected : " + cluesCollected);
        Debug.Log("Level Completed : " + levelCompleted);
        Debug.Log("Player Deaths : " + playerDeaths);
        Debug.Log("===============================");
    }
}