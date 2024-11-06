using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SliderUIControl : MonoBehaviour
{
    [SerializeField] private Slider timeSlider; // Reference to the UI slider
    [SerializeField] private LightingManager lightingManager; // Reference to the LightingManager

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set the initial value of the slider to match the TimeOfDay
        timeSlider.value = lightingManager.TimeOfDay;

        // Add a listener to update the LightingManager when the slider value changes
        timeSlider.onValueChanged.AddListener(UpdateTimeOfDay);

    }

    private void UpdateTimeOfDay(float value)
    {
        // Update the TimeOfDay in the LightingManager
        lightingManager.TimeOfDay = value;
        lightingManager.isSliderActive = true;
    }

    public void OnEndEdit()
    {
        lightingManager.isSliderActive = false; // Notify that the slider has been released
    }
}
