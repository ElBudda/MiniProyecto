using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName;  // The name of the item this object represents

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Ensure only the player can pick up the item
        {
            Inventory playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(itemName);  // Add the item to the player's inventory
                Destroy(gameObject);  // Destroy the pickup item after it's collected
            }
        }
    }
}

