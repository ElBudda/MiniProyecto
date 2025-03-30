using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public SceneChanges sceneManager; // Reference to SceneChanges script

    private int targetScene; // Scene to load when pressing W
    private bool canSwitchScene = false; // Checks if rat is in a trigger

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SewerEntrance"))
        {
            targetScene = sceneManager.sewerScene;
            canSwitchScene = true;
        }
        else if (other.CompareTag("CityEntrance"))
        {
            targetScene = sceneManager.cityScene;
            canSwitchScene = true;
        }
        else if (other.CompareTag("Menu"))
        {
            targetScene = sceneManager.MenuScene;
            canSwitchScene = true;
        }
        else if (other.CompareTag("Testing"))
        {
            targetScene = sceneManager.testingScene;
            canSwitchScene = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canSwitchScene = false;
    }

    void Update()
    {
        if (canSwitchScene && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(targetScene);
            sceneManager.sceneToLoad = targetScene;
            sceneManager.LoadScene();
        }
    }
}

