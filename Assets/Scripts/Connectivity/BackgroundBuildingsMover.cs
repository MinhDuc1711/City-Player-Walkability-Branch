using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Linq;

public class BackgroundBuildingsMover : MonoBehaviour
{
    private List<float> initialPos = new List<float>();
    public GameObject block;

    private float initialBlockPosX;

    private void Start()
    {
        initialBlockPosX = block.transform.position.x;

        foreach (Transform child in transform)
        {
            initialPos.Add(child.transform.position.x);
        }
    }

    private void Update()
    {
        float blockDeltaX = block.transform.position.x - initialBlockPosX;

        int i = 0;
        foreach (Transform child in transform)
        {
            child.transform.position = new Vector3(
                initialPos[i] + blockDeltaX,
                child.transform.position.y,
                child.transform.position.z
            );

            i++;
        }
    }
}
