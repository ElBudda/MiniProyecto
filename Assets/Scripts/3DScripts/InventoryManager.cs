using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [System.Serializable]
    public class FoodItem
    {
        public string name;
        public int foodValue;

        public FoodItem(string n, int v)
        {
            name = n;
            foodValue = v;
        }
    }

    public List<FoodItem> foodInventory = new List<FoodItem>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddFood(string name, int value)
    {
        foodInventory.Add(new FoodItem(name, value));
        Debug.Log($"Picked up {name} (+{value} food points)");
    }

    public void EatFood(int index)
    {
        if (index >= 0 && index < foodInventory.Count)
        {
            FoodItem item = foodInventory[index];
            PlayerFoodSystem.Instance.AddFoodPoints(item.foodValue);
            Debug.Log($"Ate {item.name} (+{item.foodValue} points)");
            foodInventory.RemoveAt(index);
        }
    }
}

