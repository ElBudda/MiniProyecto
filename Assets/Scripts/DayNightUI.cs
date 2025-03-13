using UnityEngine;
using UnityEngine.UI;

public class DayNightUI : MonoBehaviour
{
    public RectTransform sun;
    public RectTransform moon;
    public Transform rotationCenter; // Center of the arc
    public float orbitRadius = 300f; // Radius of the arc
    public NightTimeLogic nightTimeLogic; // Reference to the day-night script
    public Transform watchFaceTransform; //tracking realtive to this

    private float fullDayDuration; // Get duration from NightTimeLogic
    private float timer = 0f;

    void Start()
    {
        if (nightTimeLogic != null)
        {
            fullDayDuration = nightTimeLogic.fullDayDuration; // Get duration from other script
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        float progress = timer / fullDayDuration;

        // Calculate rotation angle from 180° (left) to 0° (right)
        float angle = Mathf.Lerp(180f, 0f, progress) * Mathf.Deg2Rad;

        // Convert rotation center to world position for UI accuracy
        Vector3 rotationCenterWorld = rotationCenter.position;

        // Compute sun & moon positions relative to the rotation center
        Vector3 sunOffset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * orbitRadius;
        Vector3 moonOffset = new Vector3(Mathf.Cos(angle + Mathf.PI), Mathf.Sin(angle + Mathf.PI), 0f) * orbitRadius;

        // Convert back to local space (so it works within the UI system)
        sun.position = rotationCenterWorld + sunOffset;
        moon.position = rotationCenterWorld + moonOffset;

        // Fade transition effect
        float alpha = Mathf.Sin(progress * Mathf.PI);
        sun.GetComponent<Image>().color = new Color(1, 1, 1, 1 - alpha);
        moon.GetComponent<Image>().color = new Color(1, 1, 1, alpha);

        // Reset cycle
        if (timer >= fullDayDuration) timer = 0f;
    }
}


