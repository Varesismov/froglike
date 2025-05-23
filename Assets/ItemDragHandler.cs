using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    [HideInInspector] public Transform originalParent;


    [HideInInspector] public bool wasDropped = false;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        Debug.Log("Original parent: " + originalParent.name);

        canvasGroup.blocksRaycasts = false;
        transform.SetParent(transform.root); // Przenosimy do najwy¿szego canvasu ¿eby by³o nad wszystkim
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //canvasGroup.blocksRaycasts = true;
        //transform.SetParent(originalParent); // wracamy jeœli nie wyl¹dowaliœmy na slocie
        //rectTransform.anchoredPosition = Vector2.zero;
        canvasGroup.blocksRaycasts = true;

        if (!wasDropped)
        {
            transform.SetParent(originalParent); // wracamy jeœli nie wyl¹dowaliœmy na slocie
            rectTransform.anchoredPosition = Vector2.zero;
        }

        wasDropped = false; // reset flagi po zakoñczeniu przeci¹gania
    }
}
