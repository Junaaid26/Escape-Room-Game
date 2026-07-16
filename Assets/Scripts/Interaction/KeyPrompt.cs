using UnityEngine;
using System.Collections;
using TMPro;

public class KeyPrompt : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public float pickupDistance = 3f;

    [Header("HUD")]
    public GameObject interactionPanel;
    public TextMeshProUGUI interactionText;

    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI keyStatusText;

    public GameObject pickupNotification;
    public TextMeshProUGUI notificationText;

    private bool keyPicked = false;

    void Start()
    {
        DoorController.hasKey = false;

        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (pickupNotification != null)
            pickupNotification.SetActive(false);

        // DO NOT change Objective or Inventory here.
        // PuzzleManager controls them until the key is picked up.
    }

    void Update()
    {
        if (keyPicked)
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= pickupDistance)
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(true);

            if (interactionText != null)
                interactionText.text = "[E] Pick Up Key";

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(PickUpKey());
            }
        }
        else
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(false);
        }
    }

    IEnumerator PickUpKey()
    {
        keyPicked = true;

        DoorController.hasKey = true;
        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayKeyPickup();
        }

        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        // Update Objective
        if (objectiveText != null)
        {
            objectiveText.text =
                "Go to the Exit Door.Escape the Room";
        }

        // Update Inventory
        if (keyStatusText != null)
        {
            keyStatusText.text =
                "☑ Exit Key : Acquired";
        }

        // Notification
        if (pickupNotification != null)
            pickupNotification.SetActive(true);

        if (notificationText != null)
            notificationText.text =
                "✓ Exit Key Acquired";

        // Hide Key Mesh
        MeshRenderer[] renderers =
            GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
            renderer.enabled = false;

        // Disable Colliders
        Collider[] colliders =
            GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
            collider.enabled = false;

        yield return new WaitForSeconds(2f);

        if (pickupNotification != null)
            pickupNotification.SetActive(false);

        Destroy(gameObject);
    }
}