// using UnityEngine;
// using NUnit.Framework;

// public class WaypointTests
// {
//    private GameObject waypointObject;
//    private Waypoint waypoint;

//    [SetUp]
//    public void Setup()
//    {
//        waypointObject = new GameObject("Waypoint");
//        waypoint = waypointObject.AddComponent<Waypoint>();
//    }

//    [TearDown]
//    public void Teardown()
//    {
//        Object.DestroyImmediate(waypointObject);
//    }

//    [Test]
//    public void GetPosition_WithinBounds()
//    {
//        waypoint.width = 2f;
//        Vector3 position = waypoint.GetPosition();

//        Vector3 rightOffset = waypoint.transform.right * waypoint.width / 2f;
//        Vector3 minBound = waypoint.transform.position - rightOffset;
//        Vector3 maxBound = waypoint.transform.position + rightOffset;

//        Assert.IsTrue(position.x >= minBound.x && position.x <= maxBound.x, "Generated position is outside bounds on the X-axis");
//        Assert.IsTrue(position.y == waypoint.transform.position.y, "Generated position deviates on the Y-axis");
//        Assert.IsTrue(position.z == waypoint.transform.position.z, "Generated position deviates on the Z-axis");
//    }

//    [Test]
//    public void HasNextWaypoint_ReturnsTrueWhenNextWaypointExists()
//    {
//        GameObject nextWaypointObject = new GameObject("NextWaypoint");
//        Waypoint nextWaypoint = nextWaypointObject.AddComponent<Waypoint>();

//        waypoint.nextWaypoint = nextWaypoint;

//        Assert.IsTrue(waypoint.HasNextWaypoint(), "HasNextWaypoint should return true when nextWaypoint is assigned");

//        Object.DestroyImmediate(nextWaypointObject);
//    }

//    [Test]
//    public void HasNextWaypoint_ReturnsFalseWhenNextWaypointIsNull()
//    {
//        waypoint.nextWaypoint = null;

//        Assert.IsFalse(waypoint.HasNextWaypoint(), "HasNextWaypoint should return false when nextWaypoint is null");
//    }
// }
