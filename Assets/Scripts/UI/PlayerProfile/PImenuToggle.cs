using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PImenuToggle : UIMenu, IPointerClickHandler
{
    private bool state = false;
    private float animationTime = 0;

    public uiMenuManager manager;
    public GameObject traffic;
    public RectTransform menu;

    private Vector3 originalCameraPosition;

    public Transform cameraTransform;

    protected void Awake()
    {
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ActivateProfilePanel();
        }
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
            traffic.gameObject.SetActive(true);
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("UI-starting-Menu");
    }

    public void ActivateProfilePanel()
    {
        if (!state)
            manager.showUIMenu(this);
        else if (state)
            manager.hideUIMenu(this);
    }
}
