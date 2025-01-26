using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GreenerySliderTest
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
        greeneryGeneration.TreePrefabs = new[] { new GameObject(), new GameObject() };
        greeneryGeneration.FlowerPrefab = new GameObject();
    }

   
}

