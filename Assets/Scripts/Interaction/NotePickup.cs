using UnityEngine;

public class NotePickup : MonoBehaviour
{
    public Transform player;
    public float distance = 1.5f;

    private bool picked = false;

    void Update()
    {
        if (picked)
            return;

        float d =
            Vector3.Distance(player.position,
                             transform.position);

        if (d <= distance &&
            Input.GetKeyDown(KeyCode.E))
        {
            picked = true;

            PuzzleManager.cluesFound++;

            Destroy(gameObject);
        }
    }
}