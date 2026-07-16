using UnityEngine;
using TMPro;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public static bool hasKey = false;

    [Header("Player")]
    public Transform player;
    public float openDistance = 4f;

    [Header("HUD")]
    public GameObject interactionPanel;
    public TextMeshProUGUI interactionText;

    private bool levelFinished = false;

    void Start()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);
    }

    void Update()
    {
        if (levelFinished)
            return;

        // Player cannot use the door until the key is collected
        if (!hasKey)
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(false);

            return;
        }

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= openDistance)
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(true);

            if (interactionText != null)
                interactionText.text = "[E] Escape";

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(FinishLevel());
            }
        }
        else
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(false);
        }
    }

    IEnumerator FinishLevel()
    {
        levelFinished = true;

        // Hide interaction panel
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        // Disable Player Movement
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        // Disable Mouse Look
        MouseLook mouse = player.GetComponentInChildren<MouseLook>();
        if (mouse != null)
            mouse.enabled = false;

        // Play door opening sound
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayDoorOpen();

        // Wait briefly so the player hears the door sound
        yield return new WaitForSeconds(0.6f);

        // Show Victory Panel
        if (VictoryPanelManager.Instance != null)
        {
            VictoryPanelManager.Instance.ShowVictory();
        }
    }
}