using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StreetEnclosure : MonoBehaviour
{
    [SerializeField] private float maxOffset = 3.0f; // Maximum distance buildings move towards the street
    [SerializeField] private Slider slider;
    private GameObject plotParent;
    [SerializeField] private List<GameObject> leftBuildings = new List<GameObject>();
    [SerializeField] private List<GameObject> rightBuildings = new List<GameObject>();

    private Vector3[] leftOriginalPositions;
    private Vector3[] rightOriginalPositions;

    void Start()
    {
        GetBuildings();

        if (slider != null)
        {
            slider.onValueChanged.AddListener(UpdateBuildingPositions);
        }
        else
        {
            Debug.LogError("Slider is not assigned in the Inspector!");
        }
    }

    void RefreshBuildings()
    {
        if (leftOriginalPositions == null || leftOriginalPositions.Length != leftBuildings.Count)
            leftOriginalPositions = new Vector3[leftBuildings.Count];

        if (rightOriginalPositions == null || rightOriginalPositions.Length != rightBuildings.Count)
            rightOriginalPositions = new Vector3[rightBuildings.Count];

        for (int i = 0; i < leftBuildings.Count; i++)
        {
            if (leftBuildings[i] != null)
            {
                // ✅ If it's a new building, keep the original position from the plot
                if (i >= leftOriginalPositions.Length || leftOriginalPositions[i] == Vector3.zero)
                    leftOriginalPositions[i] = leftBuildings[i].transform.position;
            }
        }
        
        for (int i = 0; i < rightBuildings.Count; i++)
        {
            if (rightBuildings[i] != null)
            {
                if (i >= rightOriginalPositions.Length || rightOriginalPositions[i] == Vector3.zero)
                    rightOriginalPositions[i] = rightBuildings[i].transform.position;
            }
        }
    }

    [ContextMenu("Get Buildings")]
    void GetBuildings()
    {
        plotParent = GameObject.Find("Plots");
        leftBuildings.Clear(); // Clean list before adding new ones
        rightBuildings.Clear();
        if (plotParent.transform.childCount > 0)
        {
            Transform leftHolder = plotParent.transform.GetChild(0);
            Transform rightHolder = plotParent.transform.GetChild(1);

            foreach (Transform plot in leftHolder)
            {
                if (plot.childCount > 0)
                {
                    leftBuildings.Add(plot.GetChild(0).gameObject);
                }
            }

            foreach (Transform plot in rightHolder)
            {
                if (plot.childCount > 0)
                {
                    rightBuildings.Add(plot.GetChild(0).gameObject);
                }
            }
        }
        else
        {
            Debug.LogError("Plots object found, but it has no children!");
        }
        RefreshBuildings();
    }

    void UpdateBuildingPositions(float value)
    {
        float offset = Mathf.Lerp(0, maxOffset, value / slider.maxValue);
        bool needsRefresh = false;

        for (int i = 0; i < leftBuildings.Count; i++)
        {
            if (leftBuildings[i] == null || leftBuildings[i].transform.parent == null)
            {
                needsRefresh = true;
                break;
            }
        }
        for (int i = 0; i < rightBuildings.Count; i++)
        {
            if (rightBuildings[i] == null || rightBuildings[i].transform.parent == null)
            {
                needsRefresh = true;
                break;
            }
        }
        if (needsRefresh)
        {
            Debug.LogWarning("Buildings have changed, calling GetBuildings()...");
            GetBuildings();
        }

        for (int i = 0; i < leftBuildings.Count; i++)
        {
            if(leftBuildings[i] != null)
            {
                Vector3 originalPos = leftOriginalPositions[i];
                leftBuildings[i].transform.position = new Vector3(originalPos.x + offset, originalPos.y, originalPos.z);
            }
        }
        for (int i = 0; i < rightBuildings.Count; i++)
        {
            if(rightBuildings != null)
            {
                Vector3 originalPos = rightOriginalPositions[i];
                rightBuildings[i].transform.position = new Vector3(originalPos.x - offset, originalPos.y, originalPos.z);
            }
        }
        
    }
}