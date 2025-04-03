using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VisualImportanceAspect : MonoBehaviour
{
    public Image bg;
    public TMP_Text txt;
    private TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (dropdown.value)
        {
            case 0: //0
            case 1: //5
                bg.color = Color.red;
                txt.color = Color.white;
                break;
            case 2: //10
            case 3: //15
                bg.color = new Color(1f, 0.647f, 0f, 1f);
                txt.color = Color.black;
                break;
            case 4: //20
            case 5: //25
                bg.color = Color.yellow;
                txt.color = Color.black;
                break;
            case 6: //30
            case 7: //40
            case 8: //50
                bg.color = new Color(0.565f, 0.933f, 0.565f, 1f);
                txt.color = Color.black;
                break;
            case 9: //60
            case 10: //70
            case 11: //80
            case 12: //90
            case 13: //100
                bg.color = Color.green;
                txt.color = Color.black;
                break;

        }
    }
}
