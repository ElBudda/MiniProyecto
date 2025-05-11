using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        // Make sure pause panel is hidden at start
        pausePanel.SetActive(false);
    }

    void Update()
    {
        // Toggle pause when Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        // Toggle pause panel visibility
        pausePanel.SetActive(isPaused);

        // Set time scale (0 = paused, 1 = normal)
        Time.timeScale = isPaused ? 0f : 1f;
    }
}