using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [Header("Volume Slider")]
    public Slider volumeSlider;

    private void Start()
    {
        // Load saved volume
        float savedVolume = PlayerPrefs.GetFloat("GameVolume", 1f);

        // Set slider value
        if (volumeSlider != null)
            volumeSlider.value = savedVolume;

        // Apply volume
        AudioListener.volume = savedVolume;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("GameVolume", volume);
        PlayerPrefs.Save();
    }
}