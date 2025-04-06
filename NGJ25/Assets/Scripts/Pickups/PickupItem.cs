using System;
using Sirenix.OdinInspector;
using UnityEngine;



public class PickupItem : MonoBehaviour,IPickUp
{
    [SerializeField]
    private Pickup_Type Type;

    private Collider _other;
    
    [ShowIf("Type", Pickup_Type.Enlarge)]
    public float enlargedSize = 2.0f;
    [ShowIf("Type", Pickup_Type.Shrink)]
    public float reducedSize = 0.75f;
    
    [SerializeField]
    private float resetTime = 5.0f;

    [SerializeField] 
    private GameObject particleSystem;
    
    [SerializeField] 
    private GameObject mesh;

    [SerializeField] 
    private float fuel = 20f;

    private GameManager _gameManager;

    private bool isConsumed;
    
    private void Start()
    {
        particleSystem.SetActive(false);
        _gameManager = FindFirstObjectByType<GameManager>();
    }

    public void Consume()
    {
        if (this.isConsumed)
        {
            return;
        }
        else
        {
            this.isConsumed = true;
        }
        
        switch (Type)
        {
            case Pickup_Type.SpeedUp:
                break;
            case Pickup_Type.SlowDown:
                break;
            case Pickup_Type.Enlarge:
                _other.transform.localScale = Vector3.one * enlargedSize;
                break;
            case Pickup_Type.Shrink:
                _other.transform.localScale = Vector3.one * reducedSize;
                break;
            case Pickup_Type.Spill:
                break;
            case Pickup_Type.Squirt:
                _gameManager.OnSquirt();
                break;
        }

        var car = _other.GetComponent<Car>();

        if (car != null)
        {
            car.AddFuel(this.fuel);
        }
        
        Debug.Log(Type +" Consumed");
        mesh.SetActive(false);
        particleSystem.SetActive(true);
        _gameManager.PickUpMessage(Type.ToString());
        Invoke("Reset",resetTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        _other = other;
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
                _other.transform.localScale = Vector3.one;
                break;
            case Pickup_Type.Spill:
                break;
            case Pickup_Type.Squirt:
                _gameManager.OnResetSquirt();
                break;
        }

        mesh.SetActive(true);
        particleSystem.SetActive(false);
        this.isConsumed = false;
    }
}
