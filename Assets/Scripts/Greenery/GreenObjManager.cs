
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenObjectManager : MonoBehaviour
{
    public GameObject[] greeneryObjects;
    private Vector3 pos;                  // Position for placing greenery
    private RaycastHit hit;
    public GameObject pendingObj;

    [SerializeField] private LayerMask layerMask;

    public bool canPlace = true;

    public float rotateAmount = 45f; // rotation degrees x*45

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            pos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        //green object instantiated
        pendingObj = Instantiate(greeneryObjects[index], pos, transform.rotation);
    }

    private void Update()
    {
        if (pendingObj != null)
        {
            pendingObj.transform.position = pos;

            // rotation using R input
            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateObject();
            }

            // left click used to move object
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }
        }
    }

    private void RotateObject()
    {
        pendingObj.transform.Rotate(Vector3.up, rotateAmount);
    }

    private void PlaceObject()
    {
        pendingObj = null;
    }
}
