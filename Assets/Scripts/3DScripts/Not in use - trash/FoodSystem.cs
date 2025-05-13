using UnityEngine;

public class FoodSystem : MonoBehaviour
{
    public int foodRequiredPerDay = 3;  // How much food rat needs each day
    public int currentFood = 0;

    // Call this when player picks up food
    public void CollectFood(int amount)
    {
        currentFood += amount;
        Debug.Log("Collected food. Current food: " + currentFood);
    }

    // Call this at end of day (or when entering sewer)
    public void EndOfDayCheck()
    {
        if (currentFood >= foodRequiredPerDay)
        {
            currentFood -= foodRequiredPerDay;
            Debug.Log("Rat survived the day. Remaining food: " + currentFood);
            // TODO: Trigger sleep animation, progress to next day
        }
        else
        {
            Debug.Log("Not enough food. Rat dies or gets penalty.");
            // TODO: Trigger death or penalty
        }
    }

    // Optional: Call this to check how much food is still needed
    public int FoodNeededToSurvive()
    {
        return Mathf.Max(0, foodRequiredPerDay - currentFood);
    }
}

