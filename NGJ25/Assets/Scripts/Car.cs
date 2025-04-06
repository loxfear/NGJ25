using System;
using Coherence.Toolkit;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Car : MonoBehaviour
{
    [SerializeField] public float motorTorque = 2000f;

    [SerializeField] public float brakeTorque = 2000f;

    [SerializeField] private float maxSpeed = 120f;

    [SerializeField] public float steeringRange = 30f;

    [SerializeField] public float steeringRangeAtMaxSpeed = 10f;

    [SerializeField] public float centreOfGravityOffset = -1f;

    [SerializeField] private float handBreakMult = 0.5f;

    [SerializeField] private Transform cameraPoint;

    [SerializeField] private Rigidbody currentRigidbody;

    [SerializeField] private Wheel[] wheels;

    public float MaxSpeed => this.maxSpeed;

    public Transform CameraPoint => this.cameraPoint;

    public float CurrentSpeed { get; private set; }

    private Track currentTrack;

    private PlayerController playerController;

    private PlayerControls playerControls;

    private GameObject lastCP;

    public void Initialize(Track track)
    {
        this.currentTrack = track;

        var centerOfMass = this.currentRigidbody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        this.currentRigidbody.centerOfMass = centerOfMass;

        this.transform.SetParent(track.SplineExtrude.transform);

        this.SetOnSpline(0);
        
    }

    public void SetLastCheckpoint(GameObject CP)
    {
        lastCP = CP;
    }

    private void SetOnSpline(float value)
    {
        this.transform.position = this.currentTrack.SplineExtrude.Container.EvaluatePosition(value);
        this.transform.rotation =
            quaternion.LookRotation(this.currentTrack.SplineExtrude.Container.EvaluateTangent(value),
                this.currentTrack.SplineExtrude.Container.EvaluateUpVector(value));
        this.transform.position =
            this.transform.TransformPoint(this.currentTrack.SplineExtrude.Container.EvaluateUpVector(value) * 0.25f);

        this.currentRigidbody.position = this.transform.position;
        this.currentRigidbody.rotation = this.transform.rotation;
        this.currentRigidbody.angularVelocity = Vector3.zero;
        this.currentRigidbody.linearVelocity = Vector3.zero;
    }

    public void SetControls(PlayerControls playerControls)
    {
        this.playerControls = playerControls;
    }

    void FixedUpdate()
    {
        var movement = Vector2.zero;

        var breaking = 0f;
        var reset  = 0f;

        if (this.playerControls != null)
        {
            movement.y = this.playerControls.Player.Accelerate.ReadValue<float>() -
                         this.playerControls.Player.Break.ReadValue<float>();
            movement.x = this.playerControls.Player.Move.ReadValue<Vector2>().x;
            breaking = this.playerControls.Player.HandBreak.ReadValue<float>();
            reset = this.playerControls.Player.Reset.ReadValue<float>();

            if (reset != 0)
            {
                if(lastCP!=null)
                    this.transform.position = lastCP.transform.position + new Vector3(0, 1, 0);
                else
                {
                    this.SetOnSpline(0);
                    this.transform.position += new Vector3(0, 1, 0);
                }
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
}