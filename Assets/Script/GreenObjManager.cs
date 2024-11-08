using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenObjectManager : MonoBehaviour
{
    public GameObject[] greeneryObjects; // Array of greenery objects (trees, bushes, etc.)
    private Vector3 pos;                  // Position for placing greenery
    private RaycastHit hit;               // Raycast hit info
    private GameObject pendingObj;

    [SerializeField] private LayerMask layerMask;          // LayerMask to specify the ground layer

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1, layerMask))
        {
            pos = hit.point; // Set pos to the point where the raycast hits
        }
    }

    public void SelectObject(int index)
    {
        // Instantiate the selected greenery object at the raycast position
        pendingObj = Instantiate(greeneryObjects[index], pos, transform.rotation);
    }

    private void Update()
    {
        if (pendingObj != null)
        {
            // Move the pending object to the mouse position (raycast position)
            pendingObj.transform.position = pos;

            // Place the object on left mouse click
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    private void PlaceObject()
    {
        pendingObj = null; // Clear the pending object after placement
    }

}