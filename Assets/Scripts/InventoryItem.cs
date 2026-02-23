using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public string itemID;
    public enum ItemType { Head, Body, Legs, Accessory }
    public ItemType type;

    [HideInInspector] public Transform parentAfterDrag;
    private Image image;

    void Awake() => image = GetComponent<Image>();


    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.clickCount == 2 && eventData.button == PointerEventData.InputButton.Left)
        {
            TryAutoEquip();
        }
    }

    private void TryAutoEquip()
    {
        InventorySlot currentSlot = transform.parent.GetComponent<InventorySlot>();
        if (currentSlot == null) return;


        bool lookingForEquipment = !currentSlot.isEquipmentSlot;


        InventorySlot[] allSlots = Object.FindObjectsByType<InventorySlot>(FindObjectsSortMode.None);
        InventorySlot targetSlot = null;

        foreach (var slot in allSlots)
        {

            if (slot.transform.childCount == 0)
            {
                if (lookingForEquipment)
                {

                    if (slot.isEquipmentSlot && slot.slotType == (InventoryItem.ItemType)this.type)
                    {
                        targetSlot = slot;
                        break;
                    }
                }
                else
                {

                    if (!slot.isEquipmentSlot)
                    {
                        targetSlot = slot;
                        break;
                    }
                }
            }
        }

        if (targetSlot != null)
        {

            bool isAcc = (this.type == ItemType.Accessory);
            AudioManager.instance.PlayEquip(isAcc);

            if (currentSlot.isEquipmentSlot)
            {
                currentSlot.DeactivateVisualByItem(this);
            }


            transform.SetParent(targetSlot.transform);
            transform.localPosition = Vector3.zero;
            parentAfterDrag = targetSlot.transform;


            if (targetSlot.isEquipmentSlot)
            {
                targetSlot.UpdateVisualState(this.itemID);
            }
        }
    }
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

        InventorySlot slot = parentAfterDrag.GetComponent<InventorySlot>();
        if (slot != null && slot.isEquipmentSlot)
        {
            slot.UpdateVisualState(this.itemID);
        }
    }
}