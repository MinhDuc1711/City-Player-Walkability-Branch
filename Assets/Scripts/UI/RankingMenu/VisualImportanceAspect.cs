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
            case 0:
                bg.color = Color.red;
                txt.color = Color.black;
                break;
            case 1:
                bg.color = Color.red;
                txt.color = Color.black;
                break;
            case 2:
                bg.color = new Color(1f, 0.647f, 0f, 1f);
                txt.color = Color.black;
                break;
            case 3:
                bg.color = new Color(1f, 0.647f, 0f, 1f);
                txt.color = Color.black;
                break;
            case 4:
                bg.color = Color.yellow;
                txt.color = Color.black;
                break;
            case 5:
                bg.color = Color.yellow;
                txt.color = Color.black;
                break;
            case 6:
                bg.color = Color.yellow;
                txt.color = Color.black;
                break;
            case 7:
                bg.color = new Color(0.565f, 0.933f, 0.565f, 1f);
                txt.color = Color.black;
                break;
            case 8:
                bg.color = new Color(0.565f, 0.933f, 0.565f, 1f);
                txt.color = Color.black;
                break;
            case 9:
                bg.color = Color.green;
                txt.color = Color.black;
                break;
            case 10:
                bg.color = Color.green;
                txt.color = Color.black;
                break;

        }
    }
}
