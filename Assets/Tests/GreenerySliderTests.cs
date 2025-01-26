using NUnit.Framework;
using UnityEngine;
using System.Linq; 

public class GreenerySliderTests
{
    private GreeneryGeneration greeneryGeneration;

    [SetUp]
    public void Setup()
    {
        GameObject mockGameObject = new GameObject();
        greeneryGeneration = mockGameObject.AddComponent<GreeneryGeneration>();

        // required properties
        greeneryGeneration.LeftGreenStripStart = new GameObject { transform = { position = new Vector3(0, 0, 0) } };
        greeneryGeneration.LeftGreenStripEnd = new GameObject { transform = { position = new Vector3(10, 0, 0) } };
        greeneryGeneration.RightGreenStripStart = new GameObject { transform = { position = new Vector3(0, 0, 5) } };
        greeneryGeneration.RightGreenStripEnd = new GameObject { transform = { position = new Vector3(10, 0, 5) } };

        GameObject treePrefab1 = new GameObject("TreePrefab1");
        GameObject treePrefab2 = new GameObject("TreePrefab2");
        greeneryGeneration.TreePrefabs = new[] { treePrefab1, treePrefab2 };

        GameObject flowerPrefab = new GameObject("FlowerPrefab");
        greeneryGeneration.FlowerPrefab = flowerPrefab;
    }

    [Test]
    public void GenerateGreenery_DensityVariation_CreatedObjects()
    {
        // Act
        greeneryGeneration.GenerateGreenery(5);

       
        int treeCount = Object.FindObjectsOfType<GameObject>()
                              .Count(obj => greeneryGeneration.TreePrefabs.Contains(obj));
        int flowerCount = Object.FindObjectsOfType<GameObject>()
                                .Count(obj => obj.name == greeneryGeneration.FlowerPrefab.name);

        Assert.IsTrue(treeCount > 0, "Trees were not generated as expected.");
        Assert.IsTrue(flowerCount > 0, "Flowers were not generated as expected.");
    }

    [Test]
    public void GenerateGreenery_RemovedObjects()
    {
        greeneryGeneration.GenerateGreenery(5);

        greeneryGeneration.ClearGreenery();

        int treeCount = GameObject.FindGameObjectsWithTag("Tree").Length;
        int flowerCount = GameObject.FindGameObjectsWithTag("Flower").Length;

        int objectCount = Object.FindObjectsOfType<GameObject>()
                                        .Count(obj => greeneryGeneration.TreePrefabs.Contains(obj) ||
                                                      obj.name == greeneryGeneration.FlowerPrefab.name);

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


