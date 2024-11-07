using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageBuilding : MonoBehaviour
{
    [SerializeField]
    private GameObject Ui;

    public static GameObject selectedBuilding;

    private GameObject highlight;

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Deselect()
    {
        if (selectedBuilding != null)
        {
            selectedBuilding.GetComponent<Outline>().enabled = false;
            Ui.SetActive(false);
            selectedBuilding = null;
        }
    }

    public void Highlight(GameObject obj)
    {
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null)
        {
            obj.AddComponent<Outline>();
        }
        else
        {
            outline.enabled = true;
        }
        selectedBuilding = obj;
        Ui.SetActive(true);
       
    }


}
