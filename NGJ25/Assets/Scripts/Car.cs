using System;
using Coherence.Toolkit;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] 
    private float speed = 120f;
    
    [SerializeField] 
    private Vector2 acceleration = new Vector2(10f, 10f);

    [SerializeField] 
    private Transform cameraPoint;

    [SerializeField] 
    private Rigidbody currentRigidbody;

    [SerializeField] 
    private Wheel wheels;

    public Transform CameraPoint => this.cameraPoint;
    
    private Track currentTrack;

    private PlayerController playerController;

    private float position;
    private PlayerControls playerControls;
    
    private Vector3 offset;

    private Vector2 currentSpeed;

    public void Initialize(Track track)
    {
        this.currentTrack = track;
        this.currentTrack.Initialize();
        
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

    private void Update()
    {
        var movement = Vector2.zero;

        if (this.playerControls != null)
        {
            movement = this.playerControls.Player.Move.ReadValue<Vector2>();
        }
        
        if (this.currentTrack != null)
        {
                
        }
    }
}
