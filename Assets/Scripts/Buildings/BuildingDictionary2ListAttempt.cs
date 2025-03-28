using System.Collections.Generic;
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

    private List<int> buildingsToReset = new List<int>();
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
        #if UNITY_EDITOR
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
        #endif
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
            this.numOfBuildingsChanged = Mathf.FloorToInt(Mathf.Round(percentage * unmodifiedBuildings.Count));
            flag = false;
        }

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
            
            if (newBuildingType)
            {
                buildingsToReset.Add(randomIndex);
                BuildingSpawn(randomIndex);
                Debug.Log(buildingsToReset[c]);
            }
            else
            {
                unmodifiedBuildings[buildingsToReset[0]].SetActive(true);
                Destroy(buildingList[randomIndex].gameObject);
                buildingList.Remove(buildingList[randomIndex]);
                buildingsToReset.RemoveAt(0);
            }   
        }
    }

    private void BuildingSpawn(int index)
    {
       int randomPrefab = Random.Range(0, buildingPrefabs.Count);
       GameObject buildingPrefab = buildingPrefabs[randomPrefab];

       GameObject selectedBuilding = unmodifiedBuildings[index].gameObject;
       Transform parentPlot = selectedBuilding.transform.parent;

       GameObject newBuilding = Instantiate(buildingPrefab,
           new Vector3(selectedBuilding.transform.position.x, parentPlot.position.y, selectedBuilding.transform.position.z),
           selectedBuilding.transform.rotation);

       newBuilding.transform.SetParent(parentPlot, true);
       newBuilding.SetActive(true);
       selectedBuilding.SetActive(false);
       modifiedBuildings.Add(newBuilding); 
    }
    
}
