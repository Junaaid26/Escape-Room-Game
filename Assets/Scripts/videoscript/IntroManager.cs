using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [Header("Video")]
    public VideoPlayer videoPlayer;

    [Header("Skip Key")]
    public KeyCode skipKey = KeyCode.Space;

    private bool skipped = false;

    private void Start()
    {
        videoPlayer.loopPointReached += VideoFinished;
        videoPlayer.Play();
    }

    private void Update()
    {
        if (!skipped && Input.GetKeyDown(skipKey))
        {
            SkipVideo();
        }
    }

    private void VideoFinished(VideoPlayer vp)
    {
        if (!skipped)
        {
            skipped = true;
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void SkipVideo()
    {
        skipped = true;
        videoPlayer.Stop();
        SceneManager.LoadScene("MainMenu");
    }
}