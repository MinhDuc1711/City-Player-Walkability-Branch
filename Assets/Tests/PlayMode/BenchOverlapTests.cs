// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;




// public class BenchOverlapTests
// {
//     private PublicSpaceGeneration publicSpaceManager;
//     private GameObject leftStripStart, leftStripEnd, rightStripStart, rightStripEnd;
//     private GameObject[] benchPrefabs;
//     private Slider publicSpaceSlider;

//     [SetUp]
//     public void Setup()
//     {
//         GameObject managerObject = new GameObject("PublicSpaceManager");
//         publicSpaceManager = managerObject.AddComponent<PublicSpaceGeneration>();

//         leftStripStart = new GameObject("LeftStripStart") { transform = { position = new Vector3(3.46f, 0, -199.3f) } };
//         leftStripEnd = new GameObject("LeftStripEnd") { transform = { position = new Vector3(3.46f, 0, 371.4f) } };
//         rightStripStart = new GameObject("RightStripStart") { transform = { position = new Vector3(3, 14.68f, -293.2f) } };
//         rightStripEnd = new GameObject("RightStripEnd") { transform = { position = new Vector3(3, 14.68f, 277.42f) } };

//         publicSpaceManager.GetType().GetField("LeftStripStart", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
//             .SetValue(publicSpaceManager, leftStripStart);
//         publicSpaceManager.GetType().GetField("LeftStripEnd", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
//             .SetValue(publicSpaceManager, leftStripEnd);
//         publicSpaceManager.GetType().GetField("RightStripStart", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
//             .SetValue(publicSpaceManager, rightStripStart);
//         publicSpaceManager.GetType().GetField("RightStripEnd", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
//             .SetValue(publicSpaceManager, rightStripEnd);

//         benchPrefabs = new GameObject[2];
//         benchPrefabs[0] = new GameObject("BenchPrefab1");
//         benchPrefabs[1] = new GameObject("BenchPrefab2");

//         publicSpaceManager.GetType().GetField("BenchPrefabs", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
//             .SetValue(publicSpaceManager, benchPrefabs);

//         GameObject sliderObject = new GameObject("PublicSpaceSlider");
//         publicSpaceSlider = sliderObject.AddComponent<Slider>();
//         publicSpaceSlider.maxValue = 10;
//         publicSpaceSlider.minValue = 0;
//         publicSpaceSlider.value = 5;

//         publicSpaceManager.GetType().GetField("publicSpaceSlider", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
//             .SetValue(publicSpaceManager, publicSpaceSlider);
//     }

//     [Test]
//     public void TestBenchesDoNotOverlap_Optimized()
//     {
//         publicSpaceSlider.value = 5; // Mid-range density
//         publicSpaceSlider.onValueChanged.Invoke(publicSpaceSlider.value); // Triggers actual generation

//         var spawnedBenches = GameObject.FindGameObjectsWithTag("Bench");
//         Assert.Greater(spawnedBenches.Length, 0, "No benches were spawned, test is invalid.");

//         float minSpacing = 5.0f; // Adjust based on spacing logic
//         HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

//         foreach (var bench in spawnedBenches)
//         {
//             Vector3 pos = bench.transform.position;

//             // Check if any previously placed bench is too close
//             foreach (var existingPos in occupiedPositions)
//             {
//                 if (Vector3.Distance(pos, existingPos) < minSpacing)
//                 {
//                     Assert.Fail($"Benches are overlapping at {pos} and {existingPos}");
//                 }
//             }

//             occupiedPositions.Add(pos);
//         }
//     }


//     [TearDown]
//     public void Teardown()
//     {
//         foreach (var obj in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
//         {
//             Object.DestroyImmediate(obj);
//         }
//     }

// }
