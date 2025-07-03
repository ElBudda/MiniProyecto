using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenuLogic : MonoBehaviour
{
    public string citySceneName = "City";
    public string menuSceneName = "Menu";

    public void StartGame()
    {
        SceneManager.LoadScene(citySceneName);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        PlayerHealthSystem.Instance = null;
        PlayerFoodSystem.Instance = null;
        InventoryManager.Instance = null;
        LightingManager.Instance = null;


        SceneManager.LoadScene(menuSceneName);
    }

}

