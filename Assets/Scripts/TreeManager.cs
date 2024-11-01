using UnityEngine;

public class TreeManager : MonoBehaviour
{

    public GameObject[] objects;

    private Vector3 pos;

    private RaycastHit Hit;

    GameObject pendingObj;

    [SerializeField]
    private LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pendingObj !=null)
        {
            pendingObj.transform.position = pos;

            if(Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    void PlaceObject()
    {
        pendingObj = null;
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out Hit, 1000,layerMask))
        {
            pos = Hit.point;
        }
    }

    public void SelectObject(int index)
    {
        pendingObj = Instantiate(objects[index], pos, transform.rotation);
    }

}
