using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatTubeMovement : MonoBehaviour
{
    public float speed = 5f;               // Movement speed inside the tube
    public Transform tubePath;             // Assign the TubePath GameObject here
    public SpriteRenderer spriteRenderer;  // Assign the rat's SpriteRenderer

    private Rigidbody2D ratRb;
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;
    private bool inTube = false;

    void Start()
    {
        ratRb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (inTube)
        {
            MoveThroughTube();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W)) // Press W to enter a tube
            {
                TryEnterTube();
            }
        }
    }

    void TryEnterTube()
    {
        Collider2D tubeEntrance = Physics2D.OverlapCircle(transform.position, 0.2f);

        if (tubeEntrance != null)
        {
            TubePath tube = tubeEntrance.GetComponent<TubePath>();
            if (tube != null)
            {
                tubePath = tube.waypointParent; // Get waypoints from the TubePath script
                EnterTube();
            }
        }
    }

    void EnterTube()
    {
        if (tubePath == null)
        {
            Debug.LogError("Tube path not assigned!");
            return;
        }

        waypoints.Clear();
        foreach (Transform child in tubePath)
        {
            waypoints.Add(child);
        }

        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints found in tube path!");
            return;
        }

        inTube = true;
        spriteRenderer.enabled = false; // Hide rat
        currentWaypointIndex = 0;
        transform.position = waypoints[currentWaypointIndex].position;

        if (inTube == true)
        {
            ratRb.gravityScale = 0;
        }

        }

    void MoveThroughTube()
    {
        if (currentWaypointIndex < waypoints.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            ExitTube();
        }
    }

    void ExitTube()
    {
        inTube = false;
        spriteRenderer.enabled = true; // Show rat again
        if (inTube == false)
        {
            ratRb.gravityScale = 1;
        }
    }
}
