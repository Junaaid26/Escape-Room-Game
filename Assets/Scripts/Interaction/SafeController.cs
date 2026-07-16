using UnityEngine;
using TMPro;

public class SafeController : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    [Header("Interaction")]
    public GameObject interactionPanel;
    public TextMeshProUGUI interactionText;

    [Header("Safe UI")]
    public GameObject safePanel;
    public TMP_InputField codeInput;
    public TextMeshProUGUI messageText;

    [Header("Objects")]
    public GameObject masterKey;

    [Header("Settings")]
    public float interactionDistance = 3f;

    private bool safeOpened = false;
    private bool panelOpen = false;

    void Start()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (safePanel != null)
            safePanel.SetActive(false);

        if (messageText != null)
            messageText.text = "";

        if (masterKey != null)
            masterKey.SetActive(false);
    }

    void Update()
    {
    if (safeOpened)
        return;

    if (!Level3Manager.Instance.AllPiecesCollected())
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        return;
    }

    float distance =
        Vector3.Distance(
            player.position,
            transform.position);

    if (distance <= interactionDistance)
    {
        if (interactionText != null)
            interactionText.text = "[E] Unlock Safe";

        if (!panelOpen && interactionPanel != null)
            interactionPanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenSafe();
        }
    }
    else
    {
        if (!panelOpen && interactionPanel != null)
            interactionPanel.SetActive(false);
    }
    }

    void OpenSafe()
    {
        panelOpen = true;

        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (safePanel != null)
            safePanel.SetActive(true);

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (mouseLook != null)
            mouseLook.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (messageText != null)
            messageText.text = "";

        if (codeInput != null)
        {
            codeInput.text = "";
            codeInput.Select();
            codeInput.ActivateInputField();
        }
    }
        public void CheckCode()
    {
        if (codeInput == null)
            return;

        string enteredCode = codeInput.text.Trim().ToUpper();

if (enteredCode == "MA26")
{
    safeOpened = true;
    panelOpen = false;

    if (safePanel != null)
        safePanel.SetActive(false);

    if (masterKey != null)
        masterKey.SetActive(true);

    if (Level3Manager.Instance != null)
        Level3Manager.Instance.SafeUnlocked();

    if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlayCorrectCode();
        AudioManager.Instance.PlaySafeUnlock();
    }

    if (playerMovement != null)
        playerMovement.enabled = true;

    if (mouseLook != null)
        mouseLook.enabled = true;

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    Debug.Log("Safe Opened Successfully");
}
else
{
    if (AudioManager.Instance != null)
        AudioManager.Instance.PlayIncorrectCode();

    if (messageText != null)
        messageText.text = "Incorrect Code";

    codeInput.text = "";
    codeInput.Select();
    codeInput.ActivateInputField();

    CancelInvoke();
    Invoke(nameof(HideMessage), 2f);
}
    }

    void HideMessage()
    {
        if (messageText != null)
            messageText.text = "";
    }

    public void CloseSafe()
    {
        panelOpen = false;

        if (safePanel != null)
            safePanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;

        if (mouseLook != null)
            mouseLook.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!safeOpened)
        {
            float distance =
                Vector3.Distance(
                    player.position,
                    transform.position);

            if (distance <= interactionDistance &&
                interactionPanel != null)
            {
                interactionPanel.SetActive(true);
            }
        }
    }
}