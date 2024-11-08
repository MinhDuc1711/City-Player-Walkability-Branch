using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateProfile : MonoBehaviour, IPointerClickHandler
{
    private string username;
    private string password;

    public string Username { get { return username; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        setProfileInfo();
        Debug.Log("Next Menu");
    }

    private void Awake()
    {
    }

    private void setProfileInfo()
    {
        int lastChildInd = transform.parent.childCount - 1;
        Transform parent = transform.parent.GetChild(lastChildInd);
        string tempPassword = "";

        Debug.Log(parent.name);

        username = parent.GetChild(lastChildInd).GetChild(0).GetChild(0).GetComponent<Text>().ToString();
        lastChildInd++;
        tempPassword = parent.GetChild(lastChildInd).GetChild(0).GetChild(0).GetComponent<Text>().ToString();
        lastChildInd++;
        if (tempPassword.Equals(parent.GetChild(lastChildInd).GetChild(0).GetChild(0).GetComponent<Text>().ToString()))
        {
            password = tempPassword;
        }
        Debug.Log(username);
        Debug.Log(password);
    }
}