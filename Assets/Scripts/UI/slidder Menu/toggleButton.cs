using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class toggleButton : UIMenu, IPointerClickHandler
{
    public uiMenuManager menuManager;

    private Animator SlidderMenu;

    private RectTransform s_RTransform;

    private float open = -271.03f;

    private float close = -930f;

    private bool state = false; //this var is displaying when the UI is shown (true) or hidden (false)

    public float animationTime = 0.35f;

    //temporary solution for animation

    public RectTransform menu;

    private Vector2 closedPos;

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuManager.fs.writeTofile();
            BackToMenu();
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

    private void Awake()
    {
        SlidderMenu = GetComponentInParent<Animator>();
        s_RTransform = SlidderMenu.transform.GetComponent<RectTransform>();
    }

    public void Start()
    {
        closedPos = new Vector2(menu.anchoredPosition.x,menu.anchoredPosition.y);
    }

    public override void SetActive(bool state) //setting bool to true activate the open animation, false activate the close animation
    {
        //SlidderMenu.SetBool("SMenu", state);

        if (state)
        {
            menu.anchoredPosition = new Vector2((closedPos.x+menu.sizeDelta.x*1.15f), closedPos.y);
        }
        else
        {
            menu.anchoredPosition = closedPos;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Should Call the showUI function in the UI manager class
        if (!state)
            menuManager.showUIMenu(this);
        else
            menuManager.hideUIMenu(this);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("UI-starting-Menu");
    }

    public override void ActivateMenu()
    {
        if (!state)
            menuManager.showUIMenu(this);
        else if (state)
            menuManager.hideUIMenu(this);
    }
}
