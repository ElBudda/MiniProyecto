using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    public GameObject sittingRat;  // Assign the sitting rat GameObject
    private bool nearCraftingTable = false;
    private bool isCrafting = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        sittingRat.SetActive(false); // Hide the sitting rat at start
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

    void EnterCraftingMode()
    {
        isCrafting = true;
        spriteRenderer.enabled = false; // Hide normal rat
        sittingRat.SetActive(true); // Show sitting rat
        GetComponent<PlayerMovement>().enabled = false; // Disable movement
        Debug.Log("Entered Crafting Mode");
    }

    void ExitCraftingMode()
    {
        isCrafting = false;
        spriteRenderer.enabled = true; // Show normal rat
        sittingRat.SetActive(false); // Hide sitting rat
        GetComponent<PlayerMovement>().enabled = true; // Enable movement
        Debug.Log("Exited Crafting Mode");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger with: " + other.gameObject.name); // Debugging

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
}
