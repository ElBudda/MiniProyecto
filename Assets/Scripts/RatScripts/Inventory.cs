using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> items = new Dictionary<string, int>();
    public ItemDatabase itemDatabase;  // Reference to the database
    public InventoryUIManager inventoryUIManager;

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }

        Debug.Log("Added: " + itemName);
        inventoryUIManager.UpdateUI();  // Update the UI when an item is added
    }


    public bool HasItem(string itemName, int amount)
    {
        return items.ContainsKey(itemName) && items[itemName] >= amount;
    }

    public void RemoveItem(string itemName, int amount)
    {
        if (HasItem(itemName, amount))
        {
            items[itemName] -= amount;
            Debug.Log("- " + amount + " " + itemName);
            if (items[itemName] <= 0)
            {
                items.Remove(itemName);
                Debug.Log("Removed item: " + itemName + " from inventory");
            }
        }
    }

    public ItemData GetItemData(string itemName)
    {
        return itemDatabase.GetItem(itemName);
    }
}
