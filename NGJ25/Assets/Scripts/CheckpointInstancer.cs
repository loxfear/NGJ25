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
    
    [SerializeField]
    private GameObject finishLinePrefab;
    
    private float length;
    
    [SerializeField]
    float m_Interpolation = .5f;

    

    void Start()
    {
        
    }

    [ContextMenu("TEST INIT")]
    public void Initialize(SplineExtrude extrude, GameManager gameManager)
    {
        splineExtrude = extrude;
        SetOnSpline(0.33f,checkpointPrefab);
        SetOnSpline(0.66f,checkpointPrefab);
        gameManager.FinishLine = SetOnSplineGO(0,finishLinePrefab);
    }
    
    private void SetOnSpline(float value,GameObject prefab)
    {
        
        Vector3 position = this.SplineExtrude.Container.EvaluatePosition(value);
        Quaternion rotation = Quaternion.LookRotation(SplineExtrude.Container.EvaluateTangent(value), SplineExtrude.Container.EvaluateUpVector(value));
        var Clone = Instantiate(prefab, position, rotation, this.transform);
        Clone.transform.position = Clone.transform.TransformPoint(SplineExtrude.Container.EvaluateUpVector(value) * 0.25f);
        
    }
    private GameObject SetOnSplineGO(float value,GameObject prefab)
    {
        
        Vector3 position = this.SplineExtrude.Container.EvaluatePosition(value);
        Quaternion rotation = Quaternion.LookRotation(SplineExtrude.Container.EvaluateTangent(value), SplineExtrude.Container.EvaluateUpVector(value));
        var Clone = Instantiate(prefab, position, rotation);
        Clone.transform.position = Clone.transform.TransformPoint(SplineExtrude.Container.EvaluateUpVector(value) * 0.25f);
        return Clone;
    }
    
    public float DeltaSpeedToProgress(float distance)
    {
        return distance / this.length;
    }
}
