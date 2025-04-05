using System;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI speedUi;
    
    [SerializeField]
    private TextMeshProUGUI lapCountUi;
    
    [SerializeField]
    private TextMeshProUGUI placementUi;
    
    [SerializeField]
    private TextMeshProUGUI levelUi;
    
    [SerializeField]
    private float lerpPositionMult = 0.5f;

    [SerializeField]
    private float lerpRotationMult = 0.5f;

    [SerializeField]
    private float fovMult = 1.5f;
    
    [SerializeField]
    private float fovLerp = 0.5f;

    [SerializeField] 
    private Camera mainCamera;

    private Car car;

    private float startFov;

    private void Awake()
    {
        this.startFov = this.mainCamera.fieldOfView;
    }

    public void FollowCar(Car car)
    {
        this.car = car;
    }

    private void FixedUpdate()
    {
        if (this.car != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.car.CameraPoint.position, lerpPositionMult);
            this.transform.rotation = quaternion.LookRotation(
                Vector3.Lerp(this.transform.forward, this.car.CameraPoint.forward, lerpRotationMult),
                Vector3.Lerp(this.transform.up, this.car.CameraPoint.up, lerpRotationMult));

            this.mainCamera.fieldOfView = Mathf.Lerp(this.mainCamera.fieldOfView, Mathf.Max(this.startFov * this.car.CurrentSpeed * this.fovMult, this.startFov), this.fovLerp);
            
            this.speedUi.SetText((Mathf.Round(this.car.CurrentSpeed * this.car.MaxSpeed)).ToString());
        }
    }

    public void UpdateLaps(string laps)
    {
        lapCountUi.text = laps;
    }
}
