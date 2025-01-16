using UnityEngine;
using UnityEngine.EventSystems;

public class MoveToLeft : MonoBehaviour, IPointerClickHandler
{

    public pageslidder ps;

    public void OnPointerClick(PointerEventData eventData)
    {
        ps.moveToPageOnLeft();
    }
}
