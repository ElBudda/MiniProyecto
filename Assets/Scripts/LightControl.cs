using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LightControl : MonoBehaviour
{
    public Light2D ratLight; 
    public int sewerSceneIndex = 2; 

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateLightState(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateLightState(scene.buildIndex);
    }

    private void UpdateLightState(int sceneIndex)
    {
        if (ratLight != null)
        {
            ratLight.enabled = (sceneIndex == sewerSceneIndex);
        }
    }
}
