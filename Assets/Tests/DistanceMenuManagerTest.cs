using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshProUGUI

public class DistanceMenuManagerTests
{
    private GameObject distanceManagerObj;
    private DistanceMenuManager distanceMenuManager;

    private GameObject transportPanel;
    private GameObject amenitiesPanel;
    private GameObject occurrencesPanel;

    private Button transportButton;
    private Button amenitiesButton;
    private Button occurrencesButton;

    private TextMeshProUGUI transportText;
    private TextMeshProUGUI amenitiesText;

[SetUp]
public void SetUp()
{
    // Create GameObject for DistanceManager
    distanceManagerObj = new GameObject("DistanceManager");
    distanceMenuManager = distanceManagerObj.AddComponent<DistanceMenuManager>();

    // Create Canvas and DistancePanel
    GameObject canvas = new GameObject("Canvas");
    canvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
    GameObject distancePanel = new GameObject("DistancePanel");
    distancePanel.transform.SetParent(canvas.transform);  // Nest under Canvas
    distancePanel.AddComponent<RectTransform>(); // For proper UI hierarchy
    distanceMenuManager.distancePanel = distancePanel;

    // Create and configure TransportPanel
    transportPanel = new GameObject("TransportPanel");
    transportPanel.transform.SetParent(distancePanel.transform); // Nest under DistancePanel
    transportPanel.AddComponent<RectTransform>();
    distanceMenuManager.transportPanel = transportPanel;
    CreateTextMeshPro("MetroDistanceText", transportPanel);
    CreateTextMeshPro("BusDistanceText", transportPanel);
    CreateTextMeshPro("TrainDistanceText", transportPanel);

    // Create and configure AmenitiesPanel
    amenitiesPanel = new GameObject("AmenitiesPanel");
    amenitiesPanel.transform.SetParent(distancePanel.transform); // Nest under DistancePanel
    amenitiesPanel.AddComponent<RectTransform>();
    distanceMenuManager.amenitiesPanel = amenitiesPanel;
    CreateTextMeshPro("ParkDistanceText", amenitiesPanel);
    CreateTextMeshPro("GymDistanceText", amenitiesPanel);

    // Create and configure OccurrencesPanel
    occurrencesPanel = new GameObject("OccurrencesPanel");
    occurrencesPanel.transform.SetParent(distancePanel.transform); // Nest under DistancePanel
    occurrencesPanel.AddComponent<RectTransform>();
    distanceMenuManager.occurrencesPanel = occurrencesPanel;

    // Create and assign buttons
    transportButton = CreateButton("TransportButton", distancePanel);
    amenitiesButton = CreateButton("AmenitiesButton", distancePanel);
    occurrencesButton = CreateButton("OccurrencesButton", distancePanel);

    distanceMenuManager.transportButton = transportButton;
    distanceMenuManager.amenitiesButton = amenitiesButton;
    distanceMenuManager.occurrencesButton = occurrencesButton;

    // Initially deactivate all panels
    transportPanel.SetActive(false);
    amenitiesPanel.SetActive(false);
    occurrencesPanel.SetActive(false);
    distancePanel.SetActive(false); // Ensure DistancePanel is inactive by default
}

private Button CreateButton(string name, GameObject parent)
{
    GameObject buttonObj = new GameObject(name);
    buttonObj.transform.SetParent(parent.transform);
    buttonObj.AddComponent<RectTransform>();
    buttonObj.AddComponent<CanvasRenderer>();
    Button button = buttonObj.AddComponent<Button>();
    buttonObj.AddComponent<Image>(); // Image component required for Button
    return button;
}


    private TextMeshProUGUI CreateTextMeshPro(string name, GameObject parent)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent.transform);
        textObj.AddComponent<RectTransform>();

        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = "Sample Text"; // Default text for testing
        return tmp;
    }

    [Test]
    public void TestShowTransportPanel()
    {
        distanceMenuManager.ShowTransportPanel();

        Assert.IsTrue(transportPanel.activeSelf, "Transport Panel should be active");
        Assert.IsFalse(amenitiesPanel.activeSelf, "Amenities Panel should be inactive");
        Assert.IsFalse(occurrencesPanel.activeSelf, "Occurrences Panel should be inactive");
    }

    [Test]
    public void TestShowAmenitiesPanel()
    {
        distanceMenuManager.ShowAmenitiesPanel();

        Assert.IsTrue(amenitiesPanel.activeSelf, "Amenities Panel should be active");
        Assert.IsFalse(transportPanel.activeSelf, "Transport Panel should be inactive");
        Assert.IsFalse(occurrencesPanel.activeSelf, "Occurrences Panel should be inactive");
    }

    [Test]
    public void TestShowOccurrencesPanel()
    {
        distanceMenuManager.ShowOccurrencesPanel();

        Assert.IsTrue(occurrencesPanel.activeSelf, "Occurrences Panel should be active");
        Assert.IsFalse(transportPanel.activeSelf, "Transport Panel should be inactive");
        Assert.IsFalse(amenitiesPanel.activeSelf, "Amenities Panel should be inactive");
    }

    [Test]
    public void TestToggleDistancePanel()
    {
        distanceMenuManager.ToggleDistancePanel();

        Assert.IsTrue(distanceMenuManager.distancePanel.activeSelf, "Distance Panel should be active");

        distanceMenuManager.ToggleDistancePanel();

        Assert.IsFalse(distanceMenuManager.distancePanel.activeSelf, "Distance Panel should be inactive");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(distanceManagerObj);
        Object.DestroyImmediate(transportPanel);
        Object.DestroyImmediate(amenitiesPanel);
        Object.DestroyImmediate(occurrencesPanel);
    }
}