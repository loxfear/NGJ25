using Unity.Mathematics;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Splines;

public class CheckpointInstancer : MonoBehaviour
{
    [SerializeField]
    private SplineExtrude splineExtrude;
    
    public SplineExtrude SplineExtrude => this.splineExtrude;

    [SerializeField]
    private GameObject checkpointPrefab;
    
    private float length;
    
    [SerializeField]
    float m_Interpolation = .5f;

    

    void Start()
    {
        
    }

    [ContextMenu("TEST INIT")]
    public void Initialize(SplineExtrude extrude)
    {
        splineExtrude = extrude;
        SetOnSpline(0.33f);
        SetOnSpline(0.66f);
    }
    
    private void SetOnSpline(float value)
    {
        
        Vector3 position = this.SplineExtrude.Container.EvaluatePosition(value);
        Quaternion rotation = Quaternion.LookRotation(SplineExtrude.Container.EvaluateTangent(value), SplineExtrude.Container.EvaluateUpVector(value));
        var Clone = Instantiate(checkpointPrefab, position, rotation, this.transform);
        Clone.transform.position = Clone.transform.TransformPoint(SplineExtrude.Container.EvaluateUpVector(value) * 0.25f);
        
    }
    
    public float DeltaSpeedToProgress(float distance)
    {
        return distance / this.length;
    }
}
