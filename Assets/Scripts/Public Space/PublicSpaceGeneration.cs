using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PublicSpaceGeneration : MonoBehaviour
{
    public GameObject LeftStripStart;
    public GameObject LeftStripEnd;
    public GameObject RightStripStart;
    public GameObject RightStripEnd;

    public GameObject[] BenchPrefabs; // Array for different bench types

    public Slider publicSpaceSlider;

    private List<GameObject> benches = new List<GameObject>();

    void Start()
    {
        if (publicSpaceSlider != null)
        {
            publicSpaceSlider.onValueChanged.AddListener(OnPublicSpaceSliderValueChanged);
        }
        else
        {
            Debug.LogError("PublicSpaceSlider is not assigned in the Inspector!");
        }
    }


    public void OnPublicSpaceSliderValueChanged(float value)
    {
        Debug.Log("Public space slider value changed to: " + value);
        GenerateBenches(value);
    }

    void GenerateBenches(float density)
    {
        ClearBenches();

        // Adjust density to increase with slider's right side
        float adjustedDensity = 1 - density; // Invert the value: 0 -> 1, 1 -> 0

        // Adjust spacing based on inverted density
        float spacing = Mathf.Lerp(5f, 15f, adjustedDensity);  // Closer benches at higher density

        SpawnBenchesWithSpacing(LeftStripStart.transform.position, LeftStripEnd.transform.position, spacing);
        SpawnBenchesWithSpacing(RightStripStart.transform.position, RightStripEnd.transform.position, spacing);
    }


    void SpawnBenchesWithSpacing(Vector3 start, Vector3 end, float spacing)
    {
        float distance = Vector3.Distance(start, end);
        int numberOfBenches = Mathf.FloorToInt(distance / spacing);

        for (int i = 0; i <= numberOfBenches; i++)
        {
            float t = (float)i / numberOfBenches;
            Vector3 position = Vector3.Lerp(start, end, t);

            // Check if a tree is nearby (avoid placing a bench on a tree)
            if (!IsOccupiedByTree(position))
            {
                GameObject benchPrefab = BenchPrefabs[Random.Range(0, BenchPrefabs.Length)];
                GameObject newBench = Instantiate(benchPrefab, position, Quaternion.identity);
                newBench.tag = "Bench";
                benches.Add(newBench);
            }
        }
    }

    bool IsOccupiedByTree(Vector3 position)
    {
        float checkRadius = 3.0f; // Increase radius to avoid tree overlap
        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Tree")) return true;
        }
        return false;
    }


    void ClearBenches()
    {
        foreach (GameObject bench in benches)
        {
            Destroy(bench);
        }
        benches.Clear();
    }
}
