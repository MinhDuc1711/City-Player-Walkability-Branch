using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    [Header("Car Spawner Settings")]
    public List<GameObject> carPrefabs;        // List of car prefabs to spawn
    public Transform waypointsRoot;            // Root object for waypoints
    public int maxCars = 10;                   // Maximum number of cars
    public float spawnInterval = 1f;           // Time between spawns

    private int currentCarCount = 0;           // Active car count

    private void Start()
    {
        if (waypointsRoot == null)
        {
            Debug.LogError("Waypoints root not assigned!");
            return;
        }

        // Start the spawning coroutine
        StartCoroutine(SpawnCars());
    }

    private IEnumerator SpawnCars()
    {
        while (true)
        {
            if (currentCarCount < maxCars)
            {
                SpawnCar();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCar()
    {
        // Randomly select a car prefab
        GameObject selectedCarPrefab = carPrefabs[Random.Range(0, carPrefabs.Count)];

        // Instantiate the car
        GameObject car = Instantiate(selectedCarPrefab);

        // Get the CarNavigationController component
        CarNavigationController carController = car.GetComponent<CarNavigationController>();
        if (carController == null)
        {
            Debug.LogError("Car prefab missing CarNavigationController!");
            Destroy(car);
            return;
        }

        // Assign first and last waypoints
        carController.Initialize(waypointsRoot.GetChild(0).GetComponent<Waypoint>(),
                                 waypointsRoot.GetChild(waypointsRoot.childCount - 1).GetComponent<Waypoint>(),
                                 this);

        // Increment car count
        currentCarCount++;
    }

    public void CarDestroyed()
    {
        currentCarCount--;
        if (currentCarCount < 0) currentCarCount = 0; // Safety check
    }
}
