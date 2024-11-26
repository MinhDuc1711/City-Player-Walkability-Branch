using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PedestrianSpawner : MonoBehaviour
{
    public List<GameObject> pedestrianPrefabs; 
    public Transform waypointsLeftLane;       
    public Transform waypointsRightLane;     
    public int pedestriansToSpawn;           
    public Transform trafficSystemParent;    

    private int currentPedestrians = 0;

    private void Start()
    {
        if (trafficSystemParent == null)
        {
            Debug.LogError("Traffic System parent is not assigned in the Inspector!");
        }
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (currentPedestrians < pedestriansToSpawn)
            {
                //Randomly select a pedestrian prefab
                GameObject selectedPrefab = pedestrianPrefabs[Random.Range(0, pedestrianPrefabs.Count)];
                WaypointNavigator nav = selectedPrefab.GetComponent<WaypointNavigator>();

                if (nav == null)
                {
                    Debug.LogError("Selected prefab does not have a WaypointNavigator component!");
                    yield break;
                }

                //Determine the lane based on the pedestrian's lane preference
                Transform selectedWaypointGroup = nav.lanePreference == WaypointNavigator.Lane.Left ? waypointsLeftLane : waypointsRightLane;

                if (selectedWaypointGroup == null)
                {
                    Debug.LogError("Waypoints group is not assigned!");
                    yield break;
                }

                //Select a random starting waypoint from the chosen lane
                Transform randomStartWaypoint = selectedWaypointGroup.GetChild(Random.Range(0, selectedWaypointGroup.childCount));
                Waypoint startWaypoint = randomStartWaypoint.GetComponent<Waypoint>();

                if (startWaypoint == null)
                {
                    Debug.LogError("Selected waypoint does not have a Waypoint component!");
                    yield break;
                }

                //Spawn pedestrian at the waypoint's position
                GameObject obj = Instantiate(selectedPrefab, randomStartWaypoint.position, Quaternion.identity);

                //Set the spawned pedestrian's parent to the Traffic System
                obj.transform.SetParent(trafficSystemParent);

                //Align pedestrian to face the next waypoint (if available)
                if (startWaypoint.nextWaypoint != null)
                {
                    Vector3 direction = (startWaypoint.nextWaypoint.GetPosition() - startWaypoint.GetPosition()).normalized;
                    obj.transform.rotation = Quaternion.LookRotation(direction);
                }
                else
                {
                    Debug.LogWarning("Start waypoint does not have a next waypoint!");
                }

                //Assign the starting waypoint to the pedestrian
                obj.GetComponent<WaypointNavigator>().currentWaypoint = startWaypoint;

                //Increment the pedestrian count
                currentPedestrians++;

                //Pass reference of the spawner to the pedestrian
                obj.GetComponent<WaypointNavigator>().SetSpawner(this);
            }

            yield return new WaitForSeconds(0.5f); //Delay between spawn attempts
        }
    }

    public void PedestrianDestroyed()
    {
        currentPedestrians--;
    }
}
