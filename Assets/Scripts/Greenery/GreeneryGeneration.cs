using UnityEngine;
using UnityEngine.UI;

public class GreeneryGeneration : MonoBehaviour
{
    public GameObject LeftGreenStripStart;
    public GameObject LeftGreenStripEnd;
    public GameObject RightGreenStripStart;
    public GameObject RightGreenStripEnd;

    public GameObject TreePrefab;

    public Slider treeDensitySlider; // Reference to the slider


    public void Start()
    {
        // Add a listener to detect slider value changes
        treeDensitySlider.onValueChanged.AddListener(OnSliderValueChanged);

        // Initial tree generation based on the default slider value
        GenerateTrees(treeDensitySlider.value);
    }

    public void Update()
    {

    }

    void OnSliderValueChanged(float value)
    {
        // Regenerate trees whenever the slider value changes
        Debug.Log("Slider value changed to: " + value);
        GenerateTrees(value);
    }

    void GenerateTrees(float density)
    {
        // Calculate spacing based on density (inverted logic)
        //float spacing = density == 0 ? float.MaxValue : 10f / density;
        if (density != 0)
        {

            density = 11 - density;

        }


        // Clear existing trees
        ClearTrees();

        // Spawn trees on both green strips
        SpawnTreesWithSpacing(LeftGreenStripStart.transform.position, LeftGreenStripEnd.transform.position, density);
        SpawnTreesWithSpacing(RightGreenStripStart.transform.position, RightGreenStripEnd.transform.position, density);
    }

    void SpawnTreesWithSpacing(Vector3 start, Vector3 end, float spacing)
    {
        float distance = Vector3.Distance(start, end);
        int numberOfTrees = Mathf.FloorToInt(distance / spacing);

        for (int i = 0; i <= numberOfTrees; i++)
        {
            float t = (float)i / numberOfTrees; // Normalized position (0 to 1)
            Vector3 position = Vector3.Lerp(start, end, t);

            Instantiate(TreePrefab, position, Quaternion.identity);
        }
    }

    void ClearTrees()
    {
        // Destroy all trees in the scene to avoid overlapping
        foreach (var tree in GameObject.FindGameObjectsWithTag("Tree"))
        {
            Destroy(tree);
        }
    }

    /**
    void SpawnTreesBetween(Vector3 start, Vector3 end)
    {
        for (int i = 0; i <= numberOfTrees; i++)
        {
            // Calculate the position along the line
            float t = (float)i / numberOfTrees; // Normalized position (0 to 1)
            Vector3 position = Vector3.Lerp(start, end, t);

            // Instantiate the tree at the calculated position
            Instantiate(TreePrefab, position, Quaternion.identity);
        }
    }

    **/
}
