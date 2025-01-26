using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 1f;       
    public float rotationSpeed = 120f;    
    public float stopDistance = 2.5f;     
    public float speedVariationRange = 0.2f; 

    [Header("Destination")]
    public Vector3 destination;           
    public bool reachedDestination = false; 
    public float maxXMovement = 1f;      
    public float maxYMovement = 1f;      

    private Vector3 lastPosition;         
    public Vector3 spawnPosition;                  

    public void Start()
    {
        //Save the spawn position and apply random speed variation
        spawnPosition = transform.position;
        movementSpeed += Random.Range(-speedVariationRange, speedVariationRange);
        movementSpeed = Mathf.Clamp(movementSpeed, 0.5f, 2f); //Clamp to ensure the speed is realistic

        lastPosition = transform.position;
    }

    public void Update()
    {
        //Null check to ensure the object is valid
        if (this == null || gameObject == null) return;

        //Check if the pedestrian has moved too far on the X-axis or Y-axis
        if (Mathf.Abs(transform.position.x - spawnPosition.x) > maxXMovement ||
            Mathf.Abs(transform.position.y - spawnPosition.y) > maxYMovement)
        {
            Debug.Log("Pedestrian moved too far, teleporting.");

            //Teleport the pedestrian to the first or last waypoint
            TeleportToWaypoint();
            return; //Exit Update to prevent further movement checks during this frame
        }

        //Check if the destination has been reached
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0; //Ignore vertical component

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;

                //Rotate towards the destination
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                //Move forward
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }
        }

        //Update velocity and animations
        Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        velocity.y = 0; //Ignore vertical component
        float velocityMagnitude = velocity.magnitude;
        velocity.Normalize();

        //Calculate dot products for animation
        float fwdDotProduct = Vector3.Dot(transform.forward, velocity);
        float rightDotProduct = Vector3.Dot(transform.right, velocity);

    }

    //Sets the destination for the character
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }

    public void TeleportToWaypoint()
    {
        //Get the WaypointNavigator component
        WaypointNavigator navigator = GetComponent<WaypointNavigator>();

        if (navigator == null)
        {
            Debug.LogError("WaypointNavigator component is missing from the pedestrian!");
            return;
        }

        if (navigator.currentWaypoint == null)
        {
            Debug.LogError("Current waypoint is null! Ensure waypoints are properly assigned.");
            return;
        }

        Waypoint targetWaypoint = null;

        //Check waypoint connections and decide teleport destination
        if (Random.value > 0.5f && navigator.currentWaypoint.previousWaypoint != null)
        {
            targetWaypoint = navigator.currentWaypoint.previousWaypoint; //Teleport to the first waypoint
        }
        else if (navigator.lastWaypoint != null)
        {
            targetWaypoint = navigator.lastWaypoint; //Teleport to the last waypoint
        }

        if (targetWaypoint == null)
        {
            Debug.LogWarning("No valid waypoint found for teleportation!");
            return;
        }

        //Teleport the pedestrian
        transform.position = targetWaypoint.GetPosition();
        navigator.currentWaypoint = targetWaypoint;

        //Update the destination
        SetDestination(targetWaypoint.GetPosition());

        Debug.Log($"Teleported pedestrian to waypoint: {targetWaypoint.name}");
    }

}
