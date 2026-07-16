using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    public GameObject aboutPanel;

    [Header("Sound")]
    public Toggle soundToggle;

    private void Start()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (controlsPanel != null)
            controlsPanel.SetActive(false);

        if (aboutPanel != null)
            aboutPanel.SetActive(false);

        // Load saved sound setting
        bool soundOn = PlayerPrefs.GetInt("Sound", 1) == 1;

        AudioListener.volume = soundOn ? 1f : 0f;

        if (soundToggle != null)
        {
            soundToggle.isOn = soundOn;
        }
    }

    // ------------------------
    // PLAY GAME
    // ------------------------
    public void PlayGame()
    {
        Debug.Log("PLAY BUTTON PRESSED");

        // If no progress has been saved yet, start from Level 1 (Build Index 3)
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 3);

        Debug.Log("Loading Level Build Index: " + unlockedLevel);

        LoadingManager.sceneToLoad = unlockedLevel;

        SceneManager.LoadScene("LoadingScene");
    }

    // ------------------------
    // SOUND TOGGLE
    // ------------------------
    public void ToggleSound(bool isOn)
    {
        Debug.Log("Toggle Changed: " + isOn);

        AudioListener.volume = isOn ? 1f : 0f;

        Debug.Log("Current Volume: " + AudioListener.volume);

        PlayerPrefs.SetInt("Sound", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    // ------------------------
    // SETTINGS
    // ------------------------
    public void OpenSettings()
    {
        CloseAllPanels();

        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    // ------------------------
    // CONTROLS
    // ------------------------
    public void OpenControls()
    {
        CloseAllPanels();

        if (controlsPanel != null)
            controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        if (controlsPanel != null)
            controlsPanel.SetActive(false);
    }

    // ------------------------
    // ABOUT
    // ------------------------
    public void OpenAbout()
    {
        CloseAllPanels();

        if (aboutPanel != null)
            aboutPanel.SetActive(true);
    }

    public void CloseAbout()
    {
        if (aboutPanel != null)
            aboutPanel.SetActive(false);
    }

    // ------------------------
    // CLOSE ALL PANELS
    // ------------------------
    public void CloseAllPanels()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);

        if (controlsPanel != null)
            controlsPanel.SetActive(false);

        if (aboutPanel != null)
            aboutPanel.SetActive(false);
    }

    // ------------------------
    // QUIT GAME
    // ------------------------
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}