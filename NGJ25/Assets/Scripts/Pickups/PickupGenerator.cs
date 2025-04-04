using System.Collections.Generic;
using Coherence.Toolkit;
using UnityEngine;
using UnityEngine.Splines;

public class PickupGenerator : MonoBehaviour
{
    [SerializeField]
    private SplineExtrude splineExtrude;
    
    public SplineExtrude SplineExtrude => this.splineExtrude;

    private float length;

    [SerializeField]
    private List<GameObject> powerUps;

    public void Initialize(SplineExtrude extrude)
    {
        splineExtrude = extrude;
        this.length = this.splineExtrude.Container.CalculateLength();
        Spawn();
    }

    private void Spawn()
    {
        Debug.Log(length);
    }
}
