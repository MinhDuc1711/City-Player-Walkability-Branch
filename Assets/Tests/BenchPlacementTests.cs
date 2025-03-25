// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.UI;


// public class BenchPlacementTests
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
//     public void TestGenerateBenches_CreatesBenchesAndWithinBounds()
//     {
//         publicSpaceManager.GetType().GetMethod("GenerateBenches", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
//             .Invoke(publicSpaceManager, new object[] { publicSpaceSlider.value });

//         var spawnedBenches = GameObject.FindGameObjectsWithTag("Bench");

//         Assert.IsTrue(spawnedBenches.Length > 0, "Benches were not generated as expected.");

//         foreach (var bench in spawnedBenches)
//         {
//             float xPosition = bench.transform.position.x;
//             Assert.IsTrue(xPosition >= leftStripStart.transform.position.x && xPosition <= leftStripEnd.transform.position.x ||
//                           xPosition >= rightStripStart.transform.position.x && xPosition <= rightStripEnd.transform.position.x,
//                 $"Bench out of bounds at {xPosition}");
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
