using UnityEngine;
using UnityEngine.Splines;

public class Track : MonoBehaviour
{
    [SerializeField]
    private SplineExtrude splineExtrude;

    private float length;

    public SplineExtrude SplineExtrude => this.splineExtrude;
    
    public void Initialize()
    {
        this.length = this.splineExtrude.Container.CalculateLength();
    }
    
    public float DeltaSpeedToProgress(float distance)
    {
        return distance / this.length;
    }
}
