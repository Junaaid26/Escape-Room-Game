using UnityEngine;
using System.Collections;

public class MasterKeyPickup : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("HUD")]
    public GameObject interactionPanel;
    public TMPro.TextMeshProUGUI interactionText;

    public GameObject pickupNotification;

    public float pickupDistance = 3f;

    bool collected = false;

    public static bool hasMasterKey = false;

    void Start()
    {
        hasMasterKey = false;

        if (interactionPanel != null)
            interactionPanel.SetActive(false);
    }

    void OnEnable()
    {
        collected = false;

        if (interactionPanel != null)
            interactionPanel.SetActive(false);
    }

    void Update()
    {
        if (collected)
            return;

        float distance =
            Vector3.Distance(
                player.position,
                transform.position);

        if (distance <= pickupDistance)
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(true);

            if (interactionText != null)
                interactionText.text = "[E] Pick Up Exit Key";

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(PickupKey());
            }
        }
        else
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(false);
        }
    }

    IEnumerator PickupKey()
    {
        collected = true;

        hasMasterKey = true;

        // Hide interaction panel
    if (interactionPanel != null)
        interactionPanel.SetActive(false);

    // Play pickup sound
    if (AudioManager.Instance != null)
        AudioManager.Instance.PlayKeyPickup();

    // Update Level 3 HUD
    if (Level3Manager.Instance != null)
    {
        // Show notification
        Level3Manager.Instance.ShowPickup("Exit Key");

        // Update inventory
        Level3Manager.Instance.KeyCollected();

        Debug.Log("Objective Updated Successfully");
    }

    // Hide the key model
    Renderer[] renderers =
        GetComponentsInChildren<Renderer>();

    foreach (Renderer r in renderers)
        r.enabled = false;

    // Disable colliders
    Collider[] colliders =
        GetComponentsInChildren<Collider>();

    foreach (Collider c in colliders)
        c.enabled = false;

    yield return new WaitForSeconds(1.5f);

    gameObject.SetActive(false);
    }
}