using System.Collections.Generic;
using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    public UIMenu el0;
    public UIMenu el1;
    public UIMenu el2;
    public UIMenu el3;
    // Update is called once per frame
    void Update()
    {
        //For Aspect Slidders Menu, menu index 0
        if (Input.GetKeyDown(KeyCode.E))
            el0.ActivateMenu();
        else if (Input.GetKeyDown(KeyCode.P))
            el1.ActivateMenu();
        else if (Input.GetKeyDown(KeyCode.R))
            el2.ActivateMenu();
        else if (Input.GetKeyDown(KeyCode.Escape))
            el3.ActivateMenu();
    }
}
