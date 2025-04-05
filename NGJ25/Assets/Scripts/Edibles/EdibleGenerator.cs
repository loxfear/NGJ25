using System.Collections.Generic;
using Coherence.Toolkit;
using UnityEngine;
using UnityEngine.Splines;

public class EdibleGenerator : MonoBehaviour
{
    [SerializeField]
    private SplineInstantiate splineInstantiate;

    [SerializeField]
    private SplineInstantiate.InstantiableItem[] edibles = new SplineInstantiate.InstantiableItem[6];

    public void Initialize(SplineContainer container)
    {
        splineInstantiate.Container = container;
        splineInstantiate.itemsToInstantiate = edibles;
        splineInstantiate.Clear();
        
        Invoke("Spawn",10);
    }

    private void Spawn()
    {
        splineInstantiate.UpdateInstances();
    }
}
