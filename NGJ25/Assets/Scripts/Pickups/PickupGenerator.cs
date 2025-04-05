using System.Collections.Generic;
using Coherence.Toolkit;
using UnityEngine;
using UnityEngine.Splines;

public class PickupGenerator : MonoBehaviour
{
    [SerializeField]
    private SplineInstantiate splineInstantiate;

    [SerializeField]
    private SplineInstantiate.InstantiableItem[] powerUps= new SplineInstantiate.InstantiableItem[6];

    public void Initialize(SplineContainer container)
    {
        splineInstantiate.Container = container;
        splineInstantiate.itemsToInstantiate = powerUps;
        splineInstantiate.Clear();
        
        Invoke("Spawn",10);
    }

    private void Spawn()
    {
        splineInstantiate.UpdateInstances();
    }
}
