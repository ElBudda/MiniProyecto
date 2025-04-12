using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3d : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Vector3 movement;
    private bool isGrounded;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical"); // New line: forward/back input

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }
}


