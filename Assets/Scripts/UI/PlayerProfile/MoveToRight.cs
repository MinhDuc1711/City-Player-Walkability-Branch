using UnityEngine;
using UnityEngine.EventSystems;

public class MoveToRight : MonoBehaviour, IPointerClickHandler
{
    public pageslidder ps;
    public void OnPointerClick(PointerEventData eventData)
    {
        ps.moveToPageOnRight();
    }
}
