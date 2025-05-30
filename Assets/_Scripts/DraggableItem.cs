using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    private InventorySlot currentSlot;
    
    public void SetSlot(InventorySlot slot)
    {
        currentSlot = slot;
        transform.SetParent(slot.transform);
        transform.localPosition = Vector3.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        SetSlot(currentSlot);
    }
}