using UnityEngine;

public class AddBuilding : MonoBehaviour
{
    [SerializeField]
    private GameObject building;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            Debug.Log("HELLOOOOOOOOO");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject targetHit = hit.transform.gameObject;
                Debug.Log(targetHit.transform.position);
                Vector3 posHit = hit.point;
                if (targetHit != null)
                {
                    Debug.Log("BYEEEEEE");
                    posHit = posHit + Vector3.up * building.transform.localScale.y / 2;
                    Instantiate(building, posHit, Quaternion.identity);
                }

            }
        }
    }
}
