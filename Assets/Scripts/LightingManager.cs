using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

[ExecuteAlways]

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    [SerializeField, Range(0, 24)] public float TimeOfDay;
    public bool isSliderActive = false;



    // Update is called once per frame
    void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            if (!isSliderActive) // Only update if the slider is not active
            {
               
            }

            UpdateLighting(TimeOfDay / 24f);
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }

    }

    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        UnityEngine.RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        UnityEngine.RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);

        if (DirectionalLight != null) 
        { 
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent*360f) - 90f, 170, 0 ));

        }

    }

}
