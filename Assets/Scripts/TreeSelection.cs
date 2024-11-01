using UnityEngine;

public class TreeSelection : MonoBehaviour
{
    public GameObject selectedObj;

    private TreeManager treeManager;

    public GameObject selectUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeManager = GameObject.Find("TreeManager").GetComponent<TreeManager>();
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
        selectUI.SetActive(true);
    }

    void Deselect()
    {
        selectedObj.GetComponent<Outline>().enabled = false;
        selectUI.SetActive(false);
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
        treeManager.pendingObj = selectedObj;
    }

}
