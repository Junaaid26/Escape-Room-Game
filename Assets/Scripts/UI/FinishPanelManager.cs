using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPanelManager : MonoBehaviour
{
    public static FinishPanelManager Instance;

    [Header("UI")]
    public GameObject finishPanel;

    [Header("Player")]
    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (finishPanel != null)
            finishPanel.SetActive(false);
    }

    // ------------------------
    // SHOW FINISH PANEL
    // ------------------------
    public void ShowFinishPanel()
    {
        if (finishPanel != null)
            finishPanel.SetActive(true);

        Time.timeScale = 0f;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (mouseLook != null)
            mouseLook.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayVictory();
    }

    // ------------------------
    // PLAY AGAIN
    // ------------------------
    public void PlayAgain()
    {
        Time.timeScale = 1f;

        // Reset progress back to Level 1
        PlayerPrefs.SetInt("UnlockedLevel", 3);
        PlayerPrefs.Save();

        LoadingManager.sceneToLoad = 3;
        SceneManager.LoadScene("LoadingScene");
    }

    // ------------------------
    // MAIN MENU
    // ------------------------
    public void MainMenu()
    {
        Time.timeScale = 1f;

        // Reset progress back to Level 1
        PlayerPrefs.SetInt("UnlockedLevel", 3);
        PlayerPrefs.Save();

        // Go to the Intro Scene
        SceneManager.LoadScene("IntroScene");
    }
}