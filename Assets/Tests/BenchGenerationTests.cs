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

        leftStripStart = new GameObject("LeftStripStart") { transform = { position = new Vector3(3.46, 0, -199.3) } };
        leftStripEnd = new GameObject("LeftStripEnd") { transform = { position = new Vector3(3.46, 0, 371.4) } };
        rightStripStart = new GameObject("RightStripStart") { transform = { position = new Vector3(3, 14.68, -293.2) } };
        rightStripEnd = new GameObject("RightStripEnd") { transform = { position = new Vector3(3, 14.68, 277.42) } };

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
        publicSpaceManager.OnPublicSpaceSliderValueChanged(publicSpaceSlider.value);
        int lowDensityCount = GameObject.FindGameObjectsWithTag("Bench").Length;

        publicSpaceSlider.value = 10;
        publicSpaceManager.OnPublicSpaceSliderValueChanged(publicSpaceSlider.value);
        int highDensityCount = GameObject.FindGameObjectsWithTag("Bench").Length;

        Assert.Greater(highDensityCount, lowDensityCount, "Higher density should generate more benches.");
    }

    [TearDown]
    public void Teardown()
    {
        foreach (var obj in Object.FindObjectsOfType<GameObject>())
        {
            Object.DestroyImmediate(obj);
        }
    }
}
