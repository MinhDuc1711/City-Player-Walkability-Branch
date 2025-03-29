using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreeneryGeneration : MonoBehaviour
{
    public GameObject LeftGreenStripStart;
    public GameObject LeftGreenStripEnd;
    public GameObject RightGreenStripStart;
    public GameObject RightGreenStripEnd;
    public GameObject[] TreePrefabs;
    public GameObject FlowerPrefab;
    public Slider greenObjSlider;
    public ConnectivitySlider ConnectScript;
    private GameObject LeftGreenery;
    private GameObject RightGreenery;
    private List<GameObject> greeneryList = new List<GameObject>();

    public void Start()
    {
        // Initialize holder for greenery
        LeftGreenery = new GameObject("Left Greenery");
        RightGreenery = new GameObject("Right Greenery");
        // Add listener
        greenObjSlider.onValueChanged.AddListener(OnGreenObjectSliderValueChanged);
        // Initial generation 
        GenerateGreenery(greenObjSlider.value);
    }

    void OnGreenObjectSliderValueChanged(float value)
    {
        Debug.Log("Green object slider value changed to: " + value);
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        GenerateGreenery(value);
        stopwatch.Stop();
        Debug.Log("Greenery scrit execution Time: " + stopwatch.ElapsedMilliseconds + " ms");
    }

    public void GenerateGreenery(float density)
    {
        ClearGreenery();
        if (density > 0)
        {
            density = 32 - 2 * density; // from 30 (max) to 12 (min) average distance between each object
            SpawnGreeneryWithSpacing(LeftGreenStripStart.transform.position, LeftGreenStripEnd.transform.position, density, LeftGreenery.transform);
            SpawnGreeneryWithSpacing(RightGreenStripStart.transform.position, RightGreenStripEnd.transform.position, density, RightGreenery.transform);
        }
    }

    void SpawnGreeneryWithSpacing(Vector3 start, Vector3 end, float spacing, Transform parentObject)
    {
        float distance = Vector3.Distance(start, end); // total distance to place greenery
        float currentDistance = 0f; // current distance relative to start point
        Vector3 direction = (end - start).normalized; // direction to calculate distance
        int greeneryCreated = 0; // number of benches created
        float correctionFactor = 1;
        bool placeTree = true; // if true then place tree, if false then place flower
        int TreeVariance = 0;

        while (currentDistance < distance - spacing * correctionFactor) // prevent spawning if next object wil be spawned outside of allowed range
        {
            Vector3 position = start + direction * currentDistance;
            int attempts = 1;
            bool placed = false;
            float maxAttempts = 10;

            while (attempts <= maxAttempts && !placed)
            {
                if (!IsOccupied(position))
                {
                    if (placeTree)
                    {
                        float randomRotationY = 90 * Random.Range(0, 4); // 0, 90, 180, or 270
                        Quaternion rotation = Quaternion.Euler(0, randomRotationY, 0);
                        GameObject newTree = Instantiate(TreePrefabs[TreeVariance % 2], position, rotation, parentObject);
                        TreeVariance++;
                        placeTree = false;
                        newTree.name = "Tree " + (greeneryCreated + 1);
                        greeneryList.Add(newTree);
                    }
                    else
                    {
                        GameObject newFlower = Instantiate(FlowerPrefab, position, Quaternion.identity, parentObject);
                        placeTree = true;
                        newFlower.name = "Flower " + (greeneryCreated + 1);
                        greeneryList.Add(newFlower);
                    }
                    greeneryCreated++;
                    // Debug.Log("Greenery " + greeneryCreated + " spawned at: " + position + " in " + attempts + " attempts");
                    placed = true;
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

            currentDistance = Vector3.Dot(position-start, direction);
            if (greeneryCreated > 1)
            {
                // difference between current density and theoretical density
                float error = spacing * (greeneryCreated - 1) / currentDistance;
                correctionFactor = Mathf.Pow(error, Mathf.Lerp(1.0f, 3.0f, currentDistance/distance));
                // Debug.Log("greeneryCreated: " + greeneryCreated + " currentDistance: " + currentDistance + " correctionFactor: " + correctionFactor);
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
        Debug.Log(parentObject.name + " added: " + greeneryCreated);
    }

    private bool IsOccupied(Vector3 position)
    {
        float checkRadius = 4.0f;
        int layerMask = LayerMask.GetMask("Tree", "Flower", "Bench");
        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius, layerMask);
        foreach (Collider collider in hitColliders)
        {
            // Debug.Log("Position: " + position + " occupied with: " + collider.gameObject.name + " at " + collider.gameObject.transform.position);
            if (collider.CompareTag("Tree") || collider.CompareTag("Flower") || collider.CompareTag("Bench")) 
            {
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

    public void ClearGreenery()
    {
        foreach (GameObject greenery in greeneryList)
        {
            DestroyImmediate(greenery);
        }
        greeneryList.Clear();
    }
}
