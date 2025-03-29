using UnityEngine;
using System.Collections.Generic;
using static Unity.Collections.AllocatorManager;

public class BackgroundBuildingsMover : MonoBehaviour
{
    public Transform plotParent;
    public Transform leftBuildingsGroup;
    public Transform rightBuildingsGroup;

    private List<float> initialLeftX = new List<float>();
    private List<float> initialRightX = new List<float>();

    private Transform currentBuilding;
    private float initialBlockPosX;

    private void Start()
    {
        if (plotParent == null || leftBuildingsGroup == null || rightBuildingsGroup == null)
        {
            Debug.LogError("Assign all references (plot, left group, right group) in the Inspector!");
            return;
        }

        if (plotParent.childCount > 0)
        {
            currentBuilding = plotParent.GetChild(0);
            initialBlockPosX = currentBuilding.position.x;
        }
   

        foreach (Transform child in leftBuildingsGroup)
            initialLeftX.Add(child.transform.position.x);

        foreach (Transform child in rightBuildingsGroup)
            initialRightX.Add(child.transform.position.x);
    }

    private void Update()
    {
        if (plotParent.childCount > 0)
        {
            Transform newChild = plotParent.GetChild(0);
            if (newChild != currentBuilding)
            {
                currentBuilding = newChild;
                initialBlockPosX = currentBuilding.position.x; 
            }
        }

        if (currentBuilding == null) return;

        float deltaX = currentBuilding.position.x - initialBlockPosX;

        for (int i = 0; i < leftBuildingsGroup.childCount; i++)
        {
            Transform child = leftBuildingsGroup.GetChild(i);
            child.transform.position = new Vector3(
                initialLeftX[i] - deltaX,
                child.transform.position.y,
                child.transform.position.z
            );
        }

        for (int i = 0; i < rightBuildingsGroup.childCount; i++)
        {
            Transform child = rightBuildingsGroup.GetChild(i);
            child.transform.position = new Vector3(
                initialRightX[i] + deltaX,
                child.transform.position.y,
                child.transform.position.z
            );
        }
    }
}
