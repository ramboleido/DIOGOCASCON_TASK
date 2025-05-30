using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData; // Reference to the item's data (ScriptableObject)
    private bool isPlayerNearby = false; // Tracks if player is within range

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

    private void PickUpItem()
    {
        InventoryManager inventory = FindFirstObjectByType<InventoryManager>(); // Unity 6 API update
        if (inventory != null)
        {
            inventory.AddItem(itemData);
            Destroy(gameObject); // Remove item from the world after pickup
        }
    }
}