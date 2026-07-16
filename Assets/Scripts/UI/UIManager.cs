using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject interactionText;

    public void ShowText()
    {
        interactionText.SetActive(true);
    }

    public void HideText()
    {
        interactionText.SetActive(false);
    }
}