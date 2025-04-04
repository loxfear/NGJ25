using System;
using Coherence.Toolkit;
using Unity.Mathematics;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] 
    private float speed = 1f; 
    
    private Track currentTrack;

    private PlayerController playerController;

    private float position;
    
    public void Initialize(Track track, PlayerController playerController)
    {
        this.currentTrack = track;
        this.playerController = playerController;
        this.currentTrack.Initialize();
        
        this.transform.SetParent(track.SplineExtrude.transform);
    }

    private void Update()
    {
        if (this.currentTrack != null)
        {
            this.transform.position = this.currentTrack.SplineExtrude.Container.EvaluatePosition(this.position);
            this.transform.rotation = quaternion.LookRotation(this.currentTrack.SplineExtrude.Container.EvaluateTangent(this.position), this.currentTrack.SplineExtrude.Container.EvaluateUpVector(this.position));

            var deltaProgress = this.currentTrack.DeltaSpeedToProgress((this.speed / 3.6f) * Time.deltaTime);

            this.position += deltaProgress;

            if (this.position > 1f)
            {
                this.position -= 1f;
            }
        }
    }
}
