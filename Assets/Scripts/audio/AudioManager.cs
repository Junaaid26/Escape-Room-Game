using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource footstepSource;

    [Header("Player Sounds")]
    public AudioClip footstep;

    [Header("Level Sounds")]
    public AudioClip cluePickup;
    public AudioClip keyPickup;
    public AudioClip doorOpen;
    public AudioClip victory;
    public AudioClip gameOver;
    public AudioClip pauseOpen;
    public AudioClip buttonClick;
    public AudioClip buttonHover;
    public AudioClip safeUnlock;
    public AudioClip correctCode;
    public AudioClip incorrectCode;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // -------------------------
    // Sound Effects
    // -------------------------

    public void PlayKeyPickup()
    {
        if (keyPickup != null)
            sfxSource.PlayOneShot(keyPickup);
    }

    public void PlayDoorOpen()
    {
        if (doorOpen != null)
            sfxSource.PlayOneShot(doorOpen);
    }
    public void PlayCluePickup()
    {
        if (cluePickup != null)
            sfxSource.PlayOneShot(cluePickup);
    }

    public void PlayVictory()
    {
        if (victory != null)
            sfxSource.PlayOneShot(victory);
    }

    public void PlayGameOver()
    {
        if (gameOver != null)
            sfxSource.PlayOneShot(gameOver);
    }
    public void PlayPauseOpen()
    {
        if (pauseOpen != null)
            sfxSource.PlayOneShot(pauseOpen);
    }

    public void PlayButtonClick()
    {
        if (buttonClick != null)
            sfxSource.PlayOneShot(buttonClick);
    }

    public void PlayButtonHover()
    {
        if (buttonHover != null)
            sfxSource.PlayOneShot(buttonHover);
    }
   public void PlayCorrectCode()
    {
        Debug.Log("Playing Correct Code");
        sfxSource.PlayOneShot(correctCode);
    }

    public void PlayIncorrectCode()
    {
        Debug.Log("Playing Incorrect Code");
        sfxSource.PlayOneShot(incorrectCode);
    }

    public void PlaySafeUnlock()
    {
        Debug.Log("Playing Safe Unlock");
        sfxSource.PlayOneShot(safeUnlock);
    }
    // -------------------------
    // Footsteps
    // -------------------------

    public void StartFootsteps(float pitch = 1f)
    {
        if (footstep == null)
            return;

        if (!footstepSource.isPlaying)
        {
            footstepSource.clip = footstep;
            footstepSource.pitch = pitch;
            footstepSource.loop = true;
            footstepSource.Play();
        }
        else
        {
            footstepSource.pitch = pitch;
        }
    }

    public void StopFootsteps()
    {
        if (footstepSource.isPlaying)
        {
            footstepSource.Stop();
        }
    }
}