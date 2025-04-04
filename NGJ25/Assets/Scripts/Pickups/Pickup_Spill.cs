using UnityEngine;

public class Pickup_Spill : MonoBehaviour,IPickUp
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
        Debug.Log(" Pickup_Spill Consumed");
    }

    public void OnCollisionEnter(Collision collision)
    {
        
    }
}
