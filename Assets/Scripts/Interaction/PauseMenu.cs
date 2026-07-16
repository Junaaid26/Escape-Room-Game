using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;
    public GameObject gameplayHUD;

    [Header("Player")]
    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    private bool isPaused = false;

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (gameplayHUD != null)
            gameplayHUD.SetActive(true);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // ------------------------
    // PAUSE GAME
    // ------------------------
    public void PauseGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (gameplayHUD != null)
            gameplayHUD.SetActive(false);

        Time.timeScale = 0f;

        // Stop footsteps immediately
        if (AudioManager.Instance != null)
            AudioManager.Instance.StopFootsteps();

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (mouseLook != null)
            mouseLook.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isPaused = true;
    }

    // ------------------------
    // RESUME GAME
    // ------------------------
    public void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (gameplayHUD != null)
            gameplayHUD.SetActive(true);

        Time.timeScale = 1f;

        if (playerMovement != null)
            playerMovement.enabled = true;

        if (mouseLook != null)
            mouseLook.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
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

        // Hide pause panel
        if (pausePanel != null)
            pausePanel.SetActive(false);

        // Unlock cursor for menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Go directly to Main Menu (NO Intro Video)
        SceneManager.LoadScene("MainMenu");
    }
}