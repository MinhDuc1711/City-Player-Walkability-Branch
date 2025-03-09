using UnityEngine;
using UnityEngine.UI;

public class GreeneryGeneration : MonoBehaviour
{
    public GameObject LeftGreenStripStart;
    public GameObject LeftGreenStripEnd;
    public GameObject RightGreenStripStart;
    public GameObject RightGreenStripEnd;

    public GameObject[] TreePrefabs;
    public GameObject FlowerPrefab;
    //public GameObject PotPrefab;

    public Slider greenObjSlider;

    public float ExtraSpacing=3;

    //private GameObject[] greeneryPrefabs; 

    public void Start()
    {
        //greeneryPrefabs = new GameObject[] { TreePrefab, FlowerPrefab};
        greenObjSlider.onValueChanged.AddListener(OnGreenObjectSliderValueChanged);
        // Initial generation 
        GenerateGreenery(greenObjSlider.value);
    }

    void OnGreenObjectSliderValueChanged(float value)
    {
        Debug.Log("Green object slider value changed to: " + value);
        GenerateGreenery(value);
    }

    public void GenerateGreenery(float density)
    {
        if (density != 0)
        {
            density = 15 - density; 
        }
        ClearGreenery();

        // Rnadom Pattern Generation
        SpawnGreeneryWithSpacing(LeftGreenStripStart.transform.position, LeftGreenStripEnd.transform.position, density);
        SpawnGreeneryWithSpacing(RightGreenStripStart.transform.position, RightGreenStripEnd.transform.position, density);
    }

    void SpawnGreeneryWithSpacing(Vector3 start, Vector3 end, float spacing)
    {
        float distance = Vector3.Distance(start, end);
        int numberOfObjects = Mathf.FloorToInt(distance / spacing);
        int TreeVariance = 0;


        for (int i = 0; i <= numberOfObjects; i++)
        {
            float t = (float)i / numberOfObjects; 
            Vector3 position = Vector3.Lerp(start, end, t);
            if (!IsOccupied(position))
            {
                // GameObject prefabToSpawn;
                if (i % 2 ==0)
                {
                    float randomRotationY = 90 * Random.Range(0, 4); // 0, 90, 180, or 270
                    Quaternion rotation = Quaternion.Euler(0, randomRotationY, 0);
                    Instantiate(TreePrefabs[TreeVariance % 2], position, rotation);
                    TreeVariance++;
                }
                else
                {
                    position.z += (float)0.75;
                    Instantiate(FlowerPrefab, position, Quaternion.identity);
                }
            }

        }
    }

    private bool IsOccupied(Vector3 position)
    {
        float checkRadius = 3.0f; // Increase radius to avoid tree overlap
        Collider[] hitColliders = Physics.OverlapSphere(position, checkRadius);
        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Tree") || collider.CompareTag("Flower") || collider.CompareTag("Bench")) return true;
        }
        return false;
    }

    public void ClearGreenery()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag("Tree"))
        {
            Destroy(obj);
        }

        foreach (var obj in GameObject.FindGameObjectsWithTag("Flower"))
        {
            Destroy(obj);
        }

    }
}
