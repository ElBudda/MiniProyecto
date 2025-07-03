using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeDisplay : MonoBehaviour
{

    [SerializeField] private LightingManager lightingManager; // Reference to your script

    [Header("UI References")]
    public TMP_Text dayText;
    public TMP_Text foodText;
    public TMP_Text trashText;
    [SerializeField] private TextMeshProUGUI timeText;
    public TMP_Text warningText;


    void Update()
    {
        float time = lightingManager.TimeOfDay; // Must make TimeOfDay public or expose a getter

        int hours = Mathf.FloorToInt(time);
        int minutes = Mathf.FloorToInt((time - hours) * 60f);

        timeText.text = $"{hours:00}:{minutes:00}";

        // Day Number
        dayText.text = "Day: " + PlayerFoodSystem.Instance.dayNumber;

        // Food Progress
        int current = PlayerFoodSystem.Instance.currentFoodPoints;
        int required = PlayerFoodSystem.Instance.requiredFoodPoints;
        foodText.text = $"Food: {current} / {required}";

        // Trash Points
        trashText.text = "Trash: " + InventoryManager.Instance.trashPoints;

        if (LightingManager.Instance == null || warningText == null) return;

        if (LightingManager.Instance.IsNight())
        {
            warningText.enabled = true;
        }
        else
        {
            warningText.enabled = false;
        }
    }
}

