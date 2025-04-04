using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvironmentData", menuName = "ScriptableObjects/EnvironmentData", order = 1)]
public class EnvironmentData : ScriptableObject
{
    public Color safeFogColor;
    public Color safeLightColor;
    public Color dangerFogColor;
    public Color dangerLightColor;
    public float transitionTime = 0.5f;
    public float dangerFogEndDistance = 100;
    public float safeFogEndDistance = 200;
    public Material skyboxMaterial;

}