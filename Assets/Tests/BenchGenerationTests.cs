using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;


public class BenchGenerationTests
{
    private PublicSpaceGeneration publicSpaceManager;
    private GameObject leftStripStart, leftStripEnd, rightStripStart, rightStripEnd;
    private GameObject[] benchPrefabs;
    private Slider publicSpaceSlider;

    [SetUp]
    public void Setup()
    {
        GameObject managerObject = new GameObject("PublicSpaceManager");
        publicSpaceManager = managerObject.AddComponent<PublicSpaceGeneration>();

        leftStripStart = new GameObject("LeftStripStart") { transform = { position = new Vector3(3.46f, 0, -199.3f) } };
        leftStripEnd = new GameObject("LeftStripEnd") { transform = { position = new Vector3(3.46f, 0, 371.4f) } };
        rightStripStart = new GameObject("RightStripStart") { transform = { position = new Vector3(3, 14.68f, -293.2f) } };
        rightStripEnd = new GameObject("RightStripEnd") { transform = { position = new Vector3(3, 14.68f, 277.42f) } };

        publicSpaceManager.GetType().GetField("LeftStripStart", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(publicSpaceManager, leftStripStart);
        publicSpaceManager.GetType().GetField("LeftStripEnd", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(publicSpaceManager, leftStripEnd);
        publicSpaceManager.GetType().GetField("RightStripStart", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(publicSpaceManager, rightStripStart);
        publicSpaceManager.GetType().GetField("RightStripEnd", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(publicSpaceManager, rightStripEnd);

        benchPrefabs = new GameObject[2];
        benchPrefabs[0] = new GameObject("BenchPrefab1");
        benchPrefabs[1] = new GameObject("BenchPrefab2");

        publicSpaceManager.GetType().GetField("BenchPrefabs", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(publicSpaceManager, benchPrefabs);

        GameObject sliderObject = new GameObject("PublicSpaceSlider");
        publicSpaceSlider = sliderObject.AddComponent<Slider>();
        publicSpaceSlider.maxValue = 10;
        publicSpaceSlider.minValue = 0;
        publicSpaceSlider.value = 5;

        publicSpaceManager.GetType().GetField("publicSpaceSlider", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(publicSpaceManager, publicSpaceSlider);
    }

    [Test]
    public void TestGenerateBenchesAndSliderDensityChanges()
    {
        publicSpaceSlider.value = 1;
        publicSpaceSlider.onValueChanged.Invoke(publicSpaceSlider.value); // Ensures event call
        int lowDensityCount = GameObject.FindGameObjectsWithTag("Bench").Length;

        publicSpaceSlider.value = 10;
        publicSpaceSlider.onValueChanged.Invoke(publicSpaceSlider.value);
        int highDensityCount = GameObject.FindGameObjectsWithTag("Bench").Length;

        Assert.Greater(highDensityCount, lowDensityCount, "Higher density should generate more benches.");

        // Additional check to ensure spacing logic is respected
        if (highDensityCount > 1)
        {
            GameObject[] benches = GameObject.FindGameObjectsWithTag("Bench");
            for (int i = 0; i < benches.Length - 1; i++)
            {
                float dist = Vector3.Distance(benches[i].transform.position, benches[i + 1].transform.position);
                Assert.GreaterOrEqual(dist, 5f, "Benches should maintain a minimum spacing based on density.");
            }
        }
    }


    [TearDown]
    public void Teardown()
    {
        foreach (var obj in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            Object.DestroyImmediate(obj);
        }
    }

}
