using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanges : MonoBehaviour
{
    public int sceneToLoad;
    public int sewerScene = 3;
    public int cityScene = 1;
    public int MenuScene = 0;
    public int testingScene = 2;

    public static event System.Action OnSceneChanged;

    // Store references to key components
    private CraftingTable craftingTable;
    private RatTubeMovement tubeMover;

    void Start()
    {
        // Find references after scene load
        Debug.Log("Sewer Scene index: " + sewerScene);
        FindReferences();
    }

    // Find all necessary references
    void FindReferences()
    {
        // Find the player or relevant components
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            craftingTable = player.GetComponent<CraftingTable>();
            tubeMover = player.GetComponent<RatTubeMovement>();

            if (craftingTable == null)
                Debug.LogWarning("CraftingTable component not found on player");

            if (tubeMover == null)
                Debug.LogWarning("RatTubeMovement component not found on player");
        }
        else
        {
            Debug.LogError("Player not found in scene!");
        }
    }

    public void LoadScene()
    {
        // Invoke the scene changed event
        OnSceneChanged?.Invoke();

        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Load the scene
        SceneManager.LoadScene(sceneToLoad);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name + " (index: " + scene.buildIndex + ")");

        // Find references again after scene load
        StartCoroutine(SetupAfterSceneLoad(scene.buildIndex));

        // Unsubscribe to avoid multiple calls
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator SetupAfterSceneLoad(int sceneIndex)
    {
        // Wait a short time to ensure all objects are initialized
        yield return new WaitForSeconds(0.1f);

        // Find references again after scene load
        FindReferences();

        // Adjust camera for the loaded scene
        AdjustCameraForScene(sceneIndex);

        // Handle sitting rat visibility
        if (craftingTable != null)
        {
            // Always make sure sitting rat is initially hidden
            craftingTable.EnableSittingRat(false);
        }
    }

    void AdjustCameraForScene(int sceneIndex)
    {
        Cinemachine.CinemachineVirtualCamera vCam = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();

        if (vCam != null)
        {
            if (sceneIndex == sewerScene) // Sewer Scene
            {
                vCam.m_Lens.OrthographicSize = 1.98f;
                vCam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = 0.73f;
                Debug.Log("Camera adjusted for sewer scene");
            }
            else // Other scenes
            {
                vCam.m_Lens.OrthographicSize = 4.27f;
                vCam.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = 0.5f;
                Debug.Log("Camera adjusted for standard scene" + sceneIndex);
            }
        }
        else
        {
            Debug.LogWarning("Cinemachine Virtual Camera not found in scene!");
        }
    }
}
