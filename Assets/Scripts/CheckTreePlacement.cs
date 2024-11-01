using UnityEngine;

public class CheckTreePlacement : MonoBehaviour
{
    private TreeManager treeManager;

    public GameObject selectedObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeManager = GameObject.Find("TreeManager").GetComponent<TreeManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && treeManager.pendingObj==null)
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
        if (Input.GetMouseButtonDown(1) && selectedObj!=null)
        {
            Deselect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            treeManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            treeManager.canPlace = true;
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
