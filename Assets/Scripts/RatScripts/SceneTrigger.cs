using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public SceneChanger3D sceneManager; // Reference to SceneChanges script

    private int targetScene; // Scene to load 
    private bool canSwitchScene = false; // Checks if rat is in a trigger

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SewerEntrance"))
        {
            targetScene = sceneManager.sewerScene;
            Debug.Log("Entered Sewer Entrance - Target Scene: " + targetScene);
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

    void OnTriggerExit(Collider other)
    {
        canSwitchScene = false;
    }

    void Update()
    {
        if (canSwitchScene && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("loading: " + targetScene);
            sceneManager.sceneToLoad = targetScene;
            sceneManager.LoadScene();
        }
    }
}

