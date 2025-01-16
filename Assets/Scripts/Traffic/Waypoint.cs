using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint previousWaypoint; 
    public Waypoint nextWaypoint;     

    [Range(0f, 5f)]
    public float width = 1f;          

    //Generates a random position within the bounds of the waypoint's width
    public Vector3 GetPosition()
    {
        Vector3 minBound = transform.position + transform.right * width / 2f;
        Vector3 maxBound = transform.position - transform.right * width / 2f;

        return Vector3.Lerp(minBound, maxBound, Random.Range(0f, 1f));
    }

    //Checks if this waypoint has a next waypoint
    public bool HasNextWaypoint()
    {
        return nextWaypoint != null;
    }
}
