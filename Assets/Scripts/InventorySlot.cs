using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem.ItemType slotType;
    public bool isEquipmentSlot;

    [Header("UI Hide Settings")]
    public Image hideButtonImage;
    public Color hiddenColor = new Color(0.3f, 0.3f, 0.3f, 1f);
    public Color visibleColor = Color.white;

    private bool isVisualHidden = false;

    [Header("Auto-assigned")]
    public Transform characterRoot;

    private void Awake()
    {
        if (characterRoot == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) characterRoot = player.transform;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        InventoryItem newItem = dropped.GetComponent<InventoryItem>();
        if (newItem == null) return;


        if (isEquipmentSlot && newItem.type != slotType) return;

        InventorySlot sourceSlot = newItem.parentAfterDrag.GetComponent<InventorySlot>();
        InventoryItem existingItem = GetComponentInChildren<InventoryItem>();


        if (AudioManager.instance != null)
        {
            if (isEquipmentSlot)
            {

                bool isAcc = (newItem.type == InventoryItem.ItemType.Accessory);
                AudioManager.instance.PlayEquip(isAcc);
            }
            else
            {
                AudioManager.instance.PlayClick();
            }
        }

        if (existingItem != null)
        {
            if (sourceSlot != null && sourceSlot.isEquipmentSlot && existingItem.type != sourceSlot.slotType) return;

            existingItem.transform.SetParent(newItem.parentAfterDrag);
            existingItem.transform.localPosition = Vector3.zero;

            if (sourceSlot != null)
            {
                if (sourceSlot.isEquipmentSlot) sourceSlot.UpdateVisualState(existingItem.itemID);
                else sourceSlot.DeactivateVisualByItem(existingItem);
            }
        }
        else
        {
            if (sourceSlot != null && sourceSlot.isEquipmentSlot && !this.isEquipmentSlot)
            {
                sourceSlot.DeactivateVisualByItem(newItem);
            }
        }

        newItem.parentAfterDrag = transform;

        if (isEquipmentSlot)
        {
            UpdateVisualState(newItem.itemID);
        }
    }

    public void ToggleItemVisibility()
    {
        if (!isEquipmentSlot) return;

        isVisualHidden = !isVisualHidden;

        if (hideButtonImage != null)
        {
            hideButtonImage.color = isVisualHidden ? hiddenColor : visibleColor;
        }

        InventoryItem currentItem = GetComponentInChildren<InventoryItem>();
        if (currentItem != null)
        {
            UpdateVisualState(currentItem.itemID);
        }
    }

    public void UpdateVisualState(string id)
    {
        if (characterRoot == null) return;

        foreach (Transform child in characterRoot)
        {
            if (slotType == InventoryItem.ItemType.Head && (child.name.Contains("Helmet") || child.name.Contains("Helm")))
                child.gameObject.SetActive(false);
            else if (slotType == InventoryItem.ItemType.Body && (child.name.Contains("Chestplate") || child.name.Contains("Armor")))
                child.gameObject.SetActive(false);
            else if (slotType == InventoryItem.ItemType.Legs && (child.name.Contains("Leggings") || child.name.Contains("Legs")))
                child.gameObject.SetActive(false);
            // ДОБАВЛЕНО: Логика для аксессуаров
            else if (slotType == InventoryItem.ItemType.Accessory)
            {
                InventoryItem currentItem = GetComponentInChildren<InventoryItem>();
                if (currentItem != null && child.name == currentItem.itemID)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        if (!isVisualHidden)
        {
            Transform visual = characterRoot.Find(id);
            if (visual != null) visual.gameObject.SetActive(true);
        }
    }

    public void ActivateVisual(string id) => UpdateVisualState(id);

    public void DeactivateVisual()
    {
        if (characterRoot == null) return;
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        if (item != null) DeactivateVisualByItem(item);
    }

    public void DeactivateVisualByItem(InventoryItem item)
    {
        if (characterRoot == null) return;
        Transform visual = characterRoot.Find(item.itemID);
        if (visual != null) visual.gameObject.SetActive(false);
    }
}