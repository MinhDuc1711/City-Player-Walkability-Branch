using UnityEngine;
using UnityEngine.EventSystems;

public class AspectSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        if (transform.childCount != 0)
        {
            Transform child = transform.GetChild(0);
            child.SetParent(draggableItem.parentAfterDrag);
        }
        draggableItem.parentAfterDrag = transform;
    }
}
