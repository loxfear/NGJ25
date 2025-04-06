using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class PickupInstancer : MonoBehaviour
{
    [SerializeField]
    private SplineExtrude splineExtrude;
    
    public SplineExtrude SplineExtrude => this.splineExtrude;

    [SerializeField] 
    private List<GameObject> pickups;
    
    private float length;
    
    [SerializeField]
    float m_Interpolation = .5f;
    
    [ContextMenu("TEST")]
    public void Initialize(SplineExtrude extrude)
    {
        splineExtrude = extrude;
        for (float i = 1; i < 10; i++)
        {
            if(i/10==0.1f)
                continue;
            SetOnSpline(i/10,pickups[Random.Range(0, pickups.Count)]);    
        }
    }
    
    private void SetOnSpline(float value,GameObject prefab)
    {
        
        Vector3 position = this.SplineExtrude.Container.EvaluatePosition(value);
        Quaternion rotation = Quaternion.LookRotation(SplineExtrude.Container.EvaluateTangent(value), SplineExtrude.Container.EvaluateUpVector(value));
        var Clone = Instantiate(prefab, position+ new Vector3(Random.Range(-10,10),0,0), rotation, this.transform);
        Clone.transform.position = Clone.transform.TransformPoint(SplineExtrude.Container.EvaluateUpVector(value) * 0.25f);
        
    }
}
