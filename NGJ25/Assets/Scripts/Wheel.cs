using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private bool steerable;
    
    [SerializeField]
    private bool motorized;
    
    [SerializeField]
    private WheelCollider wheelCollider;
    
    public bool Steerable => this.steerable;
    
    public bool Motorized => this.motorized;
    
    public WheelCollider WheelCollider => this.wheelCollider;
}
