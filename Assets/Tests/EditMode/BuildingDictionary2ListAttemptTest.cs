// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;

// public class BuildingDictionary2ListAttemptTests
// {
//    private BuildingDictionary2ListAttempt buildingManager;
//    private GameObject plotParent;
//    private Slider slider;
//    private List<GameObject> unmodifiedBuildings = new List<GameObject>();
//    private int numOfBuildingsChanged;
//    private float oldPercentage;
//    private int interval;

//    [SetUp]
//    public void Setup()
//    {
//        // Create a GameObject to attach the BuildingDictionary2ListAttempt script
//        GameObject testGameObject = new GameObject("TestBuildingManager");
//        buildingManager = testGameObject.AddComponent<BuildingDictionary2ListAttempt>();
        

//        // Create and set up a slider
//        GameObject sliderObject = new GameObject("Slider");
//        slider = sliderObject.AddComponent<Slider>();
//        slider.maxValue = 6;
//        slider.minValue = 0;
//        interval = Mathf.FloorToInt(100 / slider.maxValue);
//        buildingManager.GetType()
//            .GetField("slider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .SetValue(buildingManager, slider);
//        buildingManager.GetType()
//            .GetField("interval", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .SetValue(buildingManager, interval);

//        // Arrange: Create the root "Plots" GameObject
//        plotParent = new GameObject("PlotParent");

//        // Create LeftPlotsHolder and add plots and buildings
//        GameObject leftPlotsHolder = new GameObject("LeftPlotsHolder");
//        leftPlotsHolder.transform.SetParent(plotParent.transform); // LeftPlotsHolder is a child of Plots

//        for (int i = 0; i < 3; i++) // Add 3 plots with 1 building each
//        {
//            GameObject plot = new GameObject($"Plot_{i}");
//            plot.transform.SetParent(leftPlotsHolder.transform); // Plots are children of LeftPlotsHolder

//            GameObject building = new GameObject($"Building_{i}");
//            building.transform.SetParent(plot.transform); // Buildings are children of the plots
//        }
//        foreach (Transform plot in plotParent.transform)
//        {
//            foreach (Transform child in plot.transform)
//            {
//                unmodifiedBuildings.Add(child.GetChild(0).gameObject);
//            }
//        }
//        buildingManager.GetType()
//            .GetField("plotParent", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .SetValue(buildingManager, plotParent);
//        buildingManager.GetType()
//            .GetField("unmodifiedBuildings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .SetValue(buildingManager, unmodifiedBuildings);

//        // Create building prefabs
//        List<GameObject> buildingPrefabs = new List<GameObject>();
//        for (int i = 0; i < 4; i++)
//        {
//            GameObject prefab = new GameObject($"BuildingPrefab_{i}");
//            buildingPrefabs.Add(prefab);
//        }
//        buildingManager.GetType()
//            .GetField("buildingPrefabs", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .SetValue(buildingManager, buildingPrefabs);

//        // Arrange: Create the root "Plots" GameObject
//        GameObject plots = new GameObject("Plots");
        
//    }

//    [Test]
//    public void TestCleanBuildingList()
//    {
//        // Act
//        buildingManager.GetType()
//            .GetMethod("CleanBuildingList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .Invoke(buildingManager, null);

//        // Assert
//        var cleanedList = (List<GameObject>)buildingManager.GetType()
//            .GetField("unmodifiedBuildings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .GetValue(buildingManager);
//        Assert.AreEqual(0, cleanedList.Count);
//    }

//    [Test]
//    public void TestBuildingChangeAndReset()
//    {
//        // Act
//        var buildingChangeMethod = buildingManager.GetType().GetMethod("BuildingChange", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

//        // Pass the required count parameter to the method (e.g., 2)
//        buildingChangeMethod.Invoke(buildingManager, new object[] { 2 });

//        // Assert
//        var remainingBuildings = (List<GameObject>)buildingManager.GetType()
//            .GetField("unmodifiedBuildings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .GetValue(buildingManager);
//        Assert.AreEqual(1, remainingBuildings.Count);

//        //Act
//        buildingManager.GetType()
//            .GetMethod("BuildingReset", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .Invoke(buildingManager, new object[] { 2 });
//        //Assert
//        remainingBuildings = (List<GameObject>)buildingManager.GetType()
//            .GetField("unmodifiedBuildings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .GetValue(buildingManager);
//        Assert.AreEqual(3, remainingBuildings.Count);
//    }

//    [Test]
//    public void TestSliderChange()
//    {
//        oldPercentage = 0;
//        buildingManager.GetType()
//            .GetField("oldPercentage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .SetValue(buildingManager, oldPercentage);
//        buildingManager.GetType().GetField("numOfBuildingsChanged", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .SetValue(buildingManager, numOfBuildingsChanged);

//        buildingManager.GetType()
//            .GetMethod("SliderChange", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .Invoke(buildingManager, new object[] { 2 });

//        var remainingBuildings = (List<GameObject>)buildingManager.GetType()
//            .GetField("unmodifiedBuildings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .GetValue(buildingManager);
//        Assert.AreEqual(2, remainingBuildings.Count);

//        oldPercentage = 32;
//        buildingManager.GetType()
//            .GetField("oldPercentage", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .SetValue(buildingManager, oldPercentage);

//        buildingManager.GetType()
//            .GetMethod("SliderChange", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .Invoke(buildingManager, new object[] { 1 });

//        remainingBuildings = (List<GameObject>)buildingManager.GetType()
//            .GetField("unmodifiedBuildings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//            .GetValue(buildingManager);
//        Assert.AreEqual(3, remainingBuildings.Count);

//    }

// }