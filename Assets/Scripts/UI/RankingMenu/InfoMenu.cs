using UnityEngine;

public class InfoMenu : MonoBehaviour
{
    public GameObject targetGameObject;
    public GameObject targetBackground;
    private bool isInfoButtonClicked = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isInfoButtonClicked)
        {
            targetGameObject.SetActive(false);
            targetBackground.SetActive(false);
        }

        isInfoButtonClicked = false;
    }

    public void OnInfoButtonClick()
    {
        targetBackground.SetActive(true);
        targetGameObject.SetActive(true);
        isInfoButtonClicked = true;
    }
}
