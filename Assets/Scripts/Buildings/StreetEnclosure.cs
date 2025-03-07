using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StreetEnclosureController : MonoBehaviour
{
    [SerializeField] private float maxOffset = 3.0f; // Maximum distance buildings move towards the street
    [SerializeField]
    Slider slider;
    [SerializeField]
    private GameObject plotParent;
    [SerializeField]
    private List<GameObject> buildings = new List<GameObject>();

    private Vector3[] originalPositions;

    void Start()
    {
        // Store the initial positions of buildings in left and right plot holders
        int count = buildings.childCount;

        originalPositions = new Vector3[leftCount];
        rightOriginalPositions = new Vector3[rightCount];

        // Attach slider event listener
        streetEnclosureSlider.onValueChanged.AddListener(UpdateBuildingPositions);
    }

    [ContextMenu("Get Buildings")]
    void GetBuildings()
    {
        CleanBuildingList();
        foreach (Transform plot in plotParent.transform)
        {
            foreach (Transform child in plot.transform)
            {
                buildings.Add(child.GetChild(0).gameObject);
            }
        }
        EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }

    void UpdateBuildingPositions(float value)
    {
        float offset = Mathf.Lerp(0, maxOffset, value / slider.maxValue);

        foreach (Transform building in buildings)
        {
            building.position = new Vector3(originalPositions[i].x + offset, originalPositions[i].y, originalPositions[i].z);
        }

    }
}