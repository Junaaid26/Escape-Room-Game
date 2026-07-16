using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider volumeSlider;
    public Slider sensitivitySlider;

    [Header("Player")]
    public MouseLook mouseLook;

    private void Start()
    {
        // Load saved settings
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", 150f);

        // Set slider values
        if (volumeSlider != null)
            volumeSlider.value = savedVolume;

        if (sensitivitySlider != null)
            sensitivitySlider.value = savedSensitivity;

        // Apply settings
        AudioListener.volume = savedVolume;

        if (mouseLook != null)
            mouseLook.mouseSensitivity = savedSensitivity;
    }

    // -----------------------
    // Volume
    // -----------------------
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    // -----------------------
    // Mouse Sensitivity
    // -----------------------
    public void SetSensitivity(float sensitivity)
    {
        if (mouseLook != null)
            mouseLook.mouseSensitivity = sensitivity;

        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save();
    }
}