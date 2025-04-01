using UnityEngine;
using UnityEngine.UI;

public class DisplaceDecoration : MonoBehaviour
{
    public GameObject parentObject;
    public float moveStep = 1.0f;
    public float maxMoveDistance = 50.0f;
    public ConnectivitySlider ConnectScript;
    public Slider ConnectSlider;
    
    void Start()
    {
        ConnectSlider.onValueChanged.AddListener(OnSliderValueChange);
        if (parentObject == null)
        {
            Debug.LogError("Parent object is not assigned!");
        }
    }

    public void OnSliderValueChange(float x)
    {
        if (x != 0) MoveDecoration();
    }

    public void MoveDecoration()
    {
        // Debug.Log("Attempting to move" + parentObject.transform.childCount +  "children");
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            GameObject childObject = parentObject.transform.GetChild(i).gameObject;
            childObject.tag = "Decoration";
            Vector3 newPosition = childObject.transform.position;
            float movedDistance = 0f;

            while (IsOccupied(newPosition) && movedDistance < maxMoveDistance)
            {
                newPosition.z += moveStep;
                movedDistance += moveStep;
            }

            if (movedDistance < maxMoveDistance)
            {
                childObject.transform.position = newPosition;
                // Debug.Log("Moved " + childObject.name + " to " + newPosition);
            }
        }
    }

    bool IsOccupied(Vector3 position)
    {
        float checkRadius = 1.0f;
        int layerMask = LayerMask.GetMask("Decoration");
        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius, layerMask);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Decoration")) return true;
        }
        if (ConnectScript != null)
        {
            foreach (var instance in ConnectScript.IntersectionInstances)
            {
                float intersectionZ = instance.transform.position.z - 12;
                if (Mathf.Abs(position.z - intersectionZ) <= 10)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
