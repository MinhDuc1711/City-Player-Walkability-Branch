using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class DistanceMenuManager : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public Transform[] objects; // Array to hold the Transforms of the objects
    public TextMeshProUGUI[] distanceTexts; // Array to hold the Text UI elements for each object
    public GameObject distancePanel; // Reference to the DistancePanel
    public GameObject transportPanel;
    public GameObject amenitiesPanel;
    public Button transportButton;
    public Button amenitiesButton;
    public Transform[] transportObjects; // Array for transport objects
    public Transform[] amenitiesObjects; // Array for amenities objects
    public TextMeshProUGUI[] transportDistanceTexts; // Texts for transport objects
    public TextMeshProUGUI[] amenitiesDistanceTexts; // Texts for amenities objects
    public GameObject occurrencesPanel; // Reference to the OccurrencesPanel
    public Button occurrencesButton; 

    public void ShowTransportPanel()
    {
        transportPanel.SetActive(true);
        amenitiesPanel.SetActive(false);
        occurrencesPanel.SetActive(false);

        transportButton.gameObject.SetActive(false);
        amenitiesButton.gameObject.SetActive(false);
        occurrencesButton.gameObject.SetActive(false);

        UpdateDistances(); // Update distances for transport objects
    }

    public void ShowAmenitiesPanel()
    {
        amenitiesPanel.SetActive(true);
        transportPanel.SetActive(false);
        occurrencesPanel.SetActive(false);

        transportButton.gameObject.SetActive(false);
        amenitiesButton.gameObject.SetActive(false);
        occurrencesButton.gameObject.SetActive(false);

        UpdateDistances(); // Update distances for amenities objects
    }

    public void ShowOccurrencesPanel()
    {
        occurrencesPanel.SetActive(true);
        transportPanel.SetActive(false);
        amenitiesPanel.SetActive(false);

        transportButton.gameObject.SetActive(false);
        amenitiesButton.gameObject.SetActive(false);
        occurrencesButton.gameObject.SetActive(false);
    }

    public void ToggleDistancePanel()
    {
        bool isActive = !distancePanel.activeSelf;
        distancePanel.SetActive(isActive);

        if (isActive)
        {
            // Show the Transport and Amenities buttons when the panel opens
            transportButton.gameObject.SetActive(true);
            amenitiesButton.gameObject.SetActive(true);
            occurrencesButton.gameObject.SetActive(true);

            // Hide the specific category panels
            transportPanel.SetActive(false);
            amenitiesPanel.SetActive(false);
            occurrencesPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Continuously update distances while the DistancePanel is active
    void Update()
    {
        if (distancePanel.activeSelf)
        {
            UpdateDistances();
        }
    }

    private void UpdateDistances()
    {
        if (transportPanel.activeSelf)
        {
            for (int i = 0; i < transportObjects.Length; i++)
            {
                float distance = Vector3.Distance(player.position, transportObjects[i].position);
                transportDistanceTexts[i].text = transportObjects[i].name + ": " + distance.ToString("F2") + " m";
            }
        }
        else if (amenitiesPanel.activeSelf)
        {
            for (int i = 0; i < amenitiesObjects.Length; i++)
            {
                float distance = Vector3.Distance(player.position, amenitiesObjects[i].position);
                amenitiesDistanceTexts[i].text = amenitiesObjects[i].name + ": " + distance.ToString("F2") + " m";
            }
        }
    }
}