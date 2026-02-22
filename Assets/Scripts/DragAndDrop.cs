using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private ItemData itemData;

    public Image headSlot;
    public Image bodySlot;
    public Image legsSlot;
    public Image AccSlot1;
    public Image AccSlot2;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemData = GetComponent<ItemData>();
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (IsOverCharacter(eventData))
        {
            EquipItem();
        }

        rectTransform.anchoredPosition = startPosition;
    }

    bool IsOverCharacter(PointerEventData data)
    {

        foreach (var obj in data.hovered)
        {
            if (obj.name == "CharacterImage") return true;
        }
        return false;
    }

    void EquipItem()
    {

        switch (itemData.type)
        {
            case ItemData.ItemType.Head:
                headSlot.sprite = itemData.equipmentSprite;
                headSlot.color = Color.white;
                break;
            case ItemData.ItemType.Body:
                bodySlot.sprite = itemData.equipmentSprite;
                bodySlot.color = Color.white;
                break;
            case ItemData.ItemType.Legs:
                legsSlot.sprite = itemData.equipmentSprite;
                legsSlot.color = Color.white;
                break;
        }
    }
}