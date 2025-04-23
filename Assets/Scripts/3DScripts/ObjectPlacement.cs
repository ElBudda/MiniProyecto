using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    public Transform rayOrigin;

    void Start()
    {
        DropToGround();
    }

    void DropToGround()
    {
        RaycastHit hit;

        // Visualize the ray in the Scene view
        Debug.DrawRay(rayOrigin.position, Vector3.down * 1.5f, Color.red, 10f);

        // Perform the raycast
        if (Physics.Raycast(rayOrigin.position, Vector3.down, out hit, 1.5f))
        {
            // If the ray hits something, move the object to the hit point
            transform.position = hit.point;

            // Debug output to verify the hit position
            Debug.Log("Hit point: " + hit.point);
        }
        else
        {
            Debug.Log("Ray did not hit anything");
        }
    }

    void Update()
    {
        // Update logic if necessary
    }
}
