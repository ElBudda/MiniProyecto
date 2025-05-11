using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;       // Rat transform
    public float rotationSpeed = 3f;

    private float currentAngle = 0f;

    void Start()
    {
        FindRat();
    }

    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        FindRat();
    }

    void FindRat()
    {
        GameObject rat = GameObject.FindWithTag("Player");
        if (rat != null)
        {
            target = rat.transform;
        }
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        currentAngle += mouseX * rotationSpeed;

        // Keep camera at a fixed radius around the rat
        transform.position = target.position;
        transform.rotation = Quaternion.Euler(0, currentAngle, 0);
    }
}

