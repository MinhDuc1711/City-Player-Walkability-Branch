using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StreetEnclosure : MonoBehaviour
{
    [SerializeField] private float maxOffset = 3.0f; // Maximum distance buildings move towards the street
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject plotParent;
    [SerializeField] private List<GameObject> buildings = new List<GameObject>();

    private Vector3[] originalPositions;

    void Start()
    {
        // Ensure buildings are initialized
        if (buildings.Count == 0)
        {
            GetBuildings();
        }

        // Initialize positions array
        originalPositions = new Vector3[buildings.Count];

        for (int i = 0; i < buildings.Count; i++)
        {
            originalPositions[i] = buildings[i].transform.position;
        }

        // Attach slider event listener
        if (slider != null)
        {
            slider.onValueChanged.AddListener(UpdateBuildingPositions);
        }
        else
        {
            Debug.LogError("Slider is not assigned in the Inspector!");
        }
    }

    [ContextMenu("Get Buildings")]
    void GetBuildings()
    {
        buildings.Clear(); // Clean list before adding new ones
        foreach (Transform plot in plotParent.transform)
        {
            foreach (Transform child in plot.transform)
            {
                buildings.Add(child.GetChild(0).gameObject);
            }
        }
        //EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }

    void UpdateBuildingPositions(float value)
    {
        float offset = Mathf.Lerp(0, maxOffset, value / slider.maxValue);

        for (int i = 0; i < buildings.Count; i++)
        {
            Vector3 originalPos = originalPositions[i];
            buildings[i].transform.position = new Vector3(originalPos.x + offset, originalPos.y, originalPos.z);
        }
    }
}