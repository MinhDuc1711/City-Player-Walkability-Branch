using UnityEngine;
using UnityEngine.UI;

public class GreeneryGeneration : MonoBehaviour
{
    public GameObject LeftGreenStripStart;
    public GameObject LeftGreenStripEnd;
    public GameObject RightGreenStripStart;
    public GameObject RightGreenStripEnd;

    public GameObject TreePrefab;
    public GameObject FlowerPrefab;
    //public GameObject PotPrefab;

    public Slider greenObjSlider;

    public float ExtraSpacing=3;

    private GameObject[] greeneryPrefabs; 

    public void Start()
    {
        greeneryPrefabs = new GameObject[] { TreePrefab, FlowerPrefab};
        greenObjSlider.onValueChanged.AddListener(OnGreenObjectSliderValueChanged);
        // Initial generation 
        GenerateGreenery(greenObjSlider.value);
    }

    void OnGreenObjectSliderValueChanged(float value)
    {
        Debug.Log("Green object slider value changed to: " + value);
        GenerateGreenery(value);
    }

    void GenerateGreenery(float density)
    {
        if (density != 0)
        {
            density = 14 - density; 
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

        for (int i = 0; i <= numberOfObjects; i++)
        {
            float t = (float)i / numberOfObjects; 
            Vector3 position = Vector3.Lerp(start, end, t);

            GameObject prefabToSpawn = greeneryPrefabs[i % 2];

            Instantiate(prefabToSpawn, position, Quaternion.identity);
        }
    }

    void ClearGreenery()
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
