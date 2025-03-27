using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTimeLogic : MonoBehaviour
{
    public Color dayColor = Color.white;
    public Color nightColor = new Color(0.2f, 0.2f, 0.4f);
    public float fullDayDuration = 300f; // 5 minutes

    private SpriteRenderer[] backgroundSprites;
    private float timeElapsed = 0f; // Tracks time progression
    private bool isNight = false;

    void Start()
    {
        backgroundSprites = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // Increase timeElapsed to track cycle progression
        timeElapsed += Time.deltaTime;

        // Calculate the cycle progress (0 to 1)
        float cycleProgress = (timeElapsed % fullDayDuration) / fullDayDuration;

        // Determine if it's night (night starts after 50% of the cycle)
        isNight = cycleProgress >= 0.5f;

        // Lerp between day and night colors based on cycle progress
        float nightProgress = Mathf.PingPong(cycleProgress * 2, 1); // 0 to 1 to  0

        foreach (SpriteRenderer sprite in backgroundSprites)
        {
            sprite.color = Color.Lerp(dayColor, nightColor, nightProgress);
        }

    }
}

