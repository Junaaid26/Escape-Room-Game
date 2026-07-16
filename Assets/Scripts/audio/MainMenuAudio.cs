using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    public static MainMenuAudio Instance;

    public AudioSource audioSource;

    public AudioClip buttonClick;
    public AudioClip buttonHover;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(buttonClick);
    }

    public void PlayHover()
    {
        audioSource.PlayOneShot(buttonHover);
    }
}