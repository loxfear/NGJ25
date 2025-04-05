using System;
using Coherence.Toolkit;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Car : MonoBehaviour
{
    [SerializeField] 
    public float motorTorque = 2000f;
    
    [SerializeField] 
    public float brakeTorque = 2000f;    
    
    [SerializeField] 
    private float maxSpeed = 120f;
    
    [SerializeField] 
    public float steeringRange = 30f;
    
    [SerializeField] 
    public float steeringRangeAtMaxSpeed = 10f;
    
    [SerializeField] 
    public float centreOfGravityOffset = -1f;    
    
    [SerializeField] 
    private float handBreakMult = 0.5f;

    [SerializeField] 
    private Transform cameraPoint;

    [SerializeField] 
    private Rigidbody currentRigidbody;

    [SerializeField] 
    private Wheel[] wheels;

    public Transform CameraPoint => this.cameraPoint;
    
    public float CurrentSpeed { get; private set; }

    private Track currentTrack;

    private PlayerController playerController;
    
    private PlayerControls playerControls;

    public void Initialize(Track track)
    {
        this.currentTrack = track;
        
        var centerOfMass = this.currentRigidbody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        this.currentRigidbody.centerOfMass = centerOfMass;

        this.transform.SetParent(track.SplineExtrude.transform);

        this.SetOnSpline(0);
    }

    private void SetOnSpline(float value)
    {
        this.transform.position = this.currentTrack.SplineExtrude.Container.EvaluatePosition(value);
        this.transform.rotation = quaternion.LookRotation(this.currentTrack.SplineExtrude.Container.EvaluateTangent(value), this.currentTrack.SplineExtrude.Container.EvaluateUpVector(value));
        this.transform.position = this.transform.TransformPoint(this.currentTrack.SplineExtrude.Container.EvaluateUpVector(value) * 0.25f);
    }
    
    public void SetControls(PlayerControls playerControls)
    {
        this.playerControls = playerControls;
    }

    void FixedUpdate()
    {
        var movement = Vector2.zero;

        var breaking = 0f;

        if (this.playerControls != null)
        {
            movement = this.playerControls.Player.Move.ReadValue<Vector2>();
            breaking = this.playerControls.Player.Jump.ReadValue<float>();
        }

        foreach (var wheel in this.wheels)
        {
            wheel.SetStiffnessMult(1f - breaking * this.handBreakMult);
        }
        
        if (this.currentTrack != null)
        {
            var vInput = movement.y;
            var hInput = movement.x;

            var forwardSpeed = Vector3.Dot(transform.forward, this.currentRigidbody.linearVelocity);
            var speedFactor = Mathf.InverseLerp(0, this.maxSpeed, Mathf.Abs(forwardSpeed));

            this.CurrentSpeed = forwardSpeed / this.maxSpeed;
            
            var currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
            var currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

            bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

            foreach (var wheel in wheels)
            {
                wheel.UpdateVisuals(this, this.currentRigidbody.linearVelocity);
                
                if (wheel.Steerable)
                {
                    wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
                }

                if (isAccelerating)
                {
                    if (wheel.Motorized)
                    {
                        wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                    }
                    wheel.WheelCollider.brakeTorque = 0f;
                }
                else
                {
                    wheel.WheelCollider.motorTorque = 0f;
                    wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                }
            }
        }
    }
}
