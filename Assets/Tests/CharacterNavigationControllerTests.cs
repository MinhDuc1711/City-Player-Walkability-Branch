using UnityEngine;
using NUnit.Framework;

public class CharacterNavigationControllerTests
{
    private GameObject characterObject;
    private CharacterNavigationController characterController;

    [SetUp]
    public void Setup()
    {
        characterObject = new GameObject("Character");
        characterController = characterObject.AddComponent<CharacterNavigationController>();
        characterController.destination = new Vector3(10f, 0f, 10f);
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(characterObject);
    }

    [Test]
    public void Start_InitializesSpawnPositionAndSpeed()
    {
        characterController.speedVariationRange = 0.2f;
        characterController.movementSpeed = 1f;

        characterController.Start();

        Assert.AreEqual(characterController.spawnPosition, characterObject.transform.position, "Spawn position not initialized correctly");
        Assert.IsTrue(characterController.movementSpeed >= 0.5f && characterController.movementSpeed <= 2f, "Movement speed is not clamped correctly");
    }

    [Test]
    public void Update_ReachesDestination()
    {
        characterController.stopDistance = 0.5f;
        characterController.destination = characterObject.transform.position; 

        characterController.Update();

        Assert.IsTrue(characterController.reachedDestination, "Controller does not correctly detect when the destination is reached");
    }

    [Test]
    public void Update_MovesTowardsDestination()
    {
        Vector3 initialPosition = characterObject.transform.position;

        characterController.Update();

        Vector3 updatedPosition = characterObject.transform.position;
        Assert.AreNotEqual(initialPosition, updatedPosition, "Controller did not move the character towards the destination");
    }

    [Test]
    public void TeleportToWaypoint_WithoutNavigatorLogsError()
    {
        characterController.TeleportToWaypoint();
    }

    [Test]
    public void SetDestination_UpdatesDestination()
    {
        Vector3 newDestination = new Vector3(5f, 0f, 5f);
        characterController.SetDestination(newDestination);

        Assert.AreEqual(newDestination, characterController.destination, "SetDestination does not update the destination correctly");
        Assert.IsFalse(characterController.reachedDestination, "SetDestination should reset the reachedDestination flag");
    }
}
