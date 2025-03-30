using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemName;
    public Sprite icon;
    public GameObject worldPrefab;
}

public class ItemDatabase : MonoBehaviour
{
    public List<ItemData> allItems; // Assign items in Unity Inspector
    private Dictionary<string, ItemData> itemLookup = new Dictionary<string, ItemData>();

    void Awake()
    {
        foreach (ItemData item in allItems)
        {
            itemLookup[item.itemName] = item;
        }
    }

    public ItemData GetItem(string itemName)
    {
        return itemLookup.ContainsKey(itemName) ? itemLookup[itemName] : null;
    }
}

