using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class toggleButton : UIMenu, IPointerClickHandler
{
    public uiMenuManager menuManager;

    private Animator SlidderMenu;

    private RectTransform s_RTransform;

    private float open = -271.03f;

    private float close = -930f;

    private bool state = false; //this var is displaying when the UI is shown (true) or hidden (false)

    public float animationTime = 0.35f;

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

    private void Awake()
    {
        SlidderMenu = GetComponentInParent<Animator>();
        s_RTransform = SlidderMenu.transform.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void SetActive(bool state) //setting bool to true activate the open animation, false activate the close animation
    {
        SlidderMenu.SetBool("SMenu", state);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Should Call the showUI function in the UI manager class
        if (!state)
            menuManager.showUIMenu(this);
        else
            menuManager.hideUIMenu(this);
    }
}
