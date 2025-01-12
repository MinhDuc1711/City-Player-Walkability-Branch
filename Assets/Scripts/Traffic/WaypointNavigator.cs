using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    //Lane Preference set to not overlap with other waypoints
    public enum Lane { Left, Right } 
    public Lane lanePreference;    

    public Waypoint currentWaypoint;
    public Waypoint lastWaypoint;
    public Waypoint[] possibleLastWaypoints;

    private PedestrianSpawner spawner;
    private CharacterNavigationController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterNavigationController>();
    }

    void Start()
    {
        if (spawner == null)
        {
            spawner = FindObjectOfType<PedestrianSpawner>();
        }

        if (spawner == null)
        {
            Debug.LogError("No PedestrianSpawner found in the scene! Resetting pedestrian is not possible.");
            return;
        }

        if (possibleLastWaypoints.Length > 0)
        {
            lastWaypoint = possibleLastWaypoints[Random.Range(0, possibleLastWaypoints.Length)];
        }

        if (currentWaypoint != null)
        {
            controller.SetDestination(currentWaypoint.GetPosition());
        }
        else
        {
            ResetToNewWaypoint();
        }
    }

    void Update()
    {
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

    private void TeleportToRandomWaypoint() //Teleport to a random waypoint
    {
        Transform waypointGroup = lanePreference == Lane.Left ? spawner.waypointsLeftLane : spawner.waypointsRightLane;

        Transform randomChild = waypointGroup.GetChild(Random.Range(0, waypointGroup.childCount));
        Waypoint randomWaypoint = randomChild.GetComponent<Waypoint>();

        if (randomWaypoint == null) //error check
        {
            Debug.LogError("Selected child does not have a Waypoint component!");
            return;
        }

        currentWaypoint = randomWaypoint;
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    //reset pedestrian
    private void ResetToNewWaypoint()
    {
        Transform waypointGroup = lanePreference == Lane.Left ? spawner.waypointsLeftLane : spawner.waypointsRightLane;

        Transform randomChild = waypointGroup.GetChild(Random.Range(0, waypointGroup.childCount));
        Waypoint randomWaypoint = randomChild.GetComponent<Waypoint>();

        if (randomWaypoint == null)
        {
            Debug.LogError("Reset failed: Randomly selected child does not have a Waypoint component!");
            return;
        }

        transform.position = randomWaypoint.GetPosition();
        currentWaypoint = randomWaypoint;
        controller.SetDestination(currentWaypoint.GetPosition());
    }

    public void SetSpawner(PedestrianSpawner spawner)
    {
        this.spawner = spawner;
    }
}
