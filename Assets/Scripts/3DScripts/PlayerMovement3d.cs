using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement3d : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform camTransform; // Reference to the camera
    public bool canMove = true;

    [HideInInspector]
    public bool isKnockedBack = false;

    private Rigidbody rb;
    private Vector3 movement;
    private bool isGrounded;
    Animator anim;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        anim.SetFloat("Xinput", inputX);
        anim.SetFloat("Zinput", inputZ);

        if (!canMove || isKnockedBack)
        {
            anim.SetFloat("Xinput", 0);
            anim.SetFloat("Zinput", 0);
            if (animator != null)
            {
                animator.SetFloat("Speed", 0);
            }
            return;
        }

        // Flatten camera forward/right and normalize
        Vector3 camForward = camTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = camTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        // Calculate camera-relative movement
        movement = (camForward * inputZ + camRight * inputX).normalized;

        if (inputX < 0)
        {
           
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (inputX > 0)
        {
           
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        // Optional: set animator parameters if needed
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.magnitude);
        }
    }

    void FixedUpdate()
    {
        if (!canMove || isKnockedBack) return;

        Vector3 velocity = new Vector3(movement.x * speed, rb.velocity.y, movement.z * speed);
        rb.velocity = velocity;
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



