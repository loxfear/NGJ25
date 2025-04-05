using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private float smokeMult = 5f;

    [SerializeField]
    private float smokeCutoff = 0.1f;

    [SerializeField]
    private bool steerable;
    
    [SerializeField]
    private bool motorized;
    
    [SerializeField]
    private WheelCollider wheelCollider;
    
    [SerializeField]
    private Transform model;

    [SerializeField] 
    private ParticleSystem driftParticleSystem;
    
    public bool Steerable => this.steerable;
    
    public bool Motorized => this.motorized;
    
    public WheelCollider WheelCollider => this.wheelCollider;

    public void UpdateVisuals(Car car, Vector3 linearVelocity)
    {
        WheelCollider.GetWorldPose(out var position, out var rotation);
        this.model.position = position;
        this.model.rotation = rotation;

        if (this.wheelCollider.GetGroundHit(out var hit))
        {
            var drift = (Vector3.Angle(hit.forwardDir, linearVelocity) / 90f) - this.smokeCutoff;

            this.driftParticleSystem.transform.position = hit.point + hit.normal * 0.25f;

            var main = this.driftParticleSystem.main;

            main.startSpeed = linearVelocity.magnitude;

            var emission = this.driftParticleSystem.emission;

            emission.rateOverDistance = drift * this.smokeMult;
        }
    }
}
