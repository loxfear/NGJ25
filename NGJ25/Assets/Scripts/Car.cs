using System;
using Coherence.Toolkit;
using Unity.Mathematics;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] 
    private float speed = 1f;

    [SerializeField] 
    private Transform cameraPoint;

    public Transform CameraPoint => this.cameraPoint;
    
    private Track currentTrack;

    private PlayerController playerController;

    private float position;
    private PlayerControls playerControls;

    public void Initialize(Track track)
    {
        this.currentTrack = track;
        this.currentTrack.Initialize();
        
        this.transform.SetParent(track.SplineExtrude.transform);
    }

    private void Update()
    {
        var movement = Vector2.zero;

        if (this.playerControls != null)
        {
            movement = this.playerControls.Player.Move.ReadValue<Vector2>();
        }
        
        Debug.LogError(movement);
        
        if (this.currentTrack != null)
        {
            this.transform.position = this.currentTrack.SplineExtrude.Container.EvaluatePosition(this.position);
            this.transform.rotation = quaternion.LookRotation(this.currentTrack.SplineExtrude.Container.EvaluateTangent(this.position), this.currentTrack.SplineExtrude.Container.EvaluateUpVector(this.position));
            
            var deltaProgress = this.currentTrack.DeltaSpeedToProgress((this.speed / 3.6f) * Time.deltaTime * movement.y);

            this.position += deltaProgress;

            if (this.position > 1f)
            {
                this.position -= 1f;
            }
        }
    }

    public void SetControls(PlayerControls playerControls)
    {
        this.playerControls = playerControls;
    }
}
