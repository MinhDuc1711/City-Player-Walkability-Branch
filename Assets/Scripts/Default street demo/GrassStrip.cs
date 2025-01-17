using System.Collections.Generic;
using UnityEngine;

public class GrassStrip : MonoBehaviour
{
    public GameObject grassBladePrefab;  // The grass blade prefab to be used.
    public float bladeSpacing = 0.5f;    // Distance between each blade of grass.
    public int stripWidth = 1;           // How many rows of grass blades there should be.
    public int initialPoolSize = 100;    // Initial number of blades in the pool.

    private Queue<GameObject> grassPool; // Object pool to reuse grass blades.
    private List<GameObject> activeBlades; // Currently active grass blades.

    private void Awake()
    {
        grassPool = new Queue<GameObject>();
        activeBlades = new List<GameObject>();

        // Pre-instantiate a pool of grass blades.
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject blade = Instantiate(grassBladePrefab);
            blade.SetActive(false);  // Keep them inactive initially.
            grassPool.Enqueue(blade);
        }
    }

    private void Start()
    {
        GenerateGrass();
    }

    private void GenerateGrass()
    {
        // Get the size of the strip
        Vector3 stripSize = transform.localScale;

        // Calculate the number of grass blades based on strip size
        int bladesInX = Mathf.CeilToInt(stripSize.x / bladeSpacing); // Number of grass blades in the X direction
        int bladesInZ = Mathf.CeilToInt(stripSize.z / bladeSpacing); // Number of grass blades in the Z direction

        // Clear active blades list (reuse the same blades for the new strip size)
        foreach (var blade in activeBlades)
        {
            blade.SetActive(false);  // Deactivate any blades from the previous size
            grassPool.Enqueue(blade);  // Return the blade to the pool
        }
        activeBlades.Clear();

        // Iterate over the strip and position grass blades
        for (int i = 0; i < bladesInX; i++)
        {
            for (int j = 0; j < bladesInZ; j++)
            {
                // Calculate position for each blade of grass
                Vector3 position = new Vector3(
                    transform.position.x + i * bladeSpacing - stripSize.x / 2, // X position
                    transform.position.y, // Y position (on the ground)
                    transform.position.z + j * bladeSpacing - stripSize.z / 2 // Z position
                );

                // Get a grass blade from the pool, or create a new one if the pool is empty
                GameObject blade = GetPooledGrassBlade();
                blade.transform.position = position;
                blade.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                blade.SetActive(true);  // Activate the blade

                // Add it to the active list
                activeBlades.Add(blade);
            }
        }
    }

    // Helper function to get a grass blade from the pool
    private GameObject GetPooledGrassBlade()
    {
        if (grassPool.Count > 0)
        {
            // Dequeue an inactive blade from the pool
            return grassPool.Dequeue();
        }
        else
        {
            // If the pool is empty, instantiate a new blade
            GameObject newBlade = Instantiate(grassBladePrefab);
            return newBlade;
        }
    }
}
