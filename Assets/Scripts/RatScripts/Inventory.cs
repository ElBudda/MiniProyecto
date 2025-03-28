using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
            Debug.Log("Added: " + itemName);
        }
        else
        {
            items[itemName] = 1;
            Debug.Log("Added: " + itemName);
        }

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
                items.Remove(itemName);
                Debug.Log("Removed item: " + itemName + "from inventory");
        }
    }
}

