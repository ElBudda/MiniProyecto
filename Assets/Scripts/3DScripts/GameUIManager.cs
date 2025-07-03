using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [Header("Screens")]
    public GameObject deathScreen;
    public GameObject victoryScreen;

    void Awake()
    {
        // Singleton logic
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        HideAllScreens();
    }

    void Start()
    {
        HideAllScreens();
    }

    private void HideAllScreens()
    {
        if (deathScreen != null) deathScreen.SetActive(false);
        if (victoryScreen != null) victoryScreen.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        HideAllScreens();
        if (deathScreen != null) deathScreen.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        HideAllScreens();
        if (victoryScreen != null) victoryScreen.SetActive(true);
    }

    public void RestartGame()
    {
        // Destroy any DontDestroyOnLoad player object if it exists
        Destroy(PlayerHealthSystem.Instance?.gameObject);
        // Reset singleton so new one in the new scene can take over
        Instance = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Destroy(PlayerHealthSystem.Instance?.gameObject);
        Instance = null;
        SceneManager.LoadScene("Menu");
    }
}


