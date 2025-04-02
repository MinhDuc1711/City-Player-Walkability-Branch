using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class exitConf : UIMenu
{
    private bool state = false;
    private float animationTime = 0;

    public uiMenuManager manager;
    public RectTransform menu;

    public FileSaver fs;

    protected void Awake()
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

    public void BackToMenu()
    {
        // --------------- Add call to the filesaver function --------------
        fs.writeTofile();
        // -----------------------------------------------------------------

        SceneManager.LoadScene("UI-starting-Menu");
    }

    public override void ActivateMenu()
    {
        if (!state)
            manager.showUIMenu(this);
        else if (state)
            manager.hideUIMenu(this);
    }
}
