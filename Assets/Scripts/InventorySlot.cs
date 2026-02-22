using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem.ItemType slotType;
    public bool isEquipmentSlot;

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

        if (existingItem != null)
        {

            if (sourceSlot != null && sourceSlot.isEquipmentSlot && existingItem.type != sourceSlot.slotType) return;

            existingItem.transform.SetParent(newItem.parentAfterDrag);
            existingItem.transform.localPosition = Vector3.zero;

            if (sourceSlot != null)
            {
                if (sourceSlot.isEquipmentSlot) sourceSlot.ActivateVisual(existingItem.itemID);
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
            ActivateVisual(newItem.itemID);
        }
    }

    public void ActivateVisual(string id)
    {
        if (characterRoot == null) return;

        Transform visual = characterRoot.Find(id);


        foreach (Transform child in characterRoot)
        {

            if (slotType == InventoryItem.ItemType.Head &&
               (child.name.Contains("Helmet") || child.name.Contains("Helm")))
            {
                child.gameObject.SetActive(false);
            }


            else if (slotType == InventoryItem.ItemType.Body &&
                    (child.name.Contains("Chestplate") || child.name.Contains("Armor")))
            {
                child.gameObject.SetActive(false);
            }


            else if (slotType == InventoryItem.ItemType.Legs &&
                    (child.name.Contains("Leggings") || child.name.Contains("Legs")))
            {
                child.gameObject.SetActive(false);
            }
        }

        visual.gameObject.SetActive(true);
    }

    public void DeactivateVisual()
    {
        if (characterRoot == null) return;

        InventoryItem item = GetComponentInChildren<InventoryItem>();
        if (item != null)
        {
            Transform visual = characterRoot.Find(item.itemID);
            if (visual != null) visual.gameObject.SetActive(false);
        }
    }

    public void DeactivateVisualByItem(InventoryItem item)
    {
        if (characterRoot == null) return;
        Transform visual = characterRoot.Find(item.itemID);
        if (visual != null) visual.gameObject.SetActive(false);
    }
}