using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatTubeMovement : MonoBehaviour
{
    public float speed = 5f;               // Movement speed inside the tube
    public Transform tubePath;             // Assign the Tube GameObject (waypoints parent)
    public SpriteRenderer spriteRenderer;  // Assign the rat's SpriteRenderer

    private PolygonCollider2D ratCollider;
    private Rigidbody2D ratRb;
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;
    private bool inTube = false;

    void Start()
    {
        ratRb = GetComponent<Rigidbody2D>();
        ratCollider = GetComponent<PolygonCollider2D>();
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
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) // W to go up, S to go down
            {
                TryEnterTube();
            }
        }
    }

    void TryEnterTube()
    {
        Debug.Log("Checking for tube entrance...");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        foreach (Collider2D col in colliders)
        {
            Debug.Log("Checking collider: " + col.gameObject.name);

            if (col.CompareTag("TubeEntrance") && Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Tube entrance detected (going up): " + col.gameObject.name);
                StartTubeTravel(false); // Normal order (going up)
                return;
            }
            else if (col.CompareTag("TubeExit") && Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Tube exit detected (going down): " + col.gameObject.name);
                StartTubeTravel(true); // Reversed order (going down)
                return;
            }
        }

        Debug.Log("No valid tube entrance found.");
    }

    void StartTubeTravel(bool reverse)
    {
        if (tubePath == null)
        {
            Debug.LogError("Tube path not assigned!");
            return;
        }

        Debug.Log("Starting tube travel. Reverse: " + reverse);

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

        if (reverse)
        {
            waypoints.Reverse(); // Reverse waypoint order for going down
            Debug.Log("Waypoints reversed for downward travel.");
        }

        inTube = true;
        spriteRenderer.enabled = false; // Hide rat
        currentWaypointIndex = 0;
        transform.position = waypoints[currentWaypointIndex].position;

        ratRb.gravityScale = 0; // Disable gravity while in the tube
        Debug.Log("Gravity disabled. Moving through tube.");
    }

    void MoveThroughTube()
    {
        ratCollider.enabled = false;
        if (currentWaypointIndex < waypoints.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.05f)
            {
                currentWaypointIndex++;
                Debug.Log("passed waypoint" + currentWaypointIndex);
            }
        }
        else
        {
            ExitTube();
        }
    }

    void ExitTube()
    {
        ratCollider.enabled = true;
        inTube = false;
        spriteRenderer.enabled = true; // Show rat again
        ratRb.gravityScale = 1; // Restore gravity

        Debug.Log("Exited tube. Gravity restored.");
    }
}
