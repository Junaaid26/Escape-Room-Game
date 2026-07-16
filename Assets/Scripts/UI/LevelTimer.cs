using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    [Header("Timer")]
    public float timeRemaining = 300f;
    private bool timerRunning = true;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Timer Colors")]
    public Color normalColor = Color.white;
    public Color warningColor = new Color(1f, 0.84f, 0.41f);   // Gold
    public Color dangerColor = Color.red;

    private bool warningPlayed = false;

    void Update()
    {
        // Stop timer if game is paused
        if (Time.timeScale == 0f)
            return;

        if (!timerRunning)
            return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining < 0)
                timeRemaining = 0;

            UpdateTimer();
        }
        else
        {
            timerRunning = false;

            if (GameOverPanelManager.Instance != null)
            {
                GameOverPanelManager.Instance.ShowGameOver();
            }
        }
    }

    void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Normal Time
        if (timeRemaining > 60)
        {
            timerText.color = normalColor;
        }
        // Last Minute
        else if (timeRemaining > 30)
        {
            timerText.color = warningColor;

            if (!warningPlayed)
            {
                warningPlayed = true;

                // Optional: Play warning sound here
                // AudioManager.Instance.PlayTimerWarning();
            }
        }
        // Last 30 Seconds
        else
        {
            timerText.color = dangerColor;
        }
    }
}