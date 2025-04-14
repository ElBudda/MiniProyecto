using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        // Make the rat face the same direction the camera is facing
        Vector3 camForward = cam.transform.forward;
        //camForward.y = 0f; // Optional: ignore vertical tilt, so the rat stays upright
        transform.forward = camForward;
    }
}

