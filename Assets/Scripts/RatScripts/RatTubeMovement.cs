using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class RatTubeMovement : MonoBehaviour
{
    public float speed = 5f;               // Movement speed inside the tube
    public Transform tubePath;             // Assign TubeParent through script
    public SpriteRenderer spriteRenderer;  // Assign the rat's SpriteRenderer

    private PolygonCollider2D ratCollider;
    private Rigidbody2D ratRb;
    private List<Transform> waypoints = new List<Transform>();
    private int currentWaypointIndex = 0;
    private bool inTube = false;

    void Awake()
    {
        // Subscribe to sceneLoaded event as soon as the script is awake
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        Initialize();
    }

    // Initialize all needed components
    void Initialize()
    {
        if (ratRb == null)
            ratRb = GetComponent<Rigidbody2D>();

        if (ratCollider == null)
            ratCollider = GetComponent<PolygonCollider2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        // Reset tube state when initializing
        inTube = false;
        waypoints.Clear();

        // Check for TubeParent
        CheckForTubeParent();
    }

    // Called whenever a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("New scene loaded, checking for TubeParent...");

        // Reset tube status and references
        tubePath = null;
        inTube = false;
        waypoints.Clear();

        // Ensure visibility and physics are reset
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        if (ratRb != null)
            ratRb.gravityScale = 1;

        if (ratCollider != null)
            ratCollider.enabled = true;

        // Wait a frame to ensure scene is fully loaded before checking for tube parent
        Invoke("CheckForTubeParent", 0.1f);
    }

    // Helper function to find TubeParent in the scene
    void CheckForTubeParent()
    {
        tubePath = null; // Clear the reference first

        // Find by tag instead of name
        GameObject tubeParentObject = GameObject.FindGameObjectWithTag("TubeParent"); 

        if (tubeParentObject != null)
        {
            tubePath = tubeParentObject.transform; // Assign TubeParent's transform
            Debug.Log("Tube path found by tag and assigned: " + tubeParentObject.name);
        }
        else
        {
            Debug.Log("No object with tube path tag found in the current scene. Tube movement will be disabled.");
        }
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
        // Check if we're in a scene with tubes first
        if (tubePath == null)
        {
            Debug.Log("No tube system available in this scene.");
            return;
        }

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
                Debug.Log("passed waypoint " + currentWaypointIndex);
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

    // Ensure cleanup and unsubscribe from event when the object is destroyed
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}