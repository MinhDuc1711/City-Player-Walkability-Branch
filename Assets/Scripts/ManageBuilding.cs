using UnityEngine;
using UnityEngine.UI;

public class ManageRemove : MonoBehaviour
{
    [SerializeField]
    private GameObject Ui;

    private GameObject highlight;

    private GameObject buildingSelected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SelectBuilding();
        Deselect();
    }

    void Deselect()
    {
        if (Input.GetMouseButtonDown(1) && buildingSelected != null)
        {
            buildingSelected.GetComponent<Outline>().enabled = false;
            Ui.SetActive(false);
            buildingSelected = null;
        }
    }

    void SelectBuilding()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Building"))
                {
                    Highlight(hit.collider.gameObject);
                }
            }
        }
    }

    void Highlight(GameObject obj)
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
        Debug.Log("HIT");
        buildingSelected = obj;
        Ui.SetActive(true);
    }
}
