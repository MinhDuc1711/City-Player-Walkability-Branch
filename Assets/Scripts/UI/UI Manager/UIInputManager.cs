using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    public uiMenuManager manager;
    // Update is called once per frame
    void Update()
    {
        //For Aspect Slidders Menu, menu index 0
        if (Input.GetKeyDown(KeyCode.E))
            manager.InputCall(0);
        else if (Input.GetKeyDown(KeyCode.P))
            manager.InputCall(1);
        else if (Input.GetKeyDown(KeyCode.R))
            manager.InputCall(2);
    }
}
