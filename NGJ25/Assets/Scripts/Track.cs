using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Splines;

public class Track : MonoBehaviour
{
    public float Width => this.splineExtrude.Radius;

    [SerializeField]
    private SplineExtrude splineExtrude;
    
    public SplineExtrude SplineExtrude => this.splineExtrude;

    private float length;

    public void Initialize()
    {
        this.length = this.splineExtrude.Container.CalculateLength();
    }
    
    public float DeltaSpeedToProgress(float distance)
    {
        return distance / this.length;
    }
}
