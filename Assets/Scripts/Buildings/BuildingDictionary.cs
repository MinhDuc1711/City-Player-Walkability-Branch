using UnityEngine;
using System.Collections.Generic;

public class BuildingDictionary : MonoBehaviour
{
    [SerializeField]
    GameObject plotParent;
    [Space(15)]
    [SerializeField]
    List<GameObject> buildingPrefabs = new List<GameObject>();
    [SerializeField]
    List<ItemStruct> buildingList = new List<ItemStruct>();

    [ContextMenu("GetBuildings")]
    void GetBuildings()
    {
        CleanBuildingList();
        foreach (Transform plot in plotParent.transform)
        {
            foreach (Transform child in plot.transform)
            {
                ItemStruct itemStruct = new ItemStruct();
            
                itemStruct.prefab = child.GetChild(0).gameObject;
                itemStruct.taken = false;

                buildingList.Add(itemStruct);
            }
        }
    }
    void CleanBuildingList()
    {
        buildingList.Clear();
    }   
}  

[System.Serializable]
public struct ItemStruct
{   
    public bool taken;
    public GameObject prefab;
}