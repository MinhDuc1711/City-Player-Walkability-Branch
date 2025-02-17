using UnityEngine;
#if UNITY_EDITOR
using NUnit.Framework;
#endif
using System.Collections;
using UnityEngine.TestTools;

public class PedestrianSpawnerTests
{
    private GameObject spawnerObject;
    private PedestrianSpawner pedestrianSpawner;
    private GameObject pedestrianPrefab;
    private Transform leftLane;
    private Transform rightLane;
    private Transform trafficSystemParent;

    [SetUp]
    public void Setup()
    {
        spawnerObject = new GameObject("PedestrianSpawner");
        pedestrianSpawner = spawnerObject.AddComponent<PedestrianSpawner>();

        pedestrianPrefab = new GameObject("Pedestrian");
        pedestrianPrefab.AddComponent<WaypointNavigator>();

        leftLane = new GameObject("LeftLane").transform;
        rightLane = new GameObject("RightLane").transform;

        var leftWaypoint = new GameObject("LeftWaypoint").AddComponent<Waypoint>();
        leftWaypoint.transform.SetParent(leftLane);

        var rightWaypoint = new GameObject("RightWaypoint").AddComponent<Waypoint>();
        rightWaypoint.transform.SetParent(rightLane);

        trafficSystemParent = new GameObject("TrafficSystem").transform;

        pedestrianSpawner.waypointsLeftLane = leftLane;
        pedestrianSpawner.waypointsRightLane = rightLane;
        pedestrianSpawner.pedestriansToSpawn = 3;
        pedestrianSpawner.trafficSystemParent = trafficSystemParent;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(spawnerObject);
        Object.DestroyImmediate(pedestrianPrefab);
        Object.DestroyImmediate(leftLane.gameObject);
        Object.DestroyImmediate(rightLane.gameObject);
        Object.DestroyImmediate(trafficSystemParent.gameObject);
    }

    public IEnumerator Start_BeginsSpawning()
    {
        pedestrianSpawner.Start();
        yield return new WaitForSeconds(1.5f);

        Assert.AreEqual(3, trafficSystemParent.childCount, "Pedestrians are not being spawned correctly");
    }

    public IEnumerator Spawn_StopsWhenLimitReached()
    {
        pedestrianSpawner.Start();
        yield return new WaitForSeconds(3f);

        Assert.AreEqual(pedestrianSpawner.pedestriansToSpawn, trafficSystemParent.childCount, "PedestrianSpawner is exceeding spawn limit");
    }

    [Test]
    public void Start_LogsErrorWhenTrafficSystemParentIsNull()
    {
        pedestrianSpawner.trafficSystemParent = null;


        pedestrianSpawner.Start();
    }

    [Test]
    public void Spawn_LogsErrorForMissingWaypointNavigator()
    {
        Object.DestroyImmediate(pedestrianPrefab.GetComponent<WaypointNavigator>());

        pedestrianSpawner.Start();

    }

    [Test]
    public void Spawn_LogsErrorForMissingWaypoint()
    {
        Object.DestroyImmediate(leftLane.GetChild(0).GetComponent<Waypoint>());

        pedestrianSpawner.Start();

    }

    public IEnumerator PedestrianDestroyed_DecreasesCount()
    {
        pedestrianSpawner.Start();
        yield return new WaitForSeconds(1f);

        pedestrianSpawner.PedestrianDestroyed();

        Assert.AreEqual(2, trafficSystemParent.childCount - 1, "PedestrianDestroyed does not decrement the count properly");
    }
}
