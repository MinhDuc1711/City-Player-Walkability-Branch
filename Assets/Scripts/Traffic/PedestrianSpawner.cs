using UnityEngine;
using System.Collections;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject pedestrianPrefab;
    public int pedestriansToSpawn; //Maximum pedestrians allowed at once

    private int currentPedestrians = 0; //Tracks the number of pedestrians in the scene

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true) //Continuously attempt to spawn pedestrians
        {
            if (currentPedestrians < pedestriansToSpawn)
            {
                //Spawn pedestrian
                GameObject obj = Instantiate(pedestrianPrefab);

                //Select a random starting waypoint
                Transform randomStartWaypoint = transform.GetChild(Random.Range(0, transform.childCount));
                Waypoint startWaypoint = randomStartWaypoint.GetComponent<Waypoint>();

                //Place pedestrian at the selected waypoint's position
                obj.transform.position = randomStartWaypoint.position;

                //If there is a next waypoint, align the pedestrian to face it
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

                //Increment the pedestrian counter
                currentPedestrians++;

                //Pass reference of the spawner to the pedestrian to reduce count when destroyed
                obj.GetComponent<WaypointNavigator>().SetSpawner(this);
            }

            yield return new WaitForSeconds(0.5f); //Small delay between spawn checks
        }
    }

    //Decrement pedestrian count (called by pedestrians when destroyed)
    public void PedestrianDestroyed()
    {
        currentPedestrians--;
    }
}
