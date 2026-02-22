using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string itemID;
    public enum ItemType { Head, Body, Legs, Accessory }
    public ItemType type;

    [HideInInspector] public Transform parentAfterDrag;
    private Image image;

    void Awake() => image = GetComponent<Image>();

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;

        InventorySlot currentSlot = parentAfterDrag.GetComponent<InventorySlot>();
        if (currentSlot != null && currentSlot.isEquipmentSlot)
        {
            currentSlot.DeactivateVisual();
        }

        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) => transform.position = Input.mousePosition;

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        transform.localPosition = Vector3.zero;
    }
}