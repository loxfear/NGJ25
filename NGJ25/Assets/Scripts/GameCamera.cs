using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private float lerpPositionMult = 0.5f;

    [SerializeField]
    private float lerpRotationMult = 0.5f;

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
        if (this.car.CameraPoint != null)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.car.CameraPoint.position, lerpPositionMult);
            this.transform.rotation = quaternion.LookRotation(
                Vector3.Lerp(this.transform.forward, this.car.CameraPoint.forward, lerpRotationMult),
                Vector3.Lerp(this.transform.up, this.car.CameraPoint.up, lerpRotationMult));

            var distance = Vector3.Distance(this.transform.position, this.car.CameraPoint.position);

            this.mainCamera.fieldOfView = Mathf.Max(this.startFov * distance, this.startFov);
        }
    }
}
