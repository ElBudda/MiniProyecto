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
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (deathScreen != null) deathScreen.SetActive(false);
        if (victoryScreen != null) victoryScreen.SetActive(false);
    }


    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Destroy(PlayerHealthSystem.Instance?.gameObject);
        SceneManager.LoadScene("Menu"); // Make sure this is your menu scene name
    }
}

