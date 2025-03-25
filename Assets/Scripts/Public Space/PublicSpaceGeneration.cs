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

    public ConnectivitySlider ConnectScript;
    private List<GameObject> benches = new List<GameObject>();

    void Start()
    {
        if (publicSpaceSlider != null)
        {
            publicSpaceSlider.onValueChanged.AddListener(OnPublicSpaceSliderValueChanged);
            GenerateBenches(publicSpaceSlider.value);
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

    public void GenerateBenches(float density)
    {
        ClearBenches();

        // Adjust density to increase with slider's right side
        float adjustedDensity = 1 - density/10f; // Invert the value: 0 -> 1, 1 -> 0

        // Adjust spacing based on inverted density
        float spacing = Mathf.Lerp(5f, 15f, adjustedDensity);  // Closer benches at higher density

        if (density != 0)
        {
            SpawnBenchesWithSpacing(LeftStripStart.transform.position, LeftStripEnd.transform.position, spacing);
            SpawnBenchesWithSpacing(RightStripStart.transform.position, RightStripEnd.transform.position, spacing);
        }
    }


    void SpawnBenchesWithSpacing(Vector3 start, Vector3 end, float spacing)
    {
        float distance = Vector3.Distance(start, end); // total distance to place benches
        float currentDistance = 0f; // current distance relative to start point
        Vector3 direction = (end - start).normalized; // direction to calculate distance

        int numberOfBenches = Mathf.FloorToInt(distance / spacing); // maximum number of benches able to bewcreated given density
        int benchesCreated = 0; // number of benches created
        float correctionFactor = 1;

        while (currentDistance < distance)
        {
            Vector3 position = start + direction * currentDistance; // Calculate position
            int attempts = 0;
            bool placed = false;
            float maxAttempts = 15;
            while (attempts < maxAttempts && !placed)
            {
                if (!IsOccupied(position))
                {
                    GameObject benchPrefab = BenchPrefabs[Random.Range(0, BenchPrefabs.Length)];
                    GameObject newBench = Instantiate(benchPrefab, position, benchPrefab.transform.rotation);
                    newBench.tag = "Bench";
                    benches.Add(newBench);
                    benchesCreated++;
                    Debug.Log("Bench spawned at: " + currentDistance + " in " + (attempts+1) + " attempts");
                    placed = true;
                }
                else
                {
                    // If bench not successfully placed because position is occupied by another object
                    if (attempts % 2 == 0)
                    {
                        position += direction * attempts * spacing * correctionFactor / maxAttempts; // Move forward
                    }
                    else
                    {
                        position -= direction * attempts * spacing * correctionFactor / maxAttempts; // Move backward
                    }
                    attempts++;
                }
            }

            if (benchesCreated > 1)
            {
                // average distance between current objects
                float actualSpacing = currentDistance / benchesCreated;
                // difference between current spacing and ideal maximum spacing
                float errorRatio = spacing / actualSpacing;
                correctionFactor = Mathf.Pow(errorRatio, Mathf.Lerp(1.0f, 3.0f, currentDistance/distance));
                // correctionFactor = errorRatio;
                Debug.Log("currentDistance" + currentDistance + ", actualSpacing: " + actualSpacing + ", idealSpacing: " + spacing + ", correctionFactor: " + correctionFactor);
            }
            else
            {
                correctionFactor = 1;
            }
            // Reduce or increase distance between current and next object, based on current density of objects
            currentDistance += spacing * correctionFactor;
        }

        Debug.Log("Maximum benches allowed: " + numberOfBenches);
        Debug.Log("Benches added: " + benchesCreated);
    }

    bool IsOccupied(Vector3 position)
    {
        float checkRadius = 1.0f;
        int layerMask = LayerMask.GetMask("Tree", "Flower", "Bench");
        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius, layerMask);
        foreach (Collider collider in hitColliders)
        {
            // This should be reworked to include any type of object
            if (collider.CompareTag("Tree") || collider.CompareTag("Flower") || collider.CompareTag("Bench")) return true;
        }
        if (ConnectScript != null)
        {
            foreach (var instance in ConnectScript.IntersectionInstances)
            {
                //The -12 is a custom offset, its a horrible fix but it works ish
                float intersectionZ = instance.transform.position.z-12;
                if (Mathf.Abs(position.z - intersectionZ) <= 10)
                {
                    return true;
                }
            }
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
