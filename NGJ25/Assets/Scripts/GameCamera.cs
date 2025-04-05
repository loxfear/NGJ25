using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    private float lerpMult = 0.5f;
    
    private Transform targetTransform;

    public void FollowCar(Car car)
    {
        this.targetTransform = car.CameraPoint;
        this.transform.SetParent(car.transform);
    }

    private void LateUpdate()
    {
        if (this.targetTransform != null)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetTransform.position, this.lerpMult * Time.deltaTime);
            this.transform.rotation = quaternion.LookRotation(
                Vector3.MoveTowards(this.transform.forward, this.targetTransform.forward, this.lerpMult * Time.deltaTime),
                Vector3.MoveTowards(this.transform.up, this.targetTransform.up, this.lerpMult * Time.deltaTime));
        }
    }
}
