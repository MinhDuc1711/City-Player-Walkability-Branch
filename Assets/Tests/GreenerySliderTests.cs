

using NUnit.Framework;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class GreeneryGenerationTests
{
    private GreeneryGeneration greeneryManager;
    private GameObject leftGreenStripStart;
    private GameObject leftGreenStripEnd;
    private GameObject rightGreenStripStart;
    private GameObject rightGreenStripEnd;
    private List<GameObject> treePrefabs;
    private GameObject flowerPrefab;
    private Slider greenObjSlider;

    [SetUp]
    public void Setup()
    {
        GameObject greeneryManagerObject = new GameObject("GreeneryManager");
        greeneryManager = greeneryManagerObject.AddComponent<GreeneryGeneration>();

        leftGreenStripStart = new GameObject("LeftGreenStripStart") { transform = { position = new Vector3(0, 0, 0) } };
        leftGreenStripEnd = new GameObject("LeftGreenStripEnd") { transform = { position = new Vector3(10, 0, 0) } };
        rightGreenStripStart = new GameObject("RightGreenStripStart") { transform = { position = new Vector3(0, 0, 5) } };
        rightGreenStripEnd = new GameObject("RightGreenStripEnd") { transform = { position = new Vector3(10, 0, 5) } };

        greeneryManager.GetType()
            .GetField("LeftGreenStripStart", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(greeneryManager, leftGreenStripStart);

        greeneryManager.GetType()
            .GetField("LeftGreenStripEnd", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(greeneryManager, leftGreenStripEnd);

        greeneryManager.GetType()
            .GetField("RightGreenStripStart", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(greeneryManager, rightGreenStripStart);

        greeneryManager.GetType()
            .GetField("RightGreenStripEnd", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(greeneryManager, rightGreenStripEnd);

        treePrefabs = new List<GameObject>
        {
            new GameObject("TreePrefab1"),
            new GameObject("TreePrefab2")
        };

        foreach (var prefab in treePrefabs)
        {
            greeneryManager.GetType()
                .GetField("TreePrefabs", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .SetValue(greeneryManager, treePrefabs.ToArray());
        }

        flowerPrefab = new GameObject("FlowerPrefab");
        greeneryManager.GetType()
            .GetField("FlowerPrefab", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(greeneryManager, flowerPrefab);

        GameObject sliderObject = new GameObject("GreenObjSlider");
        greenObjSlider = sliderObject.AddComponent<Slider>();
        greenObjSlider.maxValue = 15;
        greenObjSlider.minValue = 0;

        greeneryManager.GetType()
            .GetField("greenObjSlider", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .SetValue(greeneryManager, greenObjSlider);
    }
   

    [Test]
    public void TestGenerateGreenery_CreatesObjects()
    {
        greeneryManager.GetType()
            .GetMethod("GenerateGreenery", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            .Invoke(greeneryManager, new object[] { 5f });

        var spawnedObjects = Object.FindObjectsOfType<GameObject>();
        int treeCount = 0;
        int flowerCount = 0;

        foreach (var obj in spawnedObjects)
        {
            if (treePrefabs.Contains(obj))
                treeCount++;
            else if (obj.name == flowerPrefab.name)
                flowerCount++;
        }

        Assert.IsTrue(treeCount > 0, "Trees were not generated as expected.");
        Assert.IsTrue(flowerCount > 0, "Flowers were not generated as expected.");
    }

    [Test]
    public void TestGenerateGreenery_RemovesObjects()
    {
        greeneryManager.GenerateGreenery(5);

        greeneryManager.ClearGreenery();

        int treeCount = GameObject.FindGameObjectsWithTag("Tree").Length;
        int flowerCount = GameObject.FindGameObjectsWithTag("Flower").Length;

        int objectCount = Object.FindObjectsOfType<GameObject>()
                                        .Count(obj => greeneryManager.TreePrefabs.Contains(obj) ||
                                                      obj.name == greeneryManager.FlowerPrefab.name);

        Assert.AreEqual(0, treeCount, "Trees were not cleared.");
        Assert.AreEqual(0, flowerCount, "Flowers were not cleared.");
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


