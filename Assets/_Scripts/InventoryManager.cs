using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private List<InventorySlot> slots;
    [SerializeField] private DraggableItem itemPrefab; // Single prefab for all items
    private bool isOpen = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);
    }

    public bool AddItem(ItemData itemData)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount == 0) // Find an empty slot
            {
                DraggableItem newItem = Instantiate(itemPrefab, slot.transform);
                newItem.AssignData(itemData);
                newItem.transform.localPosition = Vector3.zero;
                return true;
            }
        }
        return false; // Inventory full
    }

    public void RemoveItem(DraggableItem item)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.GetChild(0) == item.transform)
            {
                Destroy(item.gameObject); // Remove the item from inventory
                return;
            }
        }
    }
}