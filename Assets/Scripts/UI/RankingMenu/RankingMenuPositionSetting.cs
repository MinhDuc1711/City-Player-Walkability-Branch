using UnityEngine;

public class RankingMenuPositionSetting : MonoBehaviour
{
    public RectTransform menu2;
    public RectTransform parent;


    // Update is called once per frame
    void Update()
    {
        menu2.anchoredPosition = parent.anchoredPosition;
    }
}
