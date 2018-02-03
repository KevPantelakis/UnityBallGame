using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterHandler : MonoBehaviour {

    //This script enables underwater effects. Attach to main camera.


    //The scene's default fog settings
    private bool defaultFog;
    private Color defaultFogColor;
    private float defaultFogDensity;
    private Material defaultSkybox;

    void Start()
    {
        //Set the background color
        Camera cam = GetComponentInChildren<Camera>();
        cam.backgroundColor = new Color(0, 0.4f, 0.7f, 1);
        defaultFog = RenderSettings.fog;
        defaultFogColor = RenderSettings.fogColor;
        defaultFogDensity = RenderSettings.fogDensity;
        defaultSkybox = RenderSettings.skybox;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            print("IN WATER MOTHERFUCKERS!");
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
            RenderSettings.fogDensity = 0.04f;
            RenderSettings.skybox = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            RenderSettings.fog = defaultFog;
            RenderSettings.fogColor = defaultFogColor;
            RenderSettings.fogDensity = defaultFogDensity;
            RenderSettings.skybox = defaultSkybox;
        }
    }
}
