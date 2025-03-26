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
        Debug.Log("Slider changed to: " + x);
        if (x != 0) MoveChildrenToEmptySpace();
    }

    public void MoveChildrenToEmptySpace()
    {
        Debug.Log("Attempting to move" + parentObject.transform.childCount +  "children");
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
                Debug.Log($"Moved {childObject.name} to {newPosition}");
            }
            else
            {
                Debug.LogWarning($"Could not find a free position for {childObject.name}");
            }
        }
    }

    bool IsOccupied(Vector3 position)
    {
        // WOrk will be done later to optimize this
        float checkRadius = 0.5f;
        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius);
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
