using UnityEngine;

public class TreeSelection : MonoBehaviour
{
    public GameObject selectedObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame

    void Update()
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
        if (Input.GetMouseButtonDown(1) && selectedObj != null)
        {
            Deselect();
        }
    }

    void Select(GameObject obj)
    {
        if (obj == selectedObj) return;
        if (selectedObj != null)
            Deselect();

        Outline outline = obj.GetComponent<Outline>();

        if (outline == null)
            obj.AddComponent<Outline>();
        else
            outline.enabled = true;
        selectedObj = obj;
    }

    void Deselect()
    {
        selectedObj.GetComponent<Outline>().enabled = false;
        selectedObj = null;
    }

}
