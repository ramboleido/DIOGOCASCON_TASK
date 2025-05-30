using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel; // UI panel reference
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

        // **Pause the game & timer when inventory opens**
        Time.timeScale = isOpen ? 0 : 1; 
    }

    public bool AddItem(GameObject itemPrefab)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount == 0) // Find an empty slot
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform); // Create UI item
                newItem.transform.localPosition = Vector3.zero;
            
                DraggableItem draggableItem = newItem.GetComponent<DraggableItem>();
                if (draggableItem != null)
                {
                    draggableItem.SetSlot(slot);
                }
                else
                {
                    Debug.LogError("DraggableItem component missing on prefab!");
                }

                return true;
            }
        }

        Debug.LogWarning("Inventory is full! No empty slots available.");
        return false; // Inventory full
    }


    public void RemoveItem(DraggableItem item)
    {
        Destroy(item.gameObject);
    }

    public void DropItem(DraggableItem item)
    {
        foreach (var slot in slots)
        {
            if (slot.transform.childCount == 0)
            {
                SpawnWorldItem(item.gameObject);
                Destroy(item.gameObject); // Remove from inventory UI
                return;
            }
        }
    }

    private void SpawnWorldItem(GameObject itemPrefab)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector2 dropPosition = (Vector2)player.transform.position + new Vector2(1, 0); // Drop slightly ahead
            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        }
    }
}