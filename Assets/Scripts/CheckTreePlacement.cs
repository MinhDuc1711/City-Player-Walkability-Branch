using UnityEngine;

public class CheckTreePlacement : MonoBehaviour
{
    private TreeManager treeManager;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        treeManager = GameObject.Find("TreeManager").GetComponent<TreeManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            treeManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            treeManager.canPlace = true;
        }


    }

    

}
