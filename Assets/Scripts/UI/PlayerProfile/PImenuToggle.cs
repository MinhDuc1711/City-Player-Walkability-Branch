using UnityEngine;
using UnityEngine.EventSystems;
using static State;

public class PImenuToggle : UIMenu, IPointerClickHandler
{
    private bool state = false;
    private float animationTime = 0;

    public uiMenuManager manager;

    public RectTransform menu;

    protected void Awake()
    {
        if (menu)
        {
            menu.gameObject.SetActive(false);
        }
    }

    protected void Update()
    {

    }

    public override void SetActive(bool _state)
    {
        if (_state)
        {
            menu.gameObject.SetActive(true);
        }
        else if (!_state)
        {
            menu.gameObject.SetActive(false);
        }
    }

    public override bool State
    {
        get => state;
        set
        {
            state = value;
        }
    }

    public override float AnimationTime
    {
        get => animationTime;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!state)
            manager.showUIMenu(this);
        else if (state)
            manager.hideUIMenu(this);
    }
}
