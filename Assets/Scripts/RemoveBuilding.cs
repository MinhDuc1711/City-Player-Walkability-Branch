using System;
using UnityEngine;

public class RemoveBuilding : MonoBehaviour
{

    public void DeleteSelectedBuilding()
    {
        if (ManageBuilding.selectedBuilding != null)
        Destroy(ManageBuilding.selectedBuilding);
        gameObject.SetActive(false);
        
    }
}
 