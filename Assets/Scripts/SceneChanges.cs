using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanges : MonoBehaviour
{
    public int sceneToLoad;
    public int sewerScene = 2;
    public int cityScene = 0;
    public int MenuScene = 3;
    public int testingScene = 1;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);

    }

}
