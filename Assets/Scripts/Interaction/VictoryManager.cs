using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("VictoryManager Started");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Click Detected");
        }
    }

    public void RestartLevel()
    {
        Debug.Log("Restart Pressed");

        string level = PlayerPrefs.GetString("LastLevel", "");

        Debug.Log("LastLevel = " + level);

        if (!string.IsNullOrEmpty(level))
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            Debug.LogError("LastLevel is empty!");
        }
    }

    public void MainMenu()
    {
        Debug.Log("Main Menu Pressed");
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        Debug.Log("Next Level Pressed");

        int nextLevel = PlayerPrefs.GetInt("NextLevelIndex", -1);

        Debug.Log("NextLevelIndex = " + nextLevel);

        if (nextLevel == -1)
        {
            Debug.LogError("NextLevelIndex not found!");
            return;
        }

        Debug.Log("Trying to load scene build index: " + nextLevel);

        SceneManager.LoadScene(nextLevel);
    }
}