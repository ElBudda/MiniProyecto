using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemDatabase itemDatabase;
    public InventoryUIManager uiManager;

    // Dictionary to store items and their quantities
    public Dictionary<string, int> items = new Dictionary<string, int>();

    void Start()
    {
        if (itemDatabase == null)
            itemDatabase = FindObjectOfType<ItemDatabase>();

        if (uiManager == null)
            uiManager = FindObjectOfType<InventoryUIManager>();
    }

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
            items[itemName]++;
        else
            items[itemName] = 1;

        // Update the UI whenever the inventory changes
        uiManager.UpdateUI();
    }

    public bool RemoveItem(string itemName, int count = 1)
    {
        if (!items.ContainsKey(itemName) || items[itemName] < count)
            return false;

        items[itemName] -= count;

        if (items[itemName] <= 0)
            items.Remove(itemName);

        uiManager.UpdateUI();
        return true;
    }

    public ItemData GetItemData(string itemName)
    {
        return itemDatabase.GetItem(itemName);
    }
}