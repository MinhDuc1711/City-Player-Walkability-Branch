using UnityEngine;

public class AddOrRemove : MonoBehaviour
{
    [SerializeField]
    private bool delete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        delete = false;
    }

    // Update is called once per frame
    void Update()
    {
        Delete();
    }

    void Delete()
    {
        if (delete) 
        { 
            Destroy(gameObject);
        }
    }
}
