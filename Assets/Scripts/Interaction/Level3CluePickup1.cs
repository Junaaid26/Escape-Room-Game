using UnityEngine;
using System.Collections;

public class Level3CluePickup : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public Transform pickupPoint;

    [Header("HUD")]
    public GameObject interactionPanel;

    [Header("Notification")]
    public GameObject pickupMessage;

    [Header("Clue")]
    public string clueValue;

    public float pickupDistance = 3f;

    private bool collected = false;

    void Start()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        if (pickupMessage != null)
            pickupMessage.SetActive(false);
    }

    void Update()
    {
        if (collected)
            return;

        if (player == null)
        {
            Debug.LogError(gameObject.name + " : Player is NOT assigned!");
            return;
        }

        if (pickupPoint == null)
        {
            Debug.LogError(gameObject.name + " : Pickup Point is NOT assigned!");
            return;
        }

        float distance = Vector3.Distance(
            player.position,
            pickupPoint.position);

        // DEBUG
        Debug.Log(gameObject.name + " Distance = " + distance);

        if (distance <= pickupDistance)
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(CollectClue());
            }
        }
        else
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(false);
        }
    }

    IEnumerator CollectClue()
{
    collected = true;

    if (interactionPanel != null)
        interactionPanel.SetActive(false);

    switch (clueValue)
    {
        case "M":
            Level3Manager.gotM = true;
            break;

        case "A":
            Level3Manager.gotA = true;
            break;

        case "2":
            Level3Manager.got2 = true;
            break;

        case "6":
            Level3Manager.got6 = true;
            break;
    }

    // Play clue pickup sound
    if (AudioManager.Instance != null)
        AudioManager.Instance.PlayCluePickup();

    // Show pickup notification
    if (Level3Manager.Instance != null)
        Level3Manager.Instance.ShowPickup(clueValue);

    // Hide clue mesh
    Renderer[] renderers = GetComponentsInChildren<Renderer>();

    foreach (Renderer r in renderers)
        r.enabled = false;

    // Disable colliders
    Collider[] colliders = GetComponentsInChildren<Collider>();

    foreach (Collider c in colliders)
        c.enabled = false;

    yield return new WaitForSeconds(0.5f);

    gameObject.SetActive(false);
}
}