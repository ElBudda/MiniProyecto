using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatJump : MonoBehaviour
{
    // Start is called before the first frame update
    public float jumpForce = 5f; // Adjust as needed
    public LayerMask groundLayer; // Assign this in the Inspector
    public Transform groundCheck; // Assign an empty GameObject at the rat's feet

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
}
