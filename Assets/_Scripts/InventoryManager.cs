using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private List<InventorySlot> slots;
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

    public bool AddItem(DraggableItem item)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount == 0) // Empty slot available
            {
                item.transform.SetParent(slot.transform);
                item.transform.localPosition = Vector3.zero;
                return true; // Successfully added the item
            }
        }
        Debug.Log("Your inventory is full!"); 
        return false; // Inventory is full
    }


    public void RemoveItem(DraggableItem item)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.GetChild(0) == item.transform)
            {
                item.transform.SetParent(null);
                return;
            }
        }
    }
}