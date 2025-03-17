using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class ClearBenchTests
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

        publicSpaceManager.GetType().GetField("LeftStripStart", BindingFlags.Public | BindingFlags.Instance)
            .SetValue(publicSpaceManager, leftStripStart);
        publicSpaceManager.GetType().GetField("LeftStripEnd", BindingFlags.Public | BindingFlags.Instance)
            .SetValue(publicSpaceManager, leftStripEnd);
        publicSpaceManager.GetType().GetField("RightStripStart", BindingFlags.Public | BindingFlags.Instance)
            .SetValue(publicSpaceManager, rightStripStart);
        publicSpaceManager.GetType().GetField("RightStripEnd", BindingFlags.Public | BindingFlags.Instance)
            .SetValue(publicSpaceManager, rightStripEnd);

        benchPrefabs = new GameObject[2];
        benchPrefabs[0] = new GameObject("BenchPrefab1");
        benchPrefabs[1] = new GameObject("BenchPrefab2");

        publicSpaceManager.GetType().GetField("BenchPrefabs", BindingFlags.Public | BindingFlags.Instance)
            .SetValue(publicSpaceManager, benchPrefabs);

        GameObject sliderObject = new GameObject("PublicSpaceSlider");
        publicSpaceSlider = sliderObject.AddComponent<Slider>();
        publicSpaceSlider.maxValue = 10;
        publicSpaceSlider.minValue = 0;
        publicSpaceSlider.value = 5;

        publicSpaceManager.GetType().GetField("publicSpaceSlider", BindingFlags.Public | BindingFlags.Instance)
            .SetValue(publicSpaceManager, publicSpaceSlider);
    }

    [Test]
    public void TestClearBenches()
    {
        // Get the GenerateBenches method via reflection and invoke it
        MethodInfo generateBenchesMethod = publicSpaceManager.GetType()
            .GetMethod("GenerateBenches", BindingFlags.NonPublic | BindingFlags.Instance);

        if (generateBenchesMethod != null)
        {
            generateBenchesMethod.Invoke(publicSpaceManager, new object[] { publicSpaceSlider.value });
        }
        else
        {
            Assert.Fail("Method GenerateBenches not found.");
        }

        var spawnedBenches = GameObject.FindGameObjectsWithTag("Bench");

        // Verify benches are generated
        Assert.IsTrue(spawnedBenches.Length > 0, "No benches generated.");

        // Get the ClearBenches method via reflection and invoke it
        MethodInfo clearBenchesMethod = publicSpaceManager.GetType()
            .GetMethod("ClearBenches", BindingFlags.NonPublic | BindingFlags.Instance);

        if (clearBenchesMethod != null)
        {
            clearBenchesMethod.Invoke(publicSpaceManager, null);
        }
        else
        {
            Assert.Fail("Method ClearBenches not found.");
        }

        // Verify benches are cleared
        spawnedBenches = GameObject.FindGameObjectsWithTag("Bench");
        Assert.AreEqual(0, spawnedBenches.Length, "Benches were not cleared successfully.");
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
