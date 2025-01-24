using UnityEngine;

public class CameraLock : MonoBehaviour
{

    public FPSController controller;

    private void Start()
    {
    }

    void lockCamera()
    {
        controller.enabled = false;
    }

    void unlockCamera()
    {
        controller.enabled = true;
    }

    public void controlCamera(bool val)
    {
        if (val)
            lockCamera();
        else if (!val)
            unlockCamera();
    }
}
