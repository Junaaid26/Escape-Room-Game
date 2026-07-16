using UnityEngine;
using System.Collections;
using TMPro;

public class CluePickup : MonoBehaviour
{
    [Header("Player")]
    public Transform player;
    public float pickupDistance = 3f;

    [Header("HUD")]
    public GameObject interactionPanel;
    public TextMeshProUGUI interactionText;

    [Header("Clue")]
    public string clueName;

    private bool collected = false;

    void Start()
    {
        if (interactionPanel != null)
            interactionPanel.SetActive(false);
    }

    void Update()
    {
        if (collected)
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= pickupDistance)
        {
            if (interactionPanel != null)
                interactionPanel.SetActive(true);

            if (interactionText != null)
                interactionText.text = "[E] Collect " + clueName;

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
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayCluePickup();
        }
        if (interactionPanel != null)
            interactionPanel.SetActive(false);

        // Tell PuzzleManager which clue was collected
        if (PuzzleManager.Instance != null)
        {
            PuzzleManager.Instance.CollectClue(clueName);
        }

        // Hide clue mesh
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
            renderer.enabled = false;

        // Disable colliders
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
            collider.enabled = false;

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}