
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class GreeneryButtonTest
//{
//    private GameObject managerObject;
//    private GreenObjectManager manager;
//    private GreenSelection greenSelection;
//    private GameObject testGreenObject;

//    [SetUp]
//    public void Setup()
//    {
//        managerObject = new GameObject("GreenObjectManager");
//        manager = managerObject.AddComponent<GreenObjectManager>();

//        manager.greeneryObjects = new GameObject[1];
//        testGreenObject = new GameObject("TestGreenObject");
//        manager.greeneryObjects[0] = testGreenObject;

//        greenSelection = managerObject.AddComponent<GreenSelection>();
//        greenSelection.greenObjManager = manager;
//        greenSelection.selectUI = new GameObject();
//        greenSelection.selectUI.SetActive(false);
//        greenSelection.objNameText = new GameObject().AddComponent<Text>();
//    }

//    [TearDown]
//    public void Teardown()
//    {
//        Object.Destroy(managerObject);
//        Object.Destroy(testGreenObject);
//    }

//    [Test]
//    public void GreeneryObjectButton_InstantiatesObject()
//    {
//        manager.SelectObject(0);

//        Assert.NotNull(manager.pendingObj);
//        Assert.IsTrue(manager.pendingObj.name.StartsWith("TestGreenObject"));
//    }

//    [Test]
//    public void Delete_RemovesSelectedObject()
//    {
//        // Simulate selecting and instantiating the greenery object
//        manager.SelectObject(0);
//        GameObject createdObject = manager.pendingObj;

//        // Simulate selecting this object in GreenSelection
//        greenSelection.Select(createdObject);

//        // Confirm selection
//        Assert.AreEqual(createdObject, greenSelection.selectedObj);

//        // Simulate delete action
//        greenSelection.Delete();

//        // Check that the selectedObj has been cleared and selectUI is inactive
//        Assert.IsNull(greenSelection.selectedObj);
//        Assert.IsFalse(greenSelection.selectUI.activeSelf);

//        // Optionally, delay checking object destruction until the end of the frame
//        Object.DestroyImmediate(createdObject);
//        Assert.IsTrue(createdObject == null); // Object should be destroyed
//    }
//}


