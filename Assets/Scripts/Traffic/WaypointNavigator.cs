using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    CharacterNavigationController controller;
    public Waypoint currentWaypoint;
    public Waypoint lastWaypoint;
    public Waypoint[] possibleLastWaypoints;

    private PedestrianSpawner spawner;

    private void Awake()
    {
        controller = GetComponent<CharacterNavigationController>();
    }

    void Start()
    {
        //Fallback to find the spawner if it's not assigned
        if (spawner == null)
        {
            Debug.LogWarning("Spawner is null in Start! Attempting to find a spawner in the hierarchy.");
            spawner = FindObjectOfType<PedestrianSpawner>();
        }

        if (spawner == null)
        {
            Debug.LogError("No PedestrianSpawner found in the scene! Resetting pedestrian is not possible.");
            return;
        }

        //Assign a random last waypoint if available
        if (possibleLastWaypoints.Length > 0)
        {
            lastWaypoint = possibleLastWaypoints[Random.Range(0, possibleLastWaypoints.Length)];
        }

        //Set the initial destination if currentWaypoint is valid
        if (currentWaypoint != null)
        {
            controller.SetDestination(currentWaypoint.GetPosition());
        }
        else
        {
            Debug.LogWarning("Current waypoint is null in Start! Resetting pedestrian.");
            ResetToNewWaypoint();
        }
    }

    private void Update()
    {
        //Check for null references
        if (this == null || gameObject == null)
        {
            Debug.LogWarning("Pedestrian object is null! Resetting.");
            ResetToNewWaypoint();
            return;
        }

        if (currentWaypoint == null)
        {
            Debug.LogWarning("Current waypoint is null! Resetting pedestrian.");
            ResetToNewWaypoint();
            return;
        }

        //Handle normal navigation
        if (controller.reachedDestination)
        {
            if (Vector3.Distance(controller.destination, lastWaypoint.GetPosition()) < controller.stopDistance)
            {
                TeleportToRandomWaypoint();
            }
            else
            {
                controller.SetDestination(lastWaypoint.GetPosition());
            }
        }
    }

    private void TeleportToRandomWaypoint()
    {
        //Check if the spawner is valid
        if (spawner == null)
        {
            Debug.LogWarning("Spawner is null! Resetting the pedestrian.");
            ResetToNewWaypoint();
            return;
        }

        //Attempt to get a random waypoint
        Transform randomChild = spawner.transform.GetChild(Random.Range(0, spawner.transform.childCount));
        Waypoint randomWaypoint = randomChild.GetComponent<Waypoint>();

        if (randomWaypoint == null)
        {
            Debug.LogError("Selected child does not have a Waypoint component! Resetting pedestrian.");
            ResetToNewWaypoint();
            return;
        }

        //Set the new waypoint and destination
        currentWaypoint = randomWaypoint;
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    private void ResetToNewWaypoint()
    {
        //Ensure spawner is valid
        if (spawner == null)
        {
            Debug.LogError("Spawner is still null during reset! Cannot reset pedestrian.");
            return;
        }

        //Get a random starting waypoint
        Transform randomChild = spawner.transform.GetChild(Random.Range(0, spawner.transform.childCount));
        Waypoint randomWaypoint = randomChild.GetComponent<Waypoint>();

        if (randomWaypoint == null)
        {
            Debug.LogError("Reset failed: Randomly selected child does not have a Waypoint component!");
            return;
        }

        //Reset position to the new waypoint
        transform.position = randomWaypoint.GetPosition();
        currentWaypoint = randomWaypoint;

        //Set the destination to the new waypoint
        controller.SetDestination(currentWaypoint.GetPosition());
        Debug.Log($"Pedestrian reset to new waypoint: {randomWaypoint.name}");
    }

    public void SetSpawner(PedestrianSpawner spawner)
    {
        this.spawner = spawner;
    }
}
