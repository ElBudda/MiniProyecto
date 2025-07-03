using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] public float TimeOfDay;

    public float dayDurationSeconds = 10f;
    public bool hasHibernatedToday = true;


    public static LightingManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        TimeOfDay = 6f; // Always start morning

        Debug.Log(" LightingManager: TimeOfDay forced to 6AM at start.");
    }

    private void Update()
    {
        if (Preset == null) return;

        if (Application.isPlaying)
        {
            if (IsNight())
            {
                // Always advance at night
                TimeOfDay += (24f / dayDurationSeconds) * Time.deltaTime;
                TimeOfDay %= 24f;
            }
            else
            {
                if (hasHibernatedToday)
                {
                    // Normal daytime progression
                    TimeOfDay += (24f / dayDurationSeconds) * Time.deltaTime;
                    TimeOfDay %= 24f;
                }
                else
                {
                    // Player failed to sleep = stuck at 6am
                    TimeOfDay = 6f;
                }
            }

            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }






    public bool IsNight()
    {
        // Example: Night is between 18:00 and 6:00
        return TimeOfDay >= 18 || TimeOfDay < 6;
    }
    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}
