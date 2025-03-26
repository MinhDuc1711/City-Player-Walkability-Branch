using UnityEngine;
using System.Collections.Generic;

public class BackgroundBuildingsMover : MonoBehaviour
{
    public GameObject block;
    public Transform leftBuildingsGroup;
    public Transform rightBuildingsGroup;

    private List<float> initialLeftX = new List<float>();
    private List<float> initialRightX = new List<float>();

    private float initialBlockPosX;

    private void Start()
    {
        if (block == null || leftBuildingsGroup == null || rightBuildingsGroup == null)
        {
            Debug.LogError("Assign all references (block, left group, right group) in the Inspector!");
            return;
        }

        initialBlockPosX = block.transform.position.x;

        foreach (Transform child in leftBuildingsGroup)
            initialLeftX.Add(child.transform.position.x);

        foreach (Transform child in rightBuildingsGroup)
            initialRightX.Add(child.transform.position.x);
    }

    private void Update()
    {
        float deltaX = block.transform.position.x - initialBlockPosX;

        for (int i = 0; i < leftBuildingsGroup.childCount; i++)
        {
            Transform child = leftBuildingsGroup.GetChild(i);
            child.transform.position = new Vector3(
                initialLeftX[i] + deltaX, 
                child.transform.position.y,
                child.transform.position.z
            );
        }

        for (int i = 0; i < rightBuildingsGroup.childCount; i++)
        {
            Transform child = rightBuildingsGroup.GetChild(i);
            child.transform.position = new Vector3(
                initialRightX[i] - deltaX, 
                child.transform.position.y,
                child.transform.position.z
            );
        }
    }
}
