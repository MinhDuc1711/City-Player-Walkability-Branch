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
            BuildingChange(numOfBuildingsChanged);
        }
        if (percentage < oldPercentage)
        {
            BuildingReset(numOfBuildingsChanged);
        }
        oldPercentage = percentage;
    }

    private void BuildingChange(int count)
    {
        for (int c=0; c < count; c++)
        {
            if (unmodifiedBuildings.Count == 0) return;
            int randomIndex = Random.Range(0, unmodifiedBuildings.Count);
            NewBuildingSpawn(randomIndex);
            unmodifiedBuildings.Remove(unmodifiedBuildings[randomIndex]);
        }
    }

    private void BuildingReset(int count)
    {
        for (int c = 0; c < count; ++c)
        {
            if (modifiedBuildings.Count == 0) return;
            int randomIndex = Random.Range(0, modifiedBuildings.Count);
            OldBuildingSpawn(randomIndex);
            modifiedBuildings.Remove(modifiedBuildings[randomIndex]);
        }
    }

    private void NewBuildingSpawn(int index)
    {
        int randomPrefab = Random.Range(0, buildingPrefabs.Count);
        GameObject buildingPrefab = buildingPrefabs[randomPrefab];
        GameObject selectedBuilding = unmodifiedBuildings[index].gameObject;
        Transform parentPlot = selectedBuilding.transform.parent;

        //Debug.Log(parentPlot.gameObject.name);

        GameObject newBuilding = Instantiate(buildingPrefab, 
            new Vector3(selectedBuilding.transform.position.x, parentPlot.position.y, selectedBuilding.transform.position.z), 
            selectedBuilding.transform.rotation);

        //Debug.Log(parentPlot.position.y);
        //Debug.Log(newBuilding.transform.position.y);

        newBuilding.transform.SetParent(parentPlot, true);
        newBuilding.SetActive(true);
        Destroy(selectedBuilding);
        modifiedBuildings.Add(newBuilding);

    }

    private void OldBuildingSpawn(int index)
    {
        GameObject buildingPrefab = buildingPrefabs[0];
        GameObject selectedBuilding = modifiedBuildings[index].gameObject;
        Transform parentPlot = selectedBuilding.transform.parent;

        GameObject newBuilding = Instantiate(buildingPrefab,
            new Vector3(selectedBuilding.transform.position.x, parentPlot.position.y, selectedBuilding.transform.position.z),
            selectedBuilding.transform.rotation);

        newBuilding.transform.SetParent(parentPlot, true);
        newBuilding.SetActive(true);
        Destroy(selectedBuilding);
        unmodifiedBuildings.Add(newBuilding);
    }
    
}
