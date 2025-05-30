using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { PurpleKey, BlueKey }
    public ItemType itemType;

    public void UseItem()
    {
        if (itemType == ItemType.PurpleKey)
        {
            Debug.Log("Purple Key equipped!");
            
        }
        else if (itemType == ItemType.BlueKey)
        {
            Debug.Log("Blue Key equipped!");
            
        }
    }
}