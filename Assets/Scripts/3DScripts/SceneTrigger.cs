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
            if (LightingManager.Instance.IsNight())
            {
                targetScene = sceneManager.sewerScene;
                Debug.Log("Entered Sewer Entrance - Target Scene: " + targetScene);
                canSwitchScene = true;
            }
            else
            {
                Debug.Log("Sewer is closed during the day.");
                ShowPopup("Come back at night to enter the sewer.");
                canSwitchScene = false;
            }
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
    void ShowPopup(string message)
    {
        // TEMPORARY: You can replace this with a proper UI panel later
        Debug.Log("CLOSED SEWER. Opens at 18:00");
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

