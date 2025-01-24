using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OtherOption : MonoBehaviour
{
    public RectTransform option;
    public TMP_Dropdown optns;

    public void OnValueChange()
    {
        if (optns.options[optns.value].text == "Other")
        {
            option.gameObject.SetActive(true);
        }
        else
        {
            option.gameObject.SetActive(false);
        }
    }
}
