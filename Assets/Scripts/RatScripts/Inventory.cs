using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> items = new Dictionary<string, int>();

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
            items[itemName]++;
        else
            items[itemName] = 1;
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
            if (items[itemName] <= 0)
                items.Remove(itemName);
        }
    }
}

