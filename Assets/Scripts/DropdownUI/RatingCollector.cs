using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DropdownUI : MonoBehaviour
{
    public TMP_Dropdown D_Trees;
    public TMP_Dropdown D_Greenery;
    public TMP_Dropdown D_Building;
    public TMP_Dropdown D_DistanceWalk;
    public TMP_Dropdown D_DistanceInfr;
    public TMP_Dropdown D_Enclosure;
    public TMP_Dropdown D_Setback;
    public TMP_Dropdown D_Population;
    public TMP_Dropdown D_Connectivity;
    public TMP_Dropdown D_SpacesSocial;

    public Button Btn_Submit;

    void Start()
    {
        // Attach the Submit function to the button click
        if (Btn_Submit != null)
        {
            Btn_Submit.onClick.AddListener(SubmitRatings);
        }
    }

    void SubmitRatings()
    {
        // Collect values from each dropdown and add 1 since dropdown values start at 0
        int treesRating = D_Trees?.value + 1 ?? 0;
        int greeneryRating = D_Greenery?.value + 1 ?? 0;
        int buildingsRating = D_Building?.value + 1 ?? 0;
        int amenitiesRating = D_DistanceWalk?.value + 1 ?? 0;
        int transportRating = D_DistanceInfr?.value + 1 ?? 0;
        int enclosureRating = D_Enclosure?.value + 1 ?? 0;
        int setbackRating = D_Setback?.value + 1 ?? 0;
        int densityRating = D_Population?.value + 1 ?? 0;
        int connectivityRating = D_Connectivity?.value + 1 ?? 0;
        int socialSpacesRating = D_SpacesSocial?.value + 1 ?? 0;

        // Log each rating to the console
        Debug.Log("Trees Rating: " + treesRating);
        Debug.Log("Greenery Rating: " + greeneryRating);
        Debug.Log("Buildings Rating: " + buildingsRating);
        Debug.Log("Amenities Rating: " + amenitiesRating);
        Debug.Log("Transport Rating: " + transportRating);
        Debug.Log("Enclosure Rating: " + enclosureRating);
        Debug.Log("Setback Rating: " + setbackRating);
        Debug.Log("Density Rating: " + densityRating);
        Debug.Log("Connectivity Rating: " + connectivityRating);
        Debug.Log("Social Spaces Rating: " + socialSpacesRating);
    }
}

