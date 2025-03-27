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

    public float ExtraSpacing=3;

    public ConnectivitySlider ConnectScript;
    private GameObject LeftGreenery;
    private GameObject RightGreenery;
    public void Start()
    {
        LeftGreenery = new GameObject("GeneratedLeftGreenery");
        RightGreenery = new GameObject("GeneratedRightGreenery");
        // Add listener
        greenObjSlider.onValueChanged.AddListener(OnGreenObjectSliderValueChanged);
        // Initial generation 
        GenerateGreenery(greenObjSlider.value);
    }

    void OnGreenObjectSliderValueChanged(float value)
    {
        Debug.Log("Green object slider value changed to: " + value);
        GenerateGreenery(value);
    }

    public void GenerateGreenery(float density)
    {
        ClearGreenery();

        if (density > 0)
        {
            density = 30 - 2 * density;
            SpawnGreeneryWithSpacing(LeftGreenStripStart.transform.position, LeftGreenStripEnd.transform.position, density, LeftGreenery.transform);
            SpawnGreeneryWithSpacing(RightGreenStripStart.transform.position, RightGreenStripEnd.transform.position, density, RightGreenery.transform);
        }
    }

    void SpawnGreeneryWithSpacing(Vector3 start, Vector3 end, float spacing, Transform parentObject)
    {
        float distance = Vector3.Distance(start, end);
        int numberOfObjects = Mathf.FloorToInt(distance / spacing);
        int TreeVariance = 0;

        float correctionFactor = 1;
        int greeneryCreated = 0; // number of benches created
        float currentDistance = 0f; // current distance relative to start point
        Vector3 direction = (end - start).normalized; // direction to calculate distance
        bool placeTree = true; // if true then place tree, if false then place flower

        while (currentDistance < distance - spacing * correctionFactor) // prevent spawning if next object wil be spawned outside of allowed range
        {
            Vector3 position = start + direction * currentDistance; // Calculate position
            int attempts = 0;
            bool placed = false;
            float maxAttempts = 15;
            while (attempts < maxAttempts && !placed)
            {
                if (!IsOccupied(position))
                {
                    if (placeTree)
                    {
                        float randomRotationY = 90 * Random.Range(0, 4); // 0, 90, 180, or 270
                        Quaternion rotation = Quaternion.Euler(0, randomRotationY, 0);
                        var newTree = Instantiate(TreePrefabs[TreeVariance % 2], position, rotation, parentObject);
                        newTree.name = "Greenery " + (greeneryCreated + 1);
                        TreeVariance++;
                        placeTree = false;
                    }
                    else
                    {
                        position.z += (float)0.75;
                        var newFlower = Instantiate(FlowerPrefab, position, Quaternion.identity, parentObject);
                        newFlower.name = "Greenery " + (greeneryCreated + 1);
                        placeTree = true;
                    }
                    greeneryCreated++;
                    // Debug.Log("Greenery spawned at: " + currentDistance + " in " + (attempts+1) + " attempts");
                    placed = true;
                }
                else
                {
                    // If object not successfully placed because position is occupied by another object
                    if (attempts % 2 == 0)
                    {
                        position += direction * attempts * spacing * correctionFactor / maxAttempts; // Move forward
                    }
                    else
                    {
                        position -= direction * attempts * spacing * correctionFactor / maxAttempts; // Move backward
                    }
                    // position += direction * attempts * spacing * correctionFactor / (maxAttempts*2);
                    attempts++;
                }
            }

            if (greeneryCreated > 1)
            {
                // average distance between current objects
                float actualSpacing = currentDistance / (greeneryCreated-1);
                // difference between current spacing and ideal maximum spacing
                float errorRatio = spacing / actualSpacing;
                correctionFactor = Mathf.Pow(errorRatio, Mathf.Lerp(1.0f, 3.0f, currentDistance/distance));
                // correctionFactor = errorRatio;
                Debug.Log("greeneryCreated: " + greeneryCreated + " currentDistance: " + currentDistance + ", actualSpacing: " + actualSpacing + ", idealSpacing: " + spacing + ", correctionFactor: " + correctionFactor);
            }
            else
            {
                correctionFactor = 1;
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

        Debug.Log("Maximum greenery objects allowed: " + numberOfObjects);
        Debug.Log("Greenery objects added: " + greeneryCreated);
    }

    private bool IsOccupied(Vector3 position)
    {
        float checkRadius = 2.0f; // Increase radius to avoid tree overlap
        int layerMask = LayerMask.GetMask("Tree", "Flower", "Bench");
        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius, layerMask);
        foreach (Collider collider in hitColliders)
        {
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

    public void ClearGreenery()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Tree"))
        {
            Destroy(obj);
        }

        foreach (var obj in GameObject.FindGameObjectsWithTag("Flower"))
        {
            Destroy(obj);
        }

    }
}
