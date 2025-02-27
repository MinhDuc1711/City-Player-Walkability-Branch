using UnityEngine;
using System.Collections.Generic;

public class ConnectivitySlider : MonoBehaviour
{
    public GameObject ConnectivityStreet;

    public Transform[] IntersectionPoint;

    public Transform[] Intersection2Points;

    public Transform[] Intersection3Points;

    public Transform StreetHeight;

    public List<GameObject> IntersectionInstances = new List<GameObject>();

    public GreeneryGeneration GreeneryScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    public void GenerateIntersection(float IntersectionCount)
    {
        if (IntersectionCount == 0)
        {
            DestroyIntersectionInstances();
            SetActiveForAll(true);
            NotifyGreeneryChange();
        }
        else if (IntersectionCount == 1)
        {

            DestroyIntersectionInstances();
            SetActiveForAll(true);

            InstantiateAndAdjust(IntersectionPoint[0]);
            NotifyGreeneryChange();

        }
        else if (IntersectionCount == 2)
        {
            DestroyIntersectionInstances();
            SetActiveForAll(true);

            InstantiateAndAdjust(Intersection2Points[0]);
            InstantiateAndAdjust(Intersection2Points[1]);
            NotifyGreeneryChange();
        }
        else if (IntersectionCount == 3)
        {
            DestroyIntersectionInstances();
            SetActiveForAll(true);

            InstantiateAndAdjust(Intersection3Points[0]);
            InstantiateAndAdjust(Intersection3Points[1]);
            InstantiateAndAdjust(Intersection3Points[2]);
            NotifyGreeneryChange();
        }

    }


    void InstantiateAndAdjust(Transform inter)
    {
        if (inter != null)
        {


            GameObject streetInstance = Instantiate(ConnectivityStreet, new Vector3(inter.position.x, StreetHeight.position.y, inter.position.z), Quaternion.Euler(0, 90, 0));

            // Find the IntersectionMarker inside the prefab

            Transform IntersectionPrefabMarker = FindChildByName(streetInstance.transform, "Intersection");


            if (IntersectionPrefabMarker != null)
            {
                // Calculate the Z offset
                float xOffset = IntersectionPrefabMarker.transform.position.x - inter.position.x;

                float zOffset = IntersectionPrefabMarker.transform.position.z - inter.position.z;

                // Adjust the prefab's position to account for the Z offset
                streetInstance.transform.position = inter.position - new Vector3(xOffset, -StreetHeight.position.y, zOffset);

                inter.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("IntersectionMarker not found in the prefab!");
            }
            IntersectionInstances.Add(streetInstance);
        }
        else
        {
            Debug.LogWarning("No intersection transform point");
        }
    }

    void DestroyIntersectionInstances()
    {
        foreach (GameObject instance in IntersectionInstances)
        {
            Destroy(instance);
        }
        IntersectionInstances.Clear();

    }

    private void NotifyGreeneryChange()
    {
        if (GreeneryScript != null)
        {
            GreeneryScript.GenerateGreenery(GreeneryScript.greenObjSlider.value);
        }
    }

    void SetActiveForAll(bool isActive)
    {
        // Loop through each array and set active state
        foreach (Transform point in IntersectionPoint)
        {
            if (point != null)
                point.gameObject.SetActive(isActive);
        }

        foreach (Transform point in Intersection2Points)
        {
            if (point != null)
                point.gameObject.SetActive(isActive);
        }

        foreach (Transform point in Intersection3Points)
        {
            if (point != null)
                point.gameObject.SetActive(isActive);
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
