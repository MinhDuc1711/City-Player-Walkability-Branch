using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingVarietyController : MonoBehaviour
{
    public GameObject buildingStreet;

    [Tooltip("List of building prefabs that will be used to introduce variety.")]
    public List<GameObject> buildingPrefabs;

    [Tooltip("Reference to the slider controlling the variety percentage.")]
    public Slider varietySlider;

    // Keeps track of which buildings have been replaced so they aren't re-replaced
    private List<int> currentReplacedIndices = new List<int>();

    private void Start()
    {
        
        if (varietySlider != null)
        {
            varietySlider.onValueChanged.AddListener(UpdateBuildingVariety);
        }
        else
        {
            Debug.LogError("Variety slider is not assigned in the inspector!");
        }
        ResetBuildings();
    }

    /// <summary>
    /// Called whenever the slider value is changed.
    /// </summary>
    /// <param name="value">The new value of the slider (0 to 100).</param>
    public void UpdateBuildingVariety(float value)
    {
        // Convert slider value to a percentage (0 to 100)
        Debug.Log($"Slider value: {value}");
        float percentage = value / 100f;

        // Calculate how many buildings to replace based on the percentage
        int totalPlots = buildingStreet.transform.childCount;
        int targetReplacedBuildings = Mathf.RoundToInt(totalPlots * percentage);

        // If more buildings need to be replaced
        if (targetReplacedBuildings > currentReplacedIndices.Count)
        {
            int buildingsToAdd = targetReplacedBuildings - currentReplacedIndices.Count;
            ReplaceRandomBuildings(buildingsToAdd);
        }
        // If fewer buildings need to be replaced (slider moved down)
        else if (targetReplacedBuildings < currentReplacedIndices.Count)
        {
            int buildingsToRemove = currentReplacedIndices.Count - targetReplacedBuildings;
            RestoreDefaultBuildings(buildingsToRemove);
        }
    }

    /// <summary>
    /// Replace random buildings on the street with prefab buildings.
    /// </summary>
    /// <param name="count">How many buildings to replace.</param>
    private void ReplaceRandomBuildings(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Get a list of plots that have not been replaced yet
            List<int> availablePlots = new List<int>();
            for (int j = 0; j < buildingStreet.transform.childCount; j++)
            {
                if (!currentReplacedIndices.Contains(j))
                {
                    availablePlots.Add(j);
                }
            }
            //Debug.Log($"Available Plots: {availablePlots.Count}");

            if (availablePlots.Count == 0) return; // No available plots to change

            // Pick a random plot to replace
            int randomIndex = Random.Range(0, availablePlots.Count);
            int plotIndex = availablePlots[randomIndex];

            GameObject newBuildingPrefab = buildingPrefabs[Random.Range(0, buildingPrefabs.Count)];
            GameObject plot = buildingStreet.transform.GetChild(plotIndex).gameObject;
            ReplaceBuilding(plot, newBuildingPrefab);


            // Track that this plot now has a custom building
            currentReplacedIndices.Add(plotIndex);
        }
    }


