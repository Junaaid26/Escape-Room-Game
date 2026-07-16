using UnityEngine;

public class SimpleSettings : MonoBehaviour
{
    public AudioSource backgroundMusic;

    public void ToggleMusic(bool isOn)
    {
        if (backgroundMusic != null)
            backgroundMusic.mute = !isOn;
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
    }
}