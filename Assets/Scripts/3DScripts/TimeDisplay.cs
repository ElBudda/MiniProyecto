using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private LightingManager lightingManager; // Reference to your script

    void Update()
    {
        float time = lightingManager.TimeOfDay; // Must make TimeOfDay public or expose a getter

        int hours = Mathf.FloorToInt(time);
        int minutes = Mathf.FloorToInt((time - hours) * 60f);

        timeText.text = $"{hours:00}:{minutes:00}";
    }
}

