using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class ConnectivitySlider : MonoBehaviour
{
    public GameObject ConnectivityStreet;

    public Transform[] IntersectionPoint;

    public Transform[] Intersection2Points;

    public Transform[] Intersection3Points;

    public Transform StreetHeight;

    public List<GameObject> IntersectionInstances = new List<GameObject>();

    public GreeneryGeneration GreeneryScript;

    public PublicSpaceGeneration PublicSpaceScript;

    public StreetEnclosure EnclosureScript;

    public List<GameObject> LeftPlots = new List<GameObject>();
    public List<GameObject> RightPlots = new List<GameObject>();

    private Dictionary<GameObject, Vector3> originalLeftPlotPositions = new Dictionary<GameObject, Vector3>();
    private Dictionary<GameObject, Vector3> originalRightPlotPositions = new Dictionary<GameObject, Vector3>();

    // Background building that are on the intersection 1
    public List<GameObject> bbIntersection1;
    // Background building that are on the intersection 2
    public List<GameObject> bbIntersection2;
    // Background building that are on the intersection 3
    public List<GameObject> bbIntersection3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject plot in LeftPlots)
        {
            originalLeftPlotPositions[plot] = plot.transform.position; // Save initial positions
        }
        foreach (GameObject plot in RightPlots)
        {
            originalRightPlotPositions[plot] = plot.transform.position; // Save initial positions
        }

    }


    public void GenerateIntersection(float IntersectionCount)
    {
        DestroyIntersectionInstances();
        SetActiveForAll(true);

        if (IntersectionCount == 0)
        {
            ResetPlots();
        }
        else if (IntersectionCount == 1)
        {
            InstantiateAndAdjust(IntersectionPoint[0]);
        }
        else if (IntersectionCount == 2)
        {
            InstantiateAndAdjust(Intersection2Points[0]);
            InstantiateAndAdjust(Intersection2Points[1]);
        }
        else if (IntersectionCount == 3)
        {
            InstantiateAndAdjust(Intersection3Points[0]);
            InstantiateAndAdjust(Intersection3Points[1]);
            InstantiateAndAdjust(Intersection3Points[2]);
        }

        NotifyGreeneryChange();
        NotifyPublicSpaceChange();
        ResetPlots();
        NotifyBackgroundBuildings((int)IntersectionCount);

        AdjustPlots(LeftPlots);
        AdjustPlots(RightPlots);

        NotifyEnclosureChange();

    }

    void AdjustPlots(List<GameObject> Plots)
    {

        foreach (GameObject plot in Plots)
        {
            bool needsAdjustment = true;
            int maxIterations = 100; // Prevent infinite loops
            int iteration = 0;

            while (needsAdjustment && iteration < maxIterations)
            {
                needsAdjustment = false;
                iteration++;

                // Get the correct world Z position
                //WORLD AND LOCAL HAVE A 15 UNIT OFFSET !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                float plotWorldZ = plot.transform.position.z + 15;
                float plotSize = 25f; // Replace with actual plot size if dynamic

                foreach (GameObject street in IntersectionInstances)
                {
                    float streetWorldZ = street.transform.position.z;
                    float streetSize = 10f; // Replace with actual street size if dynamic

                    if (Mathf.Abs(plotWorldZ - streetWorldZ) <= (plotSize / 2 + streetSize / 2))
                    {
                        needsAdjustment = true;

                        // Move the plot forward or backward in world space
                        if (plotWorldZ >= streetWorldZ)
                            plot.transform.position += Vector3.forward * plotSize; // Move forward
                        else
                            plot.transform.position += Vector3.back * plotSize; // Move backward

                        break; // Recheck from the start after moving
                    }
                }
                
                foreach (GameObject otherPlot in Plots)
                {
                    if (otherPlot != plot) // Don't compare the plot to itself
                    {
                        float otherPlotWorldZ = otherPlot.transform.position.z + 15;

                        // Check if the plots are overlapping (within half of the plot size)
                        if (Mathf.Abs(plotWorldZ - otherPlotWorldZ) <= plotSize-6)
                        {
                            needsAdjustment = true;

                            // Find the furthest plot (either leftmost or rightmost) from the colliding plot
                            float furthestPlotZ = otherPlotWorldZ;

                            // Find the furthest plot
                            GameObject furthestPlot = null;
                            float maxDistance = 0;

                            foreach (GameObject other in Plots)
                            {
                                if (other != plot)
                                {
                                    float otherPlotZ = other.transform.position.z;
                                    float distance = Mathf.Abs(plotWorldZ - otherPlotZ);

                                    if (distance >= maxDistance)
                                    {
                                        furthestPlot = other;
                                        maxDistance = distance;
                                    }
                                }
                            }

                            // Once the furthest plot is found, move the colliding plot 24 units away from it
                            if (furthestPlot != null)
                            {
                                float furthestPlotWorldZ = furthestPlot.transform.position.z;

                                plot.transform.position = new Vector3(plot.transform.position.x, plot.transform.position.y, furthestPlotWorldZ);

                                if (plotWorldZ < furthestPlotWorldZ)
                                {
                                    plot.transform.position += Vector3.forward * plotSize; // Move plot forward
                                }
                                else
                                {
                                    plot.transform.position += Vector3.back * plotSize; // Move plot backward
                                }
                            }
                            else
                            {
                                plot.transform.position += Vector3.forward * plotSize; // Move plot forward
                            }

                            break; // Recheck after moving
                        }
                    }
                }
            }

        }
        
    }

    void DebugPositions(List<GameObject> Plots)
    {
        foreach (GameObject plot in Plots)
        {
            Debug.Log($"Plot: {plot.name}, World Z: {plot.transform.position.z}, Local Z: {plot.transform.localPosition.z}");
        }

        foreach (GameObject street in IntersectionInstances)
        {
            Debug.Log($"Street: {street.name}, World Z: {street.transform.position.z}, Local Z: {street.transform.localPosition.z}");
        }
    }

    void ResetPlots()
    {
        foreach (GameObject plot in LeftPlots)
        {
            if (originalLeftPlotPositions.ContainsKey(plot))
            {
                plot.transform.position = originalLeftPlotPositions[plot]; // Restore original position
            }
        }
        foreach (GameObject plot in RightPlots)
        {
            if (originalRightPlotPositions.ContainsKey(plot))
            {
                plot.transform.position = originalRightPlotPositions[plot]; // Restore original position
            }
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

    private void NotifyPublicSpaceChange()
    {
        if (PublicSpaceScript != null)
        {
            PublicSpaceScript.OnPublicSpaceSliderValueChanged(PublicSpaceScript.publicSpaceSlider.value);
        }
    }

    private void NotifyEnclosureChange()
    {
        if(EnclosureScript!=null)
        {
            EnclosureScript.UpdateOriginalBuildingPos();
            EnclosureScript.UpdateBuildingPos(EnclosureScript.SliderOffset);
        }
    }

    public void NotifyBackgroundBuildings(int intersCount)
    {
        if (intersCount == 0)
        {
            foreach (GameObject bgbuilding in bbIntersection1)
            {
                bgbuilding.SetActive(true);
            }

            foreach (GameObject bgbuilding in bbIntersection2)
            {
                bgbuilding.SetActive(true);
            }

            foreach (GameObject bgbuilding in bbIntersection3)
            {
                bgbuilding.SetActive(true);
            }
        }
        else if (intersCount == 1)
        {
            foreach (GameObject bgbuilding in bbIntersection1)
            {
                bgbuilding.SetActive(false);
            }

            foreach (GameObject bgbuilding in bbIntersection2)
            {
                bgbuilding.SetActive(true);
            }

            foreach (GameObject bgbuilding in bbIntersection3)
            {
                bgbuilding.SetActive(true);
            }
        }
        else if (intersCount == 2)
        {
            foreach (GameObject bgbuilding in bbIntersection1)
            {
                bgbuilding.SetActive(false);
            }

            foreach (GameObject bgbuilding in bbIntersection2)
            {
                bgbuilding.SetActive(false);
            }

            foreach (GameObject bgbuilding in bbIntersection3)
            {
                bgbuilding.SetActive(true);
            }
        }
        else if (intersCount == 3)
        {
            foreach (GameObject bgbuilding in bbIntersection1)
            {
                bgbuilding.SetActive(false);
            }

            foreach (GameObject bgbuilding in bbIntersection2)
            {
                bgbuilding.SetActive(false);
            }

            foreach (GameObject bgbuilding in bbIntersection3)
            {
                bgbuilding.SetActive(false);
            }
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

}

