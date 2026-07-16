using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanelManager : MonoBehaviour
{
    public static VictoryPanelManager Instance;

    [Header("UI")]
    public GameObject victoryPanel;
    public GameObject gameplayHUD;

    [Header("Player")]
    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    [Header("Level Settings")]
    public int nextLevelIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    // ------------------------
    // SHOW VICTORY PANEL
    // ------------------------
    public void ShowVictory()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (gameplayHUD != null)
            gameplayHUD.SetActive(false);

        Time.timeScale = 0f;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (mouseLook != null)
            mouseLook.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayVictory();

        // ==============================
        // SAVE PROGRESS IMMEDIATELY
        // ==============================
        // If there is another level, unlock it as soon as
        // the victory panel appears.
        if (nextLevelIndex > 0)
        {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevelIndex);
            PlayerPrefs.Save();

            Debug.Log("Progress Saved! Next Unlocked Level: " + nextLevelIndex);
        }
    }

    // ------------------------
    // NEXT LEVEL
    // ------------------------
    public void NextLevel()
    {
        Time.timeScale = 1f;

        if (nextLevelIndex > 0)
        {
            LoadingManager.sceneToLoad = nextLevelIndex;
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            if (FinishPanelManager.Instance != null)
            {
                FinishPanelManager.Instance.ShowFinishPanel();
            }
        }
    }

    // ------------------------
    // RESTART LEVEL
    // ------------------------
    public void RestartLevel()
    {
        Time.timeScale = 1f;

        LoadingManager.sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("LoadingScene");
    }

    // ------------------------
    // MAIN MENU
    // ------------------------
    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}