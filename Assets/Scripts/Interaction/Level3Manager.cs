using UnityEngine;
using TMPro;

public class Level3Manager : MonoBehaviour
{
    public static Level3Manager Instance;

    // Clue Progress
    public static bool gotM;
    public static bool gotA;
    public static bool got2;
    public static bool got6;

    // Progress
    public static bool safeUnlocked = false;
    public static bool keyCollected = false;

    [Header("HUD")]
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI safeCodeText;
    public TextMeshProUGUI keyStatusText;

    [Header("Pickup Notification")]
    public GameObject pickupNotification;
    public TextMeshProUGUI notificationText;

    [Header("Objects")]
    public GameObject masterKey;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gotM = false;
        gotA = false;
        got2 = false;
        got6 = false;

        safeUnlocked = false;
        keyCollected = false;

        if (masterKey != null)
            masterKey.SetActive(false);

        if (objectiveText != null)
            objectiveText.text = "Find all Safe Code Pieces";

        if (keyStatusText != null)
            keyStatusText.text = "[ ] Exit Key : Hidden";

        if (pickupNotification != null)
            pickupNotification.SetActive(false);

        UpdateSafeCode();
    }

    void Update()
    {
        UpdateSafeCode();

        // Only update objectives before the safe is unlocked
        if (!safeUnlocked && !keyCollected)
        {
            if (AllPiecesCollected())
            {
                if (objectiveText != null)
                    objectiveText.text = "Go to the Safe";
            }
        }
    }

    void UpdateSafeCode()
    {
        string m = gotM ? "M" : " ";
        string a = gotA ? "A" : " ";
        string two = got2 ? "2" : " ";
        string six = got6 ? "6" : " ";

        if (safeCodeText != null)
        {
            safeCodeText.text =
                "[" + m + "]   " +
                "[" + a + "]   " +
                "[" + two + "]   " +
                "[" + six + "]";
        }
    }

    public bool AllPiecesCollected()
    {
        return gotM && gotA && got2 && got6;
    }

    public void ShowPickup(string clue)
    {
        if (pickupNotification != null)
            pickupNotification.SetActive(true);

        if (notificationText != null)
        {
            notificationText.gameObject.SetActive(true);
            notificationText.text = "✓ " + clue + " Collected";
        }

        CancelInvoke(nameof(HidePickup));
        Invoke(nameof(HidePickup), 2f);
    }

    void HidePickup()
    {
        if (pickupNotification != null)
            pickupNotification.SetActive(false);
    }

    // Called from SafeController when MA26 is correct
    public void SafeUnlocked()
    {
        safeUnlocked = true;

        if (objectiveText != null)
            objectiveText.text = "Find the Exit Key";

        if (keyStatusText != null)
            keyStatusText.text = "[ ] Exit Key : Not Found";

        if (masterKey != null)
            masterKey.SetActive(true);

        Debug.Log("Safe Unlocked");
    }

    // Called from MasterKeyPickup
    public void KeyCollected()
    {
        keyCollected = true;

        if (keyStatusText != null)
            keyStatusText.text = "✓ Exit Key : Acquired";

        if (objectiveText != null)
        {
            objectiveText.text =
                "Move Towards the Exit Door\nEscape the Building";
        }

        Debug.Log("Key Collected");
    }
}