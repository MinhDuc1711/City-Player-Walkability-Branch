using UnityEngine;

public class CarNavigationController : MonoBehaviour
{
    public Waypoint currentWaypoint;
    public Waypoint lastWaypoint;

    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 2f; //Smooth turning
    public float stopDistance = 1f;

    [Header("Boundary Settings")]
    public float maxOffset = 10f; //Max allowed X/Y deviation

    private Vector3 initialPosition; //Store the starting position
    private CarSpawner spawner;

    public void Initialize(Waypoint start, Waypoint end, CarSpawner carSpawner)
    {
        currentWaypoint = start;
        lastWaypoint = end;
        spawner = carSpawner;
        initialPosition = transform.position;

        //Place car at the starting position
        transform.position = currentWaypoint.GetPosition();
        transform.rotation = Quaternion.LookRotation(currentWaypoint.transform.forward);
    }

    private void Update()
    {
        //Boundary check: destroy car if deviates too far
        if (Mathf.Abs(transform.position.x - initialPosition.x) > maxOffset ||
            Mathf.Abs(transform.position.y - initialPosition.y) > maxOffset)
        {
            //Debug.Log($"{gameObject.name} exceeded boundary limits. Destroying...");
            DestroyCar();
            return;
        }

        //Move towards the current waypoint
        if (currentWaypoint != null)
        {
            Vector3 direction = currentWaypoint.GetPosition() - transform.position;
            direction.y = 0; //Keep the car level
            float distance = direction.magnitude;

            if (distance > stopDistance)
            {
                //Smooth rotation and forward movement
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                //Reached current waypoint
                if (currentWaypoint == lastWaypoint)
                {
                    //Debug.Log($"{gameObject.name} reached the last waypoint. Destroying...");
                    DestroyCar();
                }
                else
                {
                    currentWaypoint = currentWaypoint.nextWaypoint;
                }
            }
        }
    }

    private void DestroyCar()
    {
        if (spawner != null)
        {
            spawner.CarDestroyed();
        }
        Destroy(gameObject);
    }
}
