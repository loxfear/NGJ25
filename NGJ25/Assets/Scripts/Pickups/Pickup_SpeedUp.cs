using UnityEngine;

public class Pickup_SpeedUp : MonoBehaviour,IPickUp
{
    private GameManager _manager;
    void Start()
    {
        _manager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Consume()
    {
        Debug.Log(" Pickup_SpeedUp Consumed");
    }

    public void OnCollisionEnter(Collision collision)
    {
        
    }
}
