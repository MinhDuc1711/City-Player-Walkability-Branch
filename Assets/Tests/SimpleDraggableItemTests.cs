using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleDraggableItemTests
{
    private DraggableItem draggableItem;
    private GameObject draggableGameObject;
    private Transform originalParent;

    [SetUp]
    public void SetUp()
    {
        // Create GameObject for DraggableItem and attach necessary components
        draggableGameObject = new GameObject("DraggableItem");
        draggableItem = draggableGameObject.AddComponent<DraggableItem>();

        // Add an Image component to avoid null reference
        draggableItem.image = draggableGameObject.AddComponent<Image>();

        // Create a mock original parent for the draggable item
        originalParent = new GameObject("OriginalParent").transform;
        draggableItem.parentAfterDrag = originalParent;

        // Set the DraggableItem's initial parent
        draggableGameObject.transform.SetParent(originalParent);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(draggableGameObject);
        Object.DestroyImmediate(originalParent.gameObject);
    }

    [Test]
    public void OnBeginDrag_SetsParentToRootAndDisablesRaycast()
    {
        // Arrange
        var eventData = new PointerEventData(EventSystem.current);

        // Act
        draggableItem.OnBeginDrag(eventData);

        // Assert
        Assert.AreEqual(draggableGameObject.transform.root, draggableGameObject.transform.parent, "Draggable item should be reparented to root.");
        Assert.IsFalse(draggableItem.image.raycastTarget, "Raycast target should be disabled on begin drag.");
    }

    [Test]
    public void OnEndDrag_ReparentsToOriginalParentAndEnablesRaycast()
    {
        // Arrange
        var eventData = new PointerEventData(EventSystem.current);
        draggableItem.OnBeginDrag(eventData); // Simulate beginning the drag

        // Act
        draggableItem.OnEndDrag(eventData);

        // Assert
        Assert.AreEqual(originalParent, draggableGameObject.transform.parent, "Draggable item should be reparented back to original parent on end drag.");
        Assert.IsTrue(draggableItem.image.raycastTarget, "Raycast target should be enabled after end drag.");
    }
}
