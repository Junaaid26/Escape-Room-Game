using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartLevel()
    {
        Debug.Log("Restart Pressed");

        string level = PlayerPrefs.GetString("LastLevel", "");

        if (!string.IsNullOrEmpty(level))
        {
            SceneManager.LoadScene(level);
        }
        else
        {
            Debug.LogError("LastLevel not found!");
        }
    }

    public void MainMenu()
    {
        Debug.Log("Main Menu Pressed");

        SceneManager.LoadScene("MainMenu");
    }
}