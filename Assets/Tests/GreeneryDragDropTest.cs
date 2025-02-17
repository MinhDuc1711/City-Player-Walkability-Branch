#if UNITY_EDITOR
using NUnit.Framework;
#endif
using System.Reflection;
using UnityEngine;

public class GreeneryDragDropTests
{
    private GreenObjectManager greenObjectManager;
    private GameObject greeneryObject;
    private Transform originalParent;

    [SetUp]
    public void SetUp()
    {
        // Create a GameObject for GreenObjectManager and attach necessary components
        GameObject managerObject = new GameObject("GreenObjectManager");
        greenObjectManager = managerObject.AddComponent<GreenObjectManager>();

        // Mock greenery object and set as pending object
        greeneryObject = new GameObject("GreeneryObject");
        greeneryObject.tag = "Object";  // Ensure it has the "Object" tag
        greenObjectManager.pendingObj = greeneryObject;

        // Create a mock original parent for the greenery object
        originalParent = new GameObject("OriginalParent").transform;
        greeneryObject.transform.SetParent(originalParent);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up objects after each test
        Object.DestroyImmediate(greeneryObject);
        Object.DestroyImmediate(originalParent.gameObject);
        Object.DestroyImmediate(greenObjectManager.gameObject);
    }

    [Test]
    public void CanPlaceObject_WhenNoCollisions()
    {
        // Arrange
        greenObjectManager.canPlace = true;

        // Act
        InvokePrivateMethod(greenObjectManager, "PlaceObject");

        // Assert
        Assert.IsNull(greenObjectManager.pendingObj, "Pending object should be null after placement.");
    }

    [Test]
    public void RotateObject_OnRotateInput()
    {
        // Arrange
        float initialRotation = greeneryObject.transform.rotation.eulerAngles.y;

        // Act
        InvokePrivateMethod(greenObjectManager, "RotateObject");
        float newRotation = greeneryObject.transform.rotation.eulerAngles.y;

        // Assert
        Assert.AreEqual(initialRotation + greenObjectManager.rotateAmount, newRotation, 0.01f, "Object should rotate by the specified amount.");
    }

    // Helper method to invoke private methods using reflection
    private void InvokePrivateMethod(object obj, string methodName)
    {
        MethodInfo methodInfo = obj.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo != null)
        {
            methodInfo.Invoke(obj, null);
        }
        else
        {
            Assert.Fail($"Method {methodName} not found in {obj.GetType()}");
        }
    }
}
