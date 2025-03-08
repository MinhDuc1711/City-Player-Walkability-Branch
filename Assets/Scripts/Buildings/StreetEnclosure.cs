using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StreetEnclosure : MonoBehaviour
{
    [SerializeField] private float maxOffset = 3.0f; // Maximum distance buildings move towards the street
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject plotParent;
    [SerializeField] private List<GameObject> leftBuildings = new List<GameObject>();

    private Vector3[] originalPositions;

    void Start()
    {
        // Ensure buildings are initialized
        if (leftBuildings.Count == 0)
        {
            GetBuildings();
        }

        // Initialize positions array
        originalPositions = new Vector3[leftBuildings.Count];

        for (int i = 0; i < leftBuildings.Count; i++)
        {
            originalPositions[i] = leftBuildings[i].transform.position;
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
        leftBuildings.Clear(); // Clean list before adding new ones
        foreach (Transform plot in plotParent.transform.GetChild(0))
        {
            foreach (Transform child in plot.transform)
            {
                leftBuildings.Add(child.GetChild(0).gameObject);
            }
            
        }
        //EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }

    void UpdateBuildingPositions(float value)
    {
        float offset = Mathf.Lerp(0, maxOffset, value / slider.maxValue);

        for (int i = 0; i < leftBuildings.Count; i++)
        {
            Vector3 originalPos = originalPositions[i];
            leftBuildings[i].transform.position = new Vector3(originalPos.x + offset, originalPos.y, originalPos.z);
        }
    }
}