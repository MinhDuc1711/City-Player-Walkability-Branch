using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class uiMenuManager : MonoBehaviour
{    
    public RectTransform canva;

    public CameraLock camLock;

    public List<UIMenu> toggleBtns = new List<UIMenu>();

    private UIMenu active;

    private void Awake()
    {
        camLock = this.gameObject.GetComponent<CameraLock>();
    }

    private void Start()
    {
        active = toggleBtns[1];
    }

    private void Update()
    {

        if (active)
        {
            camLock.controlCamera(true);
            unlockCursor();
        }
        else
        {
            camLock.controlCamera(false);
            lockCursor();
        }
    }

    public void AddMenu(UIMenu menu)
    {
        toggleBtns.Add(menu);
    }

    public void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void showUIMenu(UIMenu obj) //obj is the ui menu that should be shown to the user
    {
        
        if (active == null)
        {
            obj.SetActive(true);
            if (obj.AnimationTime != 0)
                StartCoroutine(ActivateUIAfterDelay(obj));
            obj.State = true;
            active = obj;
        }
        else
        {
            active.State = false;
            active.SetActive(false);
            if (active.AnimationTime != 0)
                StartCoroutine(ActivateUIAfterDelay(active));
            obj.State = true;
            obj.SetActive(true);
            if (obj.AnimationTime != 0)
                StartCoroutine(ActivateUIAfterDelay(obj));
            active = obj;
        }
    }

    public void hideUIMenu(UIMenu obj)
    {
        obj.SetActive(false);
        obj.State = false;

        if (active == obj)
        {
            active = null;
        }
    }

    IEnumerator ActivateUIAfterDelay(UIMenu obj)
    {
        yield return new WaitForSeconds(obj.AnimationTime);
    }

    public void InputCall(UIMenu _menu)
    {
        if (active != _menu)
            showUIMenu(_menu);
        else
            hideUIMenu(_menu);
    }
}
