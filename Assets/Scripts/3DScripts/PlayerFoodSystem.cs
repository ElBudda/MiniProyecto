using UnityEngine;

public class PlayerFoodSystem : MonoBehaviour
{
    public static PlayerFoodSystem Instance;

    public int currentFoodPoints = 0;
    public int requiredFoodPoints = 3; // Food needed per day

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddFoodPoints(int amount)
    {
        currentFoodPoints += amount;
        Debug.Log($"Current Food Points: {currentFoodPoints}/{requiredFoodPoints}");
    }

    public bool CanHibernate()
    {
        return currentFoodPoints >= requiredFoodPoints;
    }

    public void ResetFoodPoints()
    {
        currentFoodPoints = 0;
    }
}

