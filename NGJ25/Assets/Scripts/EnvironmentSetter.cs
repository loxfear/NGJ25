using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSetter : MonoBehaviour
{
    
    public bool isSafe;
    
    public EnvironmentData environmentData;
    
    
    private float timer = 0;
    private bool transitioning = false;
    private Color currentColor;
    private Color targetColor;
    private Color currentLightColor;
    private Color targetLightColor;
    private Light directionalLight;
    private float currentFogEndDistance;
    private float targetFogEndDistance;
    
    private Color safeFogColor;
    private Color safeLightColor;
    private Color dangerFogColor;
    private Color dangerLightColor;
    private float transitionTime = 0.5f;
    private float dangerFogEndDistance = 100;
    private float safeFogEndDistance = 200;
    [SerializeField] private AudioSource dangerMusic;
    [SerializeField] private AudioSource safeMusic;
    
    private bool hasLoaded = false;


    private void Awake()
    {
        LoadEnvironmentData();
    }


    //run in editor
    [ContextMenu("Load Environment Data")]
    public void LoadEnvironmentData()
    {
        safeFogColor = environmentData.safeFogColor;
        safeLightColor = environmentData.safeLightColor;
        dangerFogColor = environmentData.dangerFogColor;
        dangerLightColor = environmentData.dangerLightColor;
        transitionTime = environmentData.transitionTime;
        dangerFogEndDistance = environmentData.dangerFogEndDistance;
        safeFogEndDistance = environmentData.safeFogEndDistance;
        RenderSettings.skybox = environmentData.skyboxMaterial;

        hasLoaded = true;
        
        directionalLight = FindObjectOfType<Light>();
        currentLightColor = directionalLight.color;
        
        
        SetEnvironment();
    }
    
    private void SetEnvironment()
    {
        if (isSafe)
        {
            RenderSettings.fogColor = safeFogColor;
            RenderSettings.fogEndDistance = safeFogEndDistance;
            directionalLight.color = safeLightColor;

        }
        else
        {
            RenderSettings.fogColor = dangerFogColor;
            RenderSettings.fogEndDistance = dangerFogEndDistance;
            directionalLight.color = dangerLightColor;

        }
        currentFogEndDistance = RenderSettings.fogEndDistance * 0.9F;
        currentColor = RenderSettings.fogColor;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!hasLoaded)
        {
            return;
        }
        
        if ((isSafe && RenderSettings.fogColor != safeFogColor) || (!isSafe && RenderSettings.fogColor != dangerFogColor) )
        {
            StartTransition();
        }
        
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * transitionTime);
            currentLightColor = Color.Lerp(currentLightColor, targetLightColor, Time.deltaTime * transitionTime);
            currentFogEndDistance = Mathf.Lerp(currentFogEndDistance, targetFogEndDistance, Time.deltaTime * transitionTime);
            RenderSettings.fogColor = currentColor;
            directionalLight.color = currentLightColor;
            RenderSettings.fogEndDistance = currentFogEndDistance;
            
            //fade music
            if (isSafe)
            {
                safeMusic.volume = Mathf.Lerp(safeMusic.volume, 1, Time.deltaTime * transitionTime);
                dangerMusic.volume = Mathf.Lerp(dangerMusic.volume, 0, Time.deltaTime * transitionTime);
            }
            else
            {
                safeMusic.volume = Mathf.Lerp(safeMusic.volume, 0, Time.deltaTime * transitionTime);
                dangerMusic.volume = Mathf.Lerp(dangerMusic.volume, 1, Time.deltaTime * transitionTime);
            }
            
        }
        else
        {
            transitioning = false;
        }

    }
    
    void StartTransition()
    {
        currentColor = RenderSettings.fogColor;
        transitioning = true;
        timer = transitionTime;
        if (isSafe)
        {
            targetColor = safeFogColor;
            targetLightColor = safeLightColor;
            targetFogEndDistance = safeFogEndDistance;
        }
        else
        {
            targetColor = dangerFogColor;
            targetLightColor = dangerLightColor;
            targetFogEndDistance = dangerFogEndDistance;
        }
    }
}
