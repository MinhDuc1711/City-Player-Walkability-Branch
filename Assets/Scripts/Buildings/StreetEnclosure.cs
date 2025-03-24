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

    public float SliderOffset;

    void Start()
    {
        GetBuildings();
        /**
        if (slider != null)
        {
            slider.onValueChanged.AddListener(UpdateBuildingPositions);
        }
        else
        {
            Debug.LogError("Slider is not assigned in the Inspector!");
        }
        **/
    }

    public void UpdateBuildingPos(float sliderValue) 
    {
        SliderOffset = sliderValue;


        for (int i = 0; i < leftBuildings.Count; i++)
        {
            leftBuildings[i].transform.position= new Vector3(leftOriginalPositions[i].x + SliderOffset, leftOriginalPositions[i].y, leftOriginalPositions[i].z);
        }
        for (int i = 0; i < leftBuildings.Count; i++)
        {
            rightBuildings[i].transform.position = new Vector3(rightOriginalPositions[i].x - SliderOffset, rightOriginalPositions[i].y, rightOriginalPositions[i].z);
        }
    }

    public void UpdateOriginalBuildingPos()
    {
        if (leftOriginalPositions == null || leftOriginalPositions.Length != leftBuildings.Count)
            leftOriginalPositions = new Vector3[leftBuildings.Count];

        if (rightOriginalPositions == null || rightOriginalPositions.Length != rightBuildings.Count)
            rightOriginalPositions = new Vector3[rightBuildings.Count];

        for (int i=0; i<leftBuildings.Count;i++)
        {
            leftOriginalPositions[i] = new Vector3(leftBuildings[i].transform.position.x-SliderOffset, leftBuildings[i].transform.position.y, leftBuildings[i].transform.position.z);
        }
        for (int i = 0; i < leftBuildings.Count; i++)
        {
            rightOriginalPositions[i] = new Vector3(rightBuildings[i].transform.position.x + SliderOffset, rightBuildings[i].transform.position.y, rightBuildings[i].transform.position.z);
        }
    }

    [ContextMenu("Get Buildings")]
    void GetBuildings()
    {
        plotParent = GameObject.Find("Plots");
        leftBuildings.Clear();
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
        UpdateOriginalBuildingPos();
        //RefreshBuildings();
    }
    /**

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

   

    void UpdateBuildingPositions(float value)
    {
        float offset = (value / slider.maxValue) * maxOffset * 2.0f;
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

        UpdateOriginalPos(offset);


    }


    public void UpdateOriginalPos(float offset)
    {
        for (int i = 0; i < leftBuildings.Count; i++)
        {
            if (leftBuildings[i] != null)
            {
                Vector3 originalPos = leftOriginalPositions[i];
                leftBuildings[i].transform.position = new Vector3(originalPos.x + offset, originalPos.y, originalPos.z);
            }
        }
        for (int i = 0; i < rightBuildings.Count; i++)
        {
            if (rightBuildings != null)
            {
                Vector3 originalPos = rightOriginalPositions[i];
                rightBuildings[i].transform.position = new Vector3(originalPos.x - offset, originalPos.y, originalPos.z);
            }
        }
    }
    **/

}