using UnityEngine;
[ExecuteAlways]
public class GreeneryPlotRight : MonoBehaviour
{
    public GameObject plot;  // Reference to the plot object
    public GameObject greenery;  // Reference to the greenery object

    private Vector3 lastGreeneryScale;  // To track last scale of greenery

    void Start()
    {
        // Initialize the scale tracking
        lastGreeneryScale = greenery.transform.localScale;
    }

    void Update()
    {
        // Check if the scale of the greenery has changed
        if (greenery.transform.localScale != lastGreeneryScale)
        {
            // Calculate the change in scale
            Vector3 scaleChange = greenery.transform.localScale - lastGreeneryScale;

            // Get the current positions of the objects
            Vector3 plotPosition = plot.transform.position;
            Vector3 greeneryPosition = greenery.transform.position;

            // Adjust the position of the plot based on the scaling of the greenery.
            // Let's assume they are glued along their X-axis (side-by-side).
            // Adjusting plot's position on the X-axis:
            plotPosition.x = greeneryPosition.x + greenery.transform.localScale.x / 2 + plot.transform.localScale.x / 2;

            // Apply the new position to the plot
            plot.transform.position = plotPosition;

            // Update the last known scale
            lastGreeneryScale = greenery.transform.localScale;
        }
    }
}
