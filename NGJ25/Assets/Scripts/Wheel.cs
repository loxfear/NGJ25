using System;
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

    private float forwardStiffness;
    private float sidewaysStiffness;
    private float drift;

    public bool Steerable => this.steerable;
    
    public bool Motorized => this.motorized;

    public bool isDrifting => driftTime > 1f;
    
    private float driftTime = 0f;
    
    public WheelCollider WheelCollider => this.wheelCollider;

    private void Awake()
    {
        this.forwardStiffness = this.wheelCollider.forwardFriction.stiffness;
        this.sidewaysStiffness = this.wheelCollider.sidewaysFriction.stiffness;
        
        this.wheelCollider.ConfigureVehicleSubsteps(5f, 12, 5);
    }

    public void UpdateVisuals(Car car, Vector3 linearVelocity)
    {
        WheelCollider.GetWorldPose(out var position, out var rotation);
        this.model.position = position;
        this.model.rotation = rotation;

        var main = this.driftParticleSystem.main;
        var emission = this.driftParticleSystem.emission;
        
        if (this.wheelCollider.GetGroundHit(out var hit))
        {
            drift = (Vector3.Angle(hit.forwardDir, linearVelocity) / 90f) - this.smokeCutoff;

            if (drift < 1f)
                driftTime += Time.deltaTime;
            else
                driftTime = 0f;

            this.driftParticleSystem.transform.position = hit.point + hit.normal * 0.25f;
            
            main.startSpeed = linearVelocity.magnitude;
            emission.rateOverDistance = drift > 1 ? 0 : drift * this.smokeMult;
        }
        else
        {
            main.startSpeed = 0;
            emission.rateOverDistance = 0;
        }
    }

    public void SetStiffnessMult(float breaking)
    {
        if (this.motorized)
        {
            var sidewaysFriction = this.wheelCollider.sidewaysFriction;
            sidewaysFriction.stiffness = this.forwardStiffness * breaking;
            this.wheelCollider.sidewaysFriction = sidewaysFriction;
        }
    }
}
