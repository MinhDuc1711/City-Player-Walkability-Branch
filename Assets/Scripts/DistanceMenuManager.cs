using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class DistanceMenuManager : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public Transform[] objects; // Array to hold the Transforms of the objects (cubes)
    public TextMeshProUGUI[] distanceTexts; // Array to hold the Text UI elements for each object
    public GameObject distancePanel; // Reference to the DistancePanel

    // Toggle the visibility of the distance panel
public void ToggleDistancePanel()
{
    bool isActive = !distancePanel.activeSelf;
    distancePanel.SetActive(isActive);

    if (isActive)
    {
        // If the panel is now active, unlock and show the cursor
        UpdateDistances();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    else
    {
        // If the panel is now inactive, unlock and show the cursor (don’t lock it)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

    // Update the distance text for each object
    private void UpdateDistances()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            float distance = Vector3.Distance(player.position, objects[i].position);
            distanceTexts[i].text = "Distance to " + objects[i].name + ": " + distance.ToString("F2") + " meters";
        }
    }
}