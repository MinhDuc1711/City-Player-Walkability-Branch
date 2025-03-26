using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Linq;

public class BackgroundBuildingsMover : MonoBehaviour
{
    private float[] initialPos;

    public GameObject block;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            initialPos.Append(child.transform.position.x);  
        }

    }

    private void Update()
    {
        int i = 0;

        foreach (Transform child in transform)
        {
           
            child.transform.position = new Vector3(initialPos[i] + block.transform.position.x, child.transform.position.y, child.transform.position.z);

            i++;

        }
            
    }
}
