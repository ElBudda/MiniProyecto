using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [System.Serializable]
    public class ItemData
    {
        public string name;
        public Sprite icon;
        public ItemType type;
        public int foodValue;

        public ItemData(string name, ItemType type, int foodValue = 0, Sprite icon = null)
        {
            this.name = name;
            this.type = type;
            this.foodValue = foodValue;
            this.icon = icon;
        }
    }

    public enum ItemType
    {
        Food,
        Trash,
        Tool,
        Other
    }

    public List<ItemData> inventory = new List<ItemData>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }


    public void EatItem(int index)
    {
        if (index < 0 || index >= inventory.Count) return;

        ItemData item = inventory[index];
        if (item.type == ItemType.Food)
        {
            PlayerFoodSystem.Instance.AddFoodPoints(item.foodValue);
            Debug.Log($"Ate {item.name} (+{item.foodValue} points)");
            inventory.RemoveAt(index);
        }
        else
        {
            Debug.LogWarning("Item is not food!");
        }
    }

    public void AddItem(string name, ItemType type, int foodValue = 0, Sprite icon = null)
    {
        inventory.Add(new ItemData(name, type, foodValue, icon));
        Debug.Log($"Picked up {type}: {name}");
    }

}

