using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    // Scene to load
    public static int sceneToLoad;

    [Header("UI")]
    public Slider loadingSlider;
    public TextMeshProUGUI loadingText;

    [Header("Settings")]
    public float minimumLoadingTime = 1.5f;

    private IEnumerator Start()
    {
        // ------------------------------------
        // SAVE HIGHEST UNLOCKED LEVEL
        // ------------------------------------
        // Only save gameplay levels (Level1=3, Level2=4, Level3=5)
        if (sceneToLoad >= 3)
        {
            int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevel", 3);

            // Only update if this level is higher than the previous one
            if (sceneToLoad > currentUnlocked)
            {
                PlayerPrefs.SetInt("UnlockedLevel", sceneToLoad);
                PlayerPrefs.Save();

                Debug.Log("Unlocked Level Saved: " + sceneToLoad);
            }
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        float elapsedTime = 0f;

        if (loadingSlider != null)
            loadingSlider.value = 0f;

        while (elapsedTime < minimumLoadingTime)
        {
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsedTime / minimumLoadingTime);

            if (loadingSlider != null)
                loadingSlider.value = progress;

            if (loadingText != null)
                loadingText.text = "Loading " + Mathf.RoundToInt(progress * 100f) + "%";

            yield return null;
        }

        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        operation.allowSceneActivation = true;
    }
}