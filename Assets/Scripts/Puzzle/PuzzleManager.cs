using UnityEngine;
using TMPro;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public static int cluesFound = 0;

    public static PuzzleManager Instance;

    [Header("Objective")]
    public TextMeshProUGUI objectiveText;

    [Header("Inventory")]
    public TextMeshProUGUI mapStatus;
    public TextMeshProUGUI bookStatus;
    public TextMeshProUGUI fileStatus;
    public TextMeshProUGUI letterStatus;
    public TextMeshProUGUI clueCounter;
    public TextMeshProUGUI keyStatus;

    [Header("Notification")]
    public GameObject notificationPanel;
    public TextMeshProUGUI notificationText;

    [Header("Key")]
    public GameObject finalKey;

    private bool keySpawned = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cluesFound = 0;
        keySpawned = false;

        if (finalKey != null)
            finalKey.SetActive(false);

        if (notificationPanel != null)
            notificationPanel.SetActive(false);

        objectiveText.text = "Find all 4 Clues";

        mapStatus.text = "☐ Map : Not Found";
        bookStatus.text = "☐ Book : Not Found";
        fileStatus.text = "☐ File : Not Found";
        letterStatus.text = "☐ Letter : Not Found";

        clueCounter.text = "Clues : 0 / 4";

        keyStatus.text = "☐ Exit Key : Hidden";
    }

    public void CollectClue(string clueName)
    {
        cluesFound++;

        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.ClueCollected();
        }

        clueCounter.text = "Clues : " + cluesFound + " / 4";

        switch (clueName)
        {
            case "Map":
                mapStatus.text = "☑ Map : Collected";
                break;

            case "Book":
                bookStatus.text = "☑ Book : Collected";
                break;

            case "File":
                fileStatus.text = "☑ File : Collected";
                break;

            case "Letter":
                letterStatus.text = "☑ Letter : Collected";
                break;
        }

        StartCoroutine(ShowNotification(clueName + " Collected"));

        if (cluesFound >= 4 && !keySpawned)
        {
            keySpawned = true;

            objectiveText.text = "Find the Exit Key";

            keyStatus.text = "☐ Exit Key : Not Found";

            if (finalKey != null)
                finalKey.SetActive(true);

            StartCoroutine(ShowNotification("Exit Key Revealed"));
        }
    }

    IEnumerator ShowNotification(string message)
    {
        if (notificationPanel != null)
            notificationPanel.SetActive(true);

        if (notificationText != null)
            notificationText.text = "✓ " + message;

        yield return new WaitForSeconds(2f);

        if (notificationPanel != null)
            notificationPanel.SetActive(false);
    }
}