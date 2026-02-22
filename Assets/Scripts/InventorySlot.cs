public class InventorySlot : MonoBehaviour, IDropHandler
{
    public InventoryItem.ItemType slotType;
    public bool isEquipmentSlot;
    public Transform characterRoot;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            SwapItems(eventData.pointerDrag.GetComponent<InventoryItem>());
        }
        else
        {
            PlaceItem(eventData.pointerDrag.GetComponent<InventoryItem>());
        }
    }

    private void PlaceItem(InventoryItem item)
    {
        if (!isEquipmentSlot || item.type == slotType)
        {
            item.parentAfterDrag = transform;
            if (isEquipmentSlot) ActivateVisual(item.itemID);
        }
    }

    private void SwapItems(InventoryItem newItem)
    {
        if (isEquipmentSlot && newItem.type != slotType) return;

        InventoryItem existingItem = transform.GetChild(0).GetComponent<InventoryItem>();
        InventorySlot sourceSlot = newItem.parentAfterDrag.GetComponent<InventorySlot>();

        if (sourceSlot.isEquipmentSlot && existingItem.type != sourceSlot.slotType) return;


        existingItem.transform.SetParent(newItem.parentAfterDrag);
        existingItem.transform.localPosition = Vector3.zero;
        newItem.parentAfterDrag = transform;


        if (sourceSlot.isEquipmentSlot) sourceSlot.ActivateVisual(existingItem.itemID);
        if (isEquipmentSlot) ActivateVisual(newItem.itemID);
    }

    public void ActivateVisual(string id)
    {
        if (characterRoot == null) return;



        foreach (Transform child in characterRoot)
        {

            if (child.name == id) child.gameObject.SetActive(true);
            else if (IsSameCategory(child.name, id)) child.gameObject.SetActive(false);
        }
    }

    public void DeactivateVisual()
    {

        if (transform.childCount > 0)
        {
            string id = transform.GetChild(0).GetComponent<InventoryItem>().itemID;
            Transform visual = characterRoot.Find(id);
            if (visual != null) visual.gameObject.SetActive(false);
        }
    }

    private bool IsSameCategory(string objName, string id)
    {

        return false;
    }
}