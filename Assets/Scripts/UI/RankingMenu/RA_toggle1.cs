using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RA_toggle1 : UIMenu, IPointerClickHandler
{
    private bool state = false;
    private float animationTime = 0;

    public uiMenuManager manager;
    public RectTransform menu;

    //public float closed = -182f;
   // public float open = 1693f+(91f);

    protected void Awake()
    {
    }

    protected void Update()
    {
        
    }

    public override void SetActive(bool _state)
    {
        if (_state)
        {
           //menu.anchoredPosition = new Vector2(open, menu.anchoredPosition.y);
           menu.gameObject.SetActive(true);
        }
        else
        {
            //menu.anchoredPosition = new Vector2(closed, menu.anchoredPosition.y); ;
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

    public override void ActivateMenu()
    {
        if (!state)
            manager.showUIMenu(this);
        else if (state)
            manager.hideUIMenu(this);
    }
}