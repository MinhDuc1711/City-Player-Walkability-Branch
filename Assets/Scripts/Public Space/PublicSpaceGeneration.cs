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
    private GameObject LeftBenches;
    private GameObject RightBenches;

    void Start()
    {
        if (publicSpaceSlider != null)
        {
            // Initialize holders for benches
            LeftBenches = new GameObject("Left Benches");
            RightBenches = new GameObject("Right Benches");
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
        System.Diagnostics.Stopwatch stopwatch = new();
        stopwatch.Start();
        ClearBenches();
        if (density != 0)
        {
            density = 35 - 2 * density; // from 30 (max) to 12 (min) average distance between each object
            SpawnBenchesWithSpacing(LeftStripStart.transform.position, LeftStripEnd.transform.position, density, LeftBenches.transform);
            SpawnBenchesWithSpacing(RightStripStart.transform.position, RightStripEnd.transform.position, density, RightBenches.transform);
        }
        stopwatch.Stop();
        Debug.Log("Public space script execution Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    void SpawnBenchesWithSpacing(Vector3 start, Vector3 end, float spacing, Transform parentObject)
    {
        float distance = Vector3.Distance(start, end); // total distance to place benches
        float currentDistance = 0f; // current distance relative to start point
        Vector3 direction = (end - start).normalized; // direction to calculate distance
        int benchesCreated = 0;
        float correctionFactor = 1; // variable for distance adjustment purposes

        while (currentDistance < distance - spacing*correctionFactor) // check if next entity will spawn outside allowed distance
        {
            Vector3 position = start + direction * currentDistance;
            int attempts = 1;
            bool placed = false;
            float maxAttempts = 12;
            
            while (attempts <= maxAttempts && !placed)
            {
                if (!IsOccupied(position))
                {
                    GameObject benchPrefab = BenchPrefabs[Random.Range(0, BenchPrefabs.Length)];
                    GameObject newBench = Instantiate(benchPrefab, position, benchPrefab.transform.rotation, parentObject);
                    benches.Add(newBench);
                    benchesCreated++;
                    newBench.tag = "Bench";
                    newBench.name = "Bench " + benchesCreated;
                    placed = true;
                    // Debug.Log("Bench " + benchesCreated + " spawned at: " + position + " in " + attempts + " attempts");
                }
                else
                {
                    float minStep = 1.0f;
                    float step = spacing * correctionFactor / maxAttempts;
                    Vector3 tempDir = (attempts % 2 == 1) ? direction : -direction;
                    // If bench not successfully placed because position is occupied by another object
                    if (step < minStep)
                    {
                        position += attempts * minStep * tempDir;
                    }
                    else
                    {
                        position += attempts * step * tempDir;
                    }
                    attempts++;
                }
            }
            
            currentDistance = Vector3.Dot(position-start, direction); // Update current distance after searching for space
            if (benchesCreated > 1)
            {
                // difference between current density and theoretical density
                float error = spacing * (benchesCreated - 1) / currentDistance;
                correctionFactor = Mathf.Pow(error, Mathf.Lerp(1.0f, 3.0f, currentDistance/distance));
                // Debug.Log("benchesCreated: " + benchesCreated + " currentDistance: " + currentDistance + ", correctionFactor: " + correctionFactor);
            }

            // Reduce or increase distance between current and next object, based on current density of objects
            float minDistance = 3.0f;
            if (spacing * correctionFactor < minDistance)
            {
                currentDistance += minDistance;
            }
            else
            {
                currentDistance += spacing * correctionFactor;
            }
        }
        Debug.Log(parentObject.name + " added: " + benchesCreated);
    }

    bool IsOccupied(Vector3 position)
    {
        float checkRadius = 2.0f;
        int layerMask = LayerMask.GetMask("Tree", "Flower", "Bench"); // Layers to check
        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius, layerMask);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Tree") || collider.CompareTag("Flower") || collider.CompareTag("Bench")) 
            {
                // Debug.Log("Position: " + position + " occupied with: " + collider.gameObject.name + " at " + collider.gameObject.transform.position);
                return true;
            }
        }
        if (ConnectScript != null)
        {
            foreach (var instance in ConnectScript.IntersectionInstances)
            {
                //The -12 is a custom offset, its a horrible fix but it works ish
                float intersectionZ = instance.transform.position.z-12;
                if (Mathf.Abs(position.z - intersectionZ) <= 10)
                {
                    // Debug.Log("Position: " + position + " occupied with intersection at: " + instance.transform.position);
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
            DestroyImmediate(bench);
        }
        benches.Clear();
    }
}
