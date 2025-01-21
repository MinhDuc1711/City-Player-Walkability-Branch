using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConfirmRank : MonoBehaviour, IPointerClickHandler
{
    private List<string> ranking;

    public List<string> Ranking {  get { return ranking; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        setRanking();
        Debug.Log("Next Menu");
    }

    private void Awake()
    {
    }

    private void setRanking()
    {
        int lastChildInd = transform.parent.childCount - 1;
        Transform parent = transform.parent.GetChild(lastChildInd);

        Debug.Log(parent.name);

        foreach (Transform child in parent)
        {
            Text aspect = child.GetChild(0).GetChild(0).GetComponent<Text>();
            ranking.Add(aspect.text.ToString());
        }
    }
}
