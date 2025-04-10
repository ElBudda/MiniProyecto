using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject throwingStarPrefab;  // Prefab de la estrella ninja
    public float throwForce = 10f;        // Fuerza de lanzamiento

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            ThrowStar();
        }
    }

    void ThrowStar()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

        GameObject star = Instantiate(throwingStarPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = star.GetComponent<Rigidbody2D>();
        rb.velocity = direction * throwForce;
    }
}
