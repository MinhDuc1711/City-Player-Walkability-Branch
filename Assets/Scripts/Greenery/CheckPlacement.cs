using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    private GreenObjectManager greenObjectManager;
    
    public float minimumDistance = 10f; // Minimum distance between objects

    void Start()
    {
        greenObjectManager = GameObject.Find("GreenObjectManager").GetComponent<GreenObjectManager>();
    }

    // Use OnTriggerEnter or OnCollisionEnter for collision detection
    private void OnTriggerEnter(Collider other)
    {
        // Check if object collides with another Greenery or Bench object
        if (other.CompareTag("Greenery") || other.CompareTag("Bench"))
        {
            // If colliding with any object, prevent placement
            Debug.Log("Object is too close to another one.");
            greenObjectManager.canPlace = false;
        }
    }

    // Use OnTriggerStay to check the distance between objects dynamically while placing
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Greenery") || other.CompareTag("Bench"))
        {
            // Calculate distance between this object and the other object
            float distance = Vector3.Distance(other.transform.position, transform.position);

            // If the distance is less than the minimum required, disable placement
            if (distance < minimumDistance)
            {
                Debug.Log("Too close to another object. Cannot place.");
                greenObjectManager.canPlace = false;
            }
            else
            {
                greenObjectManager.canPlace = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Greenery") || other.CompareTag("Bench"))
        {
            greenObjectManager.canPlace = true;
        }
    }
}
