using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CraftingTable : MonoBehaviour
{
    private GameObject sittingRat;  // Will be found dynamically at runtime
    private bool nearCraftingTable = false;
    private bool isCrafting = false;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    // Reference to scene manager to check current scene
    public int sewerSceneIndex = 2; // Set this to match your sewer scene index

    void Awake()
    {
        // Subscribe to scene change events
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();

        // Reset crafting state
        isCrafting = false;
        nearCraftingTable = false;

        // Make sure normal rat is visible
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        // Make sure movement is enabled
        if (playerMovement != null)
            playerMovement.enabled = true;

        // Find the sitting rat
        FindSittingRat();
    }

    void FindSittingRat()
    {
        // Find by tag instead of name
        GameObject sittingRatObject = GameObject.FindGameObjectWithTag("SittingRat"); // Replace with your actual tag

        if (sittingRatObject != null)
        {
            sittingRat = sittingRatObject;
            // Set initial state
            sittingRat.SetActive(false); // Always start inactive
            Debug.Log("SittingRat found by tag and initial state set to inactive");
        }
        else
        {
            Debug.LogWarning("No object with SittingRat tag found in the scene!");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        // Reset crafting state
        ExitCraftingMode();

        // Re-initialize references
        Initialize();
    }

    void Update()
    {
        if (nearCraftingTable && Input.GetKeyDown(KeyCode.Q))
        {
            if (!isCrafting)
                EnterCraftingMode();
            else
                ExitCraftingMode();
        }
    }

    public void EnableSittingRat(bool enable)
    {
        if (sittingRat != null)
        {
            sittingRat.SetActive(enable);
            Debug.Log("Sitting rat visibility set to: " + enable);
        }
        else
        {
            // Try to find it again in case it wasn't available earlier
            FindSittingRat();
            if (sittingRat != null)
                sittingRat.SetActive(enable);
            else
                Debug.LogError("Sitting rat not found in the scene!");
        }
    }

    void EnterCraftingMode()
    {
        isCrafting = true;

        if (spriteRenderer != null)
            spriteRenderer.enabled = false; // Hide normal rat

        // Show sitting rat
        EnableSittingRat(true);

        // Disable movement
        if (playerMovement != null)
            playerMovement.enabled = false;

        Debug.Log("Entered Crafting Mode");
    }

    void ExitCraftingMode()
    {
        isCrafting = false;

        if (spriteRenderer != null)
            spriteRenderer.enabled = true; // Show normal rat

        // Hide sitting rat
        EnableSittingRat(false);

        // Enable movement
        if (playerMovement != null)
            playerMovement.enabled = true;

        Debug.Log("Exited Crafting Mode");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger with: " + other.gameObject.name);
        if (other.CompareTag("CraftingTable"))
        {
            nearCraftingTable = true;
            Debug.Log("Near crafting table");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CraftingTable"))
        {
            nearCraftingTable = false;
            ExitCraftingMode();
            Debug.Log("Left crafting table");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}