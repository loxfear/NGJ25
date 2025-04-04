using System.Collections.Generic;
using Coherence.Toolkit;
using UnityEngine;
using UnityEngine.Splines;

public class PickupGenerator : MonoBehaviour
{
    [SerializeField]
    private SplineInstantiate splineInstantiate;
    
    public SplineInstantiate SplineInstantiate => this.splineInstantiate;

    private float length;

    [SerializeField]
    private List<GameObject> powerUps;

    public void Initialize(SplineContainer container)
    {
        splineInstantiate.Container = container;
        length = container.CalculateLength();
        Spawn();
    }

    private void Spawn()
    {
        Debug.Log(length);
    }
}
