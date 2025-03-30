using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Recipe
{
    public string resultItemName;
    public List<string> ingredients;
    public List<int> quantities;
}

public class CraftingSystem : MonoBehaviour
{
    public Inventory playerInventory;
    public List<Recipe> recipes;
    public Transform craftingSlotsParent;
    public Transform resultSlotParent;
    public GameObject itemSlotPrefab;

    // Reference to the active slots in the crafting area
    private List<GameObject> craftingSlots = new List<GameObject>();
    private GameObject resultSlot;

    void Start()
    {
        if (playerInventory == null)
            playerInventory = FindObjectOfType<Inventory>();

        InitializeUI();
    }

    void InitializeUI()
    {
        // Create crafting slots
        foreach (Transform child in craftingSlotsParent)
        {
            craftingSlots.Add(child.gameObject);
        }

        // Get result slot
        resultSlot = resultSlotParent.GetChild(0).gameObject;

        // Set up drag and drop behavior for slots
        // This would need additional drag & drop scripts
    }

    public void TryCraft()
    {
        // Get items in crafting slots
        List<string> currentIngredients = new List<string>();

        // This is placeholder - you'd need to implement gathering 
        // the actual ingredients from your UI slots

        // Check if ingredients match any recipe
        foreach (Recipe recipe in recipes)
        {
            bool matches = CheckRecipeMatch(currentIngredients, recipe);

            if (matches)
            {
                // Remove ingredients from inventory
                foreach (var ingredient in recipe.ingredients)
                {
                    playerInventory.RemoveItem(ingredient);
                }

                // Add result to inventory
                playerInventory.AddItem(recipe.resultItemName);
                break;
            }
        }
    }

    bool CheckRecipeMatch(List<string> ingredients, Recipe recipe)
    {
        // Simple check - would need to be expanded
        if (ingredients.Count != recipe.ingredients.Count)
            return false;

        // Check each ingredient
        // This is a simple implementation - you might need something more complex
        foreach (string ingredient in recipe.ingredients)
        {
            if (!ingredients.Contains(ingredient))
                return false;
        }

        return true;
    }
}
