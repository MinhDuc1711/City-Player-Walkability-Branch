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

    public int numberOfTrees = 10; // Number of trees to spawn on each strip

    public void Start()
    {
        SpawnTreesBetween(LeftGreenStripStart.transform.position, LeftGreenStripEnd.transform.position);
        SpawnTreesBetween(RightGreenStripStart.transform.position, RightGreenStripEnd.transform.position);
    }

    public void Update()
    {
        
    }

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


}
