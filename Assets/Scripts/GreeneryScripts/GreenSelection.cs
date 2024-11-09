using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenSelection : MonoBehaviour
{
    public GameObject selectedObj; 
    public Text objNameText; 
    public GameObject selectUI; 

    public GreenObjectManager greenObjManager; 

    private void Start()
    {
        selectUI.SetActive(false); 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Object"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Deselect();
        }
    }

    private void Select(GameObject obj)
    {
        if (obj == selectedObj) return;

        if (selectedObj != null) Deselect();

        Outline outline = obj.GetComponent<Outline>();
        if (outline == null) obj.AddComponent<Outline>(); 
        else outline.enabled = true; 
        selectedObj = obj;
        selectUI.SetActive(true);
    }

    private void Deselect()
    {
        selectedObj.GetComponent<Outline>().enabled = false;
        selectUI.SetActive(false) ;
        selectedObj = null;
    }

    public void Delete()
    {

        GameObject objToDestroy = selectedObj;
        Deselect();
        Destroy(objToDestroy);
    }

    public void Move()
    {
        greenObjManager.pendingObj = selectedObj;
    }
}
