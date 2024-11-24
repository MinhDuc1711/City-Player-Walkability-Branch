using UnityEngine;
using UnityEngine.UI;

public class BuildingHeightAdjuster : MonoBehaviour
{
    public Slider heightSlider;       // Reference to the slider UI
    public Button saveButton;         // Reference to the Save button
    public Button cancelButton;       // Reference to the Cancel button
    private float originalHeight;     // Stores the building's original height
    private float lastSavedHeight;    // Stores the last saved height
    private bool isAdjusting = false; // Flag for tracking adjustment state
    private Vector3 originalPosition; // Stores the original position of the building
    private const float stepSize = 2f; // Step size for discrete adjustment

    private void Start()
    {
        // Initially hide the slider UI
        heightSlider.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        // Position the UI elements
        SetUIPosition();

        // Store original height and position
        originalHeight = transform.localScale.y;
        lastSavedHeight = originalHeight;
        originalPosition = transform.position;

        // Set slider range and value
        heightSlider.minValue = originalHeight * 0.5f;  // Minimum height (adjust as needed)
        heightSlider.maxValue = originalHeight * 2.0f;  // Maximum height (adjust as needed)
        heightSlider.value = originalHeight;            // Set initial value

        // Add listeners to the buttons
        saveButton.onClick.AddListener(SaveHeight);
        cancelButton.onClick.AddListener(CancelHeight);
    }

    private void OnMouseDown()
    {
        // Enable UI elements when the building is clicked
        StartAdjusting();
    }

    private void StartAdjusting()
    {
        isAdjusting = true;

        // Show slider and buttons
        heightSlider.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);

        // Set slider to current height value
        heightSlider.value = transform.localScale.y;

        // Add a listener to update building height in real-time
        heightSlider.onValueChanged.AddListener(AdjustHeight);
    }

    public void AdjustHeight(float newHeight)
    {
        if (isAdjusting)
        {
            // Round the slider value to the nearest multiple of stepSize
            float discreteHeight = Mathf.Round(newHeight / stepSize) * stepSize;

            // Calculate the change in height
            float heightDifference = discreteHeight - transform.localScale.y;

            // Adjust the building's height (only scale Y) and move it up or down to keep the bottom fixed
            Vector3 newScale = transform.localScale;
            newScale.y = discreteHeight;
            transform.localScale = newScale;

            // Move the building upwards or downwards to adjust only from the top
            Vector3 newPosition = transform.position;
            newPosition.y += heightDifference / 2;  // Shift position by half the difference
            transform.position = newPosition;
        }
    }

    public void SaveHeight()
    {
        // Save the current height as the last saved height
        lastSavedHeight = transform.localScale.y;

        // Stop adjusting and keep the new height
        isAdjusting = false;
        CloseUI();
    }

    public void CancelHeight()
    {
        // Revert to the last saved height and position
        Vector3 originalScale = transform.localScale;
        originalScale.y = lastSavedHeight;
        transform.localScale = originalScale;

        transform.position = originalPosition + new Vector3(0, (lastSavedHeight - originalHeight) / 2, 0);

        isAdjusting = false;
        CloseUI();
    }

    private void CloseUI()
    {
        // Hide the slider and buttons
        heightSlider.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        // Remove listener to avoid memory leaks
        heightSlider.onValueChanged.RemoveListener(AdjustHeight);
    }

    private void SetUIPosition()
    {
        // Position the UI elements once at the start

        RectTransform sliderRect = heightSlider.GetComponent<RectTransform>();
        sliderRect.anchoredPosition = new Vector2(0, 40);  // Center the slider on the screen

        RectTransform saveButtonRect = saveButton.GetComponent<RectTransform>();
        saveButtonRect.anchoredPosition = new Vector2(-100, -80);  // Adjust as needed

        RectTransform cancelButtonRect = cancelButton.GetComponent<RectTransform>();
        cancelButtonRect.anchoredPosition = new Vector2(100, -80);  // Adjust as needed
    }
}
