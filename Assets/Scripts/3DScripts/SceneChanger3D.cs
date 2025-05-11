using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger3D : MonoBehaviour
{
    public int sceneToLoad;
    public int sewerScene = 2;
    public int cityScene = 1;
    public int MenuScene = 0;
    public int testingScene = 10;

    public static event System.Action OnSceneChanged;


    void Start()
    {

    }

    // Find all necessary references
    public void LoadScene()
    {
       // Load the scene
        SceneManager.LoadScene(sceneToLoad);
    }

}
