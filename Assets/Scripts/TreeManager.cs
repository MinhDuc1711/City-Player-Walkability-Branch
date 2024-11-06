using UnityEngine;

public class TreeManager : MonoBehaviour
{

    public GameObject[] objects;

    private Vector3 pos;

    private RaycastHit Hit;

    public GameObject pendingObj;

    public float rotateAmount;

    public bool canPlace = true;



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

            if(Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && pendingObj != null)
        { 
            RotateObject();
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

    void RotateObject()
    {
        pendingObj.transform.Rotate(Vector3.up, rotateAmount);
    }

    

}
