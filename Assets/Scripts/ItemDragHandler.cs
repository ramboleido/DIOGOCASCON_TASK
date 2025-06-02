using UnityEngine;
using UnityEngine.EventSystems;
public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; //Save original parent
        transform.SetParent(transform.root); // Over other canvas
        canvasGroup.blocksRaycasts = false; //deactivates raycasts
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; //item follows mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; //activates raycasts
        
        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>(); //slot where item dropped
         
        if (dropSlot != null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if(dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
            Slot originalSlot = originalParent.GetComponent<Slot>();
            if (dropSlot != null)
            {
                // is a slot under the drop point
                if (dropSlot.currentItem != null)
                {
                    
                    dropSlot.currentItem.transform.SetParent(originalParent.transform);
                    originalSlot.currentItem = dropSlot.currentItem;
                    dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }
            else
            {
                originalSlot.currentItem = null;
            }
            //move item into drop slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            //no slot under item being dropped
            transform.SetParent(originalParent);
        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; //centers item in slot
    }
}
