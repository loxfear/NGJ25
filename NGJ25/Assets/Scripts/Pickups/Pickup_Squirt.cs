using UnityEngine;

public class Pickup_Squirt : MonoBehaviour,IPickUp
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Consume()
    {
        Debug.Log(" Pickup_Squirt Consumed");
    }

    public void OnCollisionEnter(Collision collision)
    {
        
    }
}
