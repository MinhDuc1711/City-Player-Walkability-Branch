using UnityEngine;
using UnityEngine.UI;

public class GreeneryGeneration : MonoBehaviour
{
    public GameObject LeftGreenStripStart;
    public GameObject LeftGreenStripEnd;
    public GameObject RightGreenStripStart;
    public GameObject RightGreenStripEnd;

    public GameObject TreePrefab;
    public GameObject GreenObjectPrefab;

    public Slider treeDensitySlider; // Reference to the tree slider
    public Slider greenObjectSlider;  // / reference to green objects (flowers, bushes, etc.)

    public void Start()
    {
        // Add a listener to detect slider value changes
        treeDensitySlider.onValueChanged.AddListener(OnTreeSliderValueChanged);
        greenObjectSlider.onValueChanged.AddListener(OnGreenObjectSliderValueChanged);

        // Initial tree generation based on the default slider value
        GenerateTrees(treeDensitySlider.value);
        GenerateGreenObjects(greenObjectSlider.value);
    }

    void OnGreenObjectSliderValueChanged(float value)
    {
        // Regenerate green objects whenever the green object slider value changes
        Debug.Log("Green object slider value changed to: " + value);
        GenerateGreenObjects(value);
    }

    void OnTreeSliderValueChanged(float value)
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

    void GenerateGreenObjects(float density)
    {
        // Adjust density logic for green objects
        if (density != 0)
        {
            density = 11 - density;
        }

        // Clear existing green objects
        ClearObjectsWithTag("GreenObject");

        // Spawn green objects on both green strips
        SpawnObjectsWithSpacing(LeftGreenStripStart.transform.position, LeftGreenStripEnd.transform.position, density, GreenObjectPrefab, "GreenObject");
        SpawnObjectsWithSpacing(RightGreenStripStart.transform.position, RightGreenStripEnd.transform.position, density, GreenObjectPrefab, "GreenObject");
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
    void SpawnObjectsWithSpacing(Vector3 start, Vector3 end, float spacing, GameObject prefab, string tag)
    {
        float distance = Vector3.Distance(start, end);
        int numberOfObjects = Mathf.FloorToInt(distance / spacing);

        for (int i = 0; i <= numberOfObjects; i++)
        {
            float t = (float)i / numberOfObjects; 
            Vector3 position = Vector3.Lerp(start, end, t);

            GameObject newObject = Instantiate(prefab, position, Quaternion.identity);
            newObject.tag = tag; 
        }
    }

    void ClearObjectsWithTag(string tag)
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag(tag))
        {
            Destroy(obj);
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
