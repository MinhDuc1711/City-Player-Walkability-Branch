using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class uiMenuManager : MonoBehaviour
{    
    public RectTransform canva;

    private UIMenu active = null;

    public List<UIMenu> toggleBtns = new List<UIMenu>();

    public List<KeyCode> keys = new List<KeyCode>();

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (active == null)
            lockCursor();
        else
            unlockCursor();
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
            Debug.Log(obj.gameObject.name);
            obj.SetActive(true);
            if (obj.AnimationTime != 0)
                StartCoroutine(ActivateUIAfterDelay(obj));
            obj.State = true;
            active = obj;
        }
        else
        {
            Debug.Log(obj.gameObject.name);
            Debug.Log(active.gameObject.name);
            active.State = false;
            active.SetActive(false);
            if (active.AnimationTime != 0)
                StartCoroutine(ActivateUIAfterDelay(active));
            obj.State = true;
            obj.SetActive(true);
            if (obj.AnimationTime != 0)
                StartCoroutine(ActivateUIAfterDelay(obj));
        }
    }

    public void hideUIMenu(UIMenu obj)
    {
        if(active != null && obj.State==true)
        {
            obj.State=false;
            obj.SetActive(false);
            active = null;
        }
    }

    IEnumerator ActivateUIAfterDelay(UIMenu obj)
    {
        yield return new WaitForSeconds(obj.AnimationTime);
    }
}
