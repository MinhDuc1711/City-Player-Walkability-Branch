using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class DistanceUI : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public Transform[] objects; // Array to hold the Transforms of the objects (cubes)
    public TextMeshProUGUI[] distanceTexts; 

    void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            float distance = Vector3.Distance(player.position, objects[i].position);
            distanceTexts[i].text = "Distance to " + objects[i].name + ": " + distance.ToString("F2") + " meters";
        }
    }
}