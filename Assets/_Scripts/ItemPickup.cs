using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public DraggableItem itemPrefab; // Prefab reference for UI representation

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager inventory = collision.GetComponent<InventoryManager>();

            if (inventory != null)
            {
                inventory.AddItem(CreateInventoryItem());
                Destroy(gameObject); // Remove the item from the world
            }
        }
    }

    private DraggableItem CreateInventoryItem()
    {
        DraggableItem newItem = Instantiate(itemPrefab);
        return newItem;
    }
}