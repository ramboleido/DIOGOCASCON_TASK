using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject inventoryItemPrefab; // Prefab for UI inventory item
    private bool isPlayerNearby = false;
    public InventoryManager inventoryManager; // Assign in Inspector


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void PickUpItem()
    {
        InventoryManager inventory = FindFirstObjectByType<InventoryManager>();
    
        if (inventory == null)
        {
            Debug.LogError("InventoryManager not found in the scene!");
            return;
        }

        if (inventoryItemPrefab == null)
        {
            Debug.LogError("Inventory item prefab is not assigned!");
            return;
        }

        bool addedSuccessfully = inventory.AddItem(inventoryItemPrefab);
    
        if (addedSuccessfully)
        {
            Debug.Log("Item successfully added to inventory!");
            Destroy(gameObject); // Remove item from world
        }
        else
        {
            Debug.LogWarning("Inventory is full or could not add item.");
        }
    }

}