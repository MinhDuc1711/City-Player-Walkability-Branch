using UnityEngine;

public class ConnectivitySlider : MonoBehaviour
{
    public GameObject ConnectivityStreet;

    public Transform[] IntersectionPoint;

    public Transform StreetHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  

        GameObject streetInstance = Instantiate(ConnectivityStreet, new Vector3(IntersectionPoint[0].position.x, StreetHeight.position.y, IntersectionPoint[0].position.z), Quaternion.Euler(0, 90, 0));

        // Find the IntersectionMarker inside the prefab

        Transform IntersectionPrefabMarker = FindChildByName(streetInstance.transform, "Intersection");


        if (IntersectionPrefabMarker != null)
        {
            // Calculate the Z offset
            float xOffset = IntersectionPrefabMarker.transform.position.x - IntersectionPoint[0].position.x;

            float zOffset = IntersectionPrefabMarker.transform.position.z - IntersectionPoint[0].position.z;

            // Adjust the prefab's position to account for the Z offset
            streetInstance.transform.position = IntersectionPoint[0].position - new Vector3(xOffset, -StreetHeight.position.y, zOffset);

            IntersectionPoint[0].gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("IntersectionMarker not found in the prefab!");
        }
        
    }

    Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;
            Transform result = FindChildByName(child, name);
            if (result != null)
                return result;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
