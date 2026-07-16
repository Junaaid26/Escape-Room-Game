using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalDoorController : MonoBehaviour
{
    public Transform player;

    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    public GameObject interactionPanel;
    public TMPro.TextMeshProUGUI interactionText;

    public GameObject levelCompleteText;

    public float interactionDistance = 3f;

    bool escaped = false;

    void Start()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (levelCompleteText != null)
            levelCompleteText.SetActive(false);
    }

    void Update()
    {
        if (escaped)
            return;

        if (!MasterKeyPickup.hasMasterKey)
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
            if (interactionPanel != null)
                interactionPanel.SetActive(true);

            if (interactionText != null)
                interactionText.text = "[E] Escape";

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(EscapeRoom());
            }
        }
        else
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(false);
        }
    }

    IEnumerator EscapeRoom()
    {
        escaped = true;

        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (mouseLook != null)
            mouseLook.enabled = false;

        if (levelCompleteText != null)
            levelCompleteText.SetActive(true);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDoorOpen();

        yield return new WaitForSeconds(2f);

        PlayerPrefs.SetString(
            "LastLevel",
            SceneManager.GetActiveScene().name);

        PlayerPrefs.SetInt(
            "NextLevelIndex",
            0);

        PlayerPrefs.Save();

        VictoryPanelManager.Instance.ShowVictory();
    }
}