using UnityEngine;
using UnityEngine.EventSystems;

public class AddBuilding : MonoBehaviour
{
    [SerializeField]
    private GameObject building;

    // Update is called once per frame
    void Update()
    {
      
    }

    public void CreateBuilding(GameObject targetHit, Vector3 posHit)
    {
        Debug.Log(targetHit.gameObject.layer);
        posHit = posHit + Vector3.up * building.transform.localScale.y / 2;
        Instantiate(building, posHit, Quaternion.identity);
    }
}
