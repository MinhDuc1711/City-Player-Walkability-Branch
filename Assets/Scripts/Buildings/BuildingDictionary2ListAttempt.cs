using System.Collections.Generic;
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
    private List<GameObject> modifiedBuildings = new List<GameObject>();

    private float interval;
    private int oldPercentage = 0;

    private void Start()
    {
        interval = 100.0f / slider.maxValue;
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
        float percentage = (value * interval) / 100.0f;
        //Debug.Log(percentage);

        //get number of buildings to be changed
        int numbOfBuildingsToChange = Mathf.RoundToInt(percentage * unmodifiedBuildings.Count);
        Debug.Log(numbOfBuildingsToChange);

        if (percentage > oldPercentage)
        {
            BuildingChange(numbOfBuildingsToChange);
        }
        if (percentage < oldPercentage)
        {
            BuildingReset(numbOfBuildingsToChange);
        }
    }

    private void BuildingChange(int count)
    {
        for (int c=0; c < count; c++)
        {
            int randomIndex = Random.Range(0, unmodifiedBuildings.Count);
            NewBuildingSpawn(randomIndex);
            modifiedBuildings.Add(unmodifiedBuildings[randomIndex]);
            unmodifiedBuildings.Remove(unmodifiedBuildings[randomIndex]);
        }
    }

    private void BuildingReset(int count)
    {

    }

    private void NewBuildingSpawn(int index)
    {
        int randomPrefab = Random.Range(0, buildingPrefabs.Count);
        GameObject buildingPrefab = buildingPrefabs[randomPrefab];
        GameObject selectedBuilding = unmodifiedBuildings[index].gameObject;
        Transform parentPlot = selectedBuilding.transform.parent;

        Debug.Log(parentPlot.gameObject.name);

        GameObject newBuilding = Instantiate(buildingPrefab, 
            new Vector3(selectedBuilding.transform.position.x, parentPlot.position.y + 1f, selectedBuilding.transform.position.z), 
            selectedBuilding.transform.rotation);

        Debug.Log(parentPlot.position.y);
        Debug.Log(newBuilding.transform.position.y);

        newBuilding.transform.SetParent(parentPlot, true);
        newBuilding.SetActive(true);
        Destroy(selectedBuilding);


    }
}
