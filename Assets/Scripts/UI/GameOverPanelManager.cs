using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelManager : MonoBehaviour
{
    public static GameOverPanelManager Instance;

    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject gameplayHUD;

    [Header("Player")]
    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    // ------------------------
    // SHOW GAME OVER PANEL
    // ------------------------
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

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
            AudioManager.Instance.PlayGameOver();
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

        LoadingManager.sceneToLoad = 0;
        SceneManager.LoadScene("LoadingScene");
    }

    // ------------------------
    // QUIT GAME
    // ------------------------
    public void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}