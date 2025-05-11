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

        Debug.DrawRay(rayOrigin.position, Vector3.down * 1.5f, Color.red, 10f);

        if (Physics.Raycast(rayOrigin.position, Vector3.down, out hit, 1.5f))
        {
            transform.position = hit.point;

            Debug.Log("Hit point: " + hit.point);
        }
        else
        {
            Debug.Log("Ray did not hit anything");
        }
    }

    void Update()
    {

    }
}
