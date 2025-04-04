using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameCamera : MonoBehaviour
{
    private Transform targetTransform;

    public void FollowCar(Car car)
    {
        this.targetTransform = car.CameraPoint;
    }

    private void LateUpdate()
    {
        if (this.targetTransform != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetTransform.position, 0.5f);
            this.transform.rotation = quaternion.LookRotation(
                Vector3.MoveTowards(this.transform.forward, this.targetTransform.forward, 0.5f),
                Vector3.MoveTowards(this.transform.up, this.targetTransform.up, 0.5f));
        }
    }
}
