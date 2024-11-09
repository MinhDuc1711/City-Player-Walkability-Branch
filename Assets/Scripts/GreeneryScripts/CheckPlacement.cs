using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    private GreenObjectManager buildingManager; 

    void Start()
    {
        buildingManager = GameObject.Find("GreenObjectManager").GetComponent<GreenObjectManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an object with the "Object" tag
        if (other.gameObject.CompareTag("Object"))
        {
            buildingManager.canPlace = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to an object with the "Object" tag
        if (other.gameObject.CompareTag("Object"))
        {
            buildingManager.canPlace = true; 
        }
    }
}
