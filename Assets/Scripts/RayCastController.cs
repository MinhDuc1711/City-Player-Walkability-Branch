using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class RayCastController : MonoBehaviour
{
    [SerializeField]
    private ManageBuilding manageBuilding;
    [SerializeField]
    private AddBuilding addBuilding;

    private int buildingMask;
    private int platformMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildingMask = LayerMask.GetMask("Building");
        platformMask = LayerMask.GetMask("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Building"))
            {
                Debug.Log("AAAAAAAAA");
                manageBuilding.Highlight(hit.collider.gameObject);
            }
            else if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Platform"))
            {
                GameObject targetHit = hit.transform.gameObject;
                Debug.Log(targetHit.transform.position);
                Vector3 posHit = hit.point;
                if (targetHit != null)
                {
                    addBuilding.CreateBuilding(targetHit.gameObject, posHit);
                }

            }
        }
        else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject()) 
        {
            manageBuilding.Deselect();
        }
    }
}
