using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rect;
    private Vector3 originalScale;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        originalScale = rect.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.localScale = originalScale * 1.05f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.localScale = originalScale;
    }
}