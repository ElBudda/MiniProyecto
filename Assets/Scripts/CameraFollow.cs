using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;

    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        AssignTarget(); // Assign the target when the scene starts
    }

    void AssignTarget()
    {
        GameObject rat = GameObject.FindGameObjectWithTag("Player"); // Make sure the rat has the "Player" tag
        if (rat != null)
        {
            vCam.Follow = rat.transform;
        }
    }

    void OnEnable()
    {
        SceneChanges.OnSceneChanged += AssignTarget; // Subscribe to scene change
    }

    void OnDisable()
    {
        SceneChanges.OnSceneChanged -= AssignTarget; // Unsubscribe
    }
}

