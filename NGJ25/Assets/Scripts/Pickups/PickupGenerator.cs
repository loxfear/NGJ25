using System.Collections.Generic;
using Coherence.Toolkit;
using UnityEngine;
using UnityEngine.Splines;

public class PickupGenerator : MonoBehaviour
{
    [SerializeField]
    private SplineInstantiate splineInstantiate;
    
    private float length;

    [SerializeField]
    private SplineInstantiate.InstantiableItem[] powerUps= new SplineInstantiate.InstantiableItem[6];

    public void Initialize(SplineContainer container)
    {
        splineInstantiate.Container = container;
        length = container.CalculateLength();
        Spawn();
    }

    private void Spawn()
    {
        splineInstantiate.itemsToInstantiate = powerUps;
        splineInstantiate.UpdateInstances();
    }
}
