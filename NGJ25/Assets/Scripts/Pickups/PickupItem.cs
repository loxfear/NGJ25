using System;
using Sirenix.OdinInspector;
using UnityEngine;

public enum Pickup_Type
{
    SpeedUp,
    SlowDown,
    Enlarge,
    Shrink,
    Spill,
    Squirt
}

public class PickupItem : MonoBehaviour,IPickUp
{
    [SerializeField]
    private Pickup_Type Type;

    private Collision _collided;
    
    [ShowIf("Type", Pickup_Type.Enlarge)]
    public float enlargedSize = 2.0f;
    [ShowIf("Type", Pickup_Type.Shrink)]
    public float reducedSize = 0.75f;
    
    [SerializeField]
    private float resetTime = 5.0f;

    [SerializeField] 
    private GameObject particleSystem;

    private void Start()
    {
        particleSystem.SetActive(false);
    }

    public void Consume()
    {
        switch (Type)
        {
            case Pickup_Type.SpeedUp:
                break;
            case Pickup_Type.SlowDown:
                break;
            case Pickup_Type.Enlarge:
                _collided.transform.localScale = Vector3.one * enlargedSize;
                break;
            case Pickup_Type.Shrink:
                _collided.transform.localScale = Vector3.one * reducedSize;
                break;
            case Pickup_Type.Spill:
                break;
            case Pickup_Type.Squirt:
                break;
        }
        Debug.Log(Type +" Consumed");
        particleSystem.SetActive(true);
        Invoke("Reset",resetTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        _collided = collision;
        Consume();
    }
    private void Reset()
    {
        switch (Type)
        {
            case Pickup_Type.SpeedUp:
                break;
            case Pickup_Type.SlowDown:
                break;
            case Pickup_Type.Enlarge:
            case Pickup_Type.Shrink:
                _collided.transform.localScale = Vector3.one;
                break;
            case Pickup_Type.Spill:
                break;
            case Pickup_Type.Squirt:
                break;
        }
        particleSystem.SetActive(false);
    }
}
