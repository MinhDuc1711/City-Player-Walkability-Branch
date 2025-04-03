using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AspectDropdown : MonoBehaviour
{

    public HappyPoolPoints poolPoints;
    private TMP_Dropdown dropdown;
    private int aspectValue;

    private List<int> aspectList = new List<int> { 0, 5, 10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 90, 100 };

    private static List<AspectDropdown> allDropdowns = new List<AspectDropdown>();

    private void Awake()
    {
        aspectValue = 0;
        poolPoints = this.GetComponentInParent<HappyPoolPoints>();
        dropdown = GetComponent<TMP_Dropdown>();

        if (poolPoints == null)
        {
            Debug.LogError("HappyPoolPoints component not found!");
        }

        allDropdowns.Add(this);
    }

    private void Start()
    {
        PopulateDropdown();
        dropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(dropdown); });
    }

    private void PopulateDropdown()
    {
        dropdown.options.Clear();
        foreach (int value in aspectList)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(value.ToString()));
        }
        UpdateDropdownOptions();
    }

    private void UpdateDropdownOptions()
    {
        int remainingPoints = poolPoints.Pool - GetTotalUsedPoints() + aspectValue;

        for (int i = 0; i < dropdown.options.Count; i++)
        {
            TMP_Dropdown.OptionData option = dropdown.options[i];
            int optionValue = aspectList[i];

            if (optionValue > remainingPoints)
            {
                option.text = $"<color=#808080>{optionValue}</color>";
            }
            else
            {
                option.text = optionValue.ToString();
            }
        }

        dropdown.captionText.text = dropdown.options[dropdown.value].text;
    }

    private void OnDropdownValueChanged(TMP_Dropdown changedDropdown)
    {
        int selectedValue = aspectList[changedDropdown.value];
        int difference = selectedValue - aspectValue;

        if (poolPoints.getPoints(difference))
        {
            aspectValue = selectedValue;
            UpdateAllDropdowns(); 
        }
        else
        {
            Debug.LogWarning("Not enough points in the pool!");
            dropdown.value = aspectList.IndexOf(aspectValue);
        }
    }
    private static int GetTotalUsedPoints()
    {
        int total = 0;
        foreach (AspectDropdown dropdown in allDropdowns)
        {
            total += dropdown.aspectValue;
        }
        return total;
    }
    private static void UpdateAllDropdowns()
    {
        foreach (AspectDropdown dropdown in allDropdowns)
        {
            dropdown.UpdateDropdownOptions();
        }
    }

    private void OnDestroy()
    {
        allDropdowns.Remove(this);
    }
}
