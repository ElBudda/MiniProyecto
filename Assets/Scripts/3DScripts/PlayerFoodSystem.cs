using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFoodSystem : MonoBehaviour
{
    public static PlayerFoodSystem Instance;

    public int currentFoodPoints = 0;
    public int requiredFoodPoints = 3; // Food needed per day

    public int targetDaysToWin = 5;

    public int dayNumber = 1;
    public int baseFoodRequirement = 3;
    public int foodIncreasePerDay = 1;
    void Start()
    {
        Debug.Log($"PlayerFoodSystem: Starting Day {dayNumber} with requirement {requiredFoodPoints} food.");
    }

    public void AdvanceDay()
    {
        dayNumber++;
        requiredFoodPoints = baseFoodRequirement + (foodIncreasePerDay * (dayNumber - 1));
        ResetFoodPoints();
        Debug.Log($"Day {dayNumber} started. Food needed today: {requiredFoodPoints}");

        if (dayNumber >= targetDaysToWin)
        {
            SceneManager.LoadScene("Victory");

        }

        EnemySpawner.Instance.SpawnForDay(dayNumber);
    }

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

