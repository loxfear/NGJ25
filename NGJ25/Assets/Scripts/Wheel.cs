using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private bool steerable;
    
    [SerializeField]
    private bool motorized;
    
    [SerializeField]
    private WheelCollider wheelCollider;
    
    [SerializeField]
    private Transform model;
    
    public bool Steerable => this.steerable;
    
    public bool Motorized => this.motorized;
    
    public WheelCollider WheelCollider => this.wheelCollider;

    public void UpdateVisuals()
    {
        WheelCollider.GetWorldPose(out var position, out var rotation);
        this.model.position = position;
        this.model.rotation = rotation;    
    }
}
