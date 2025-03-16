using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDictionary2ListAttempt : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    private GameObject plotParent;
    [Space(15)]
    [SerializeField]
    private List<GameObject> buildingPrefabs = new List<GameObject>();
    [SerializeField]
    private List<GameObject> unmodifiedBuildings = new List<GameObject>();
    [SerializeField]
    private List<GameObject> modifiedBuildings = new List<GameObject>();

    private int interval;
    private int numOfBuildingsChanged;
    private float oldPercentage = 0;
    private bool flag = true;

    private void Start()
    {
        interval = Mathf.FloorToInt(100 / slider.maxValue);
        slider.onValueChanged.AddListener(SliderChange);
    }

    [ContextMenu("Get Buildings")]
    void GetBuildings()
    {
        CleanBuildingList();
        foreach (Transform plot in plotParent.transform)
        {
            foreach (Transform child in plot.transform)
            {
                unmodifiedBuildings.Add(child.GetChild(0).gameObject);
            }
        }
        EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }
    void CleanBuildingList()
    {
        unmodifiedBuildings.Clear();
    }

    private void SliderChange(float value)
    {
        float percentage = (value * interval) / 100;
        if(flag)
        {
            Debug.Log("AAAAAAA");
            this.numOfBuildingsChanged = Mathf.FloorToInt(Mathf.Round(percentage * unmodifiedBuildings.Count));
            flag = false;
        }
        Debug.Log(percentage);
        Debug.Log(interval);
        Debug.Log(numOfBuildingsChanged);

        if (percentage > oldPercentage)
        {
            BuildingChange(numOfBuildingsChanged, unmodifiedBuildings, true);
        }
        if (percentage < oldPercentage)
        {
            BuildingChange(numOfBuildingsChanged, modifiedBuildings, false);
        }
        oldPercentage = percentage;
    }

    private void BuildingChange(int count, List<GameObject> buildingList, bool newBuildingType)
    {
        for (int c=0; c < count; c++)
        {
            if (buildingList.Count == 0) return;
            int randomIndex = Random.Range(0, buildingList.Count);
            BuildingSpawn(randomIndex, buildingList, newBuildingType);
            buildingList.Remove(buildingList[randomIndex]);
        }
    }

    private void BuildingSpawn(int index, List<GameObject> buildingList, bool newBuildingType)
    {
        GameObject buildingPrefab = null;
        if (newBuildingType)
        {
            int randomPrefab = Random.Range(0, buildingPrefabs.Count);
            buildingPrefab = buildingPrefabs[randomPrefab];
        }
        else
        {
            buildingPrefab = buildingPrefabs[0];
        } 
        GameObject selectedBuilding = buildingList[index].gameObject;
        Transform parentPlot = selectedBuilding.transform.parent;

        GameObject newBuilding = Instantiate(buildingPrefab, 
            new Vector3(selectedBuilding.transform.position.x, parentPlot.position.y, selectedBuilding.transform.position.z), 
            selectedBuilding.transform.rotation);

        newBuilding.transform.SetParent(parentPlot, true);
        newBuilding.SetActive(true);
        Destroy(selectedBuilding);
        if(newBuildingType)
        modifiedBuildings.Add(newBuilding);
        else
        unmodifiedBuildings.Add(newBuilding);
    }
    
}
