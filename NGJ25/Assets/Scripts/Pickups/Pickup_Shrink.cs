using UnityEngine;

public class Pickup_Shrink : MonoBehaviour,IPickUp
{
    private GameManager _manager;
    
    [SerializeField]
    private float reducedSize = 0.75f;
    [SerializeField]
    private float resetTime = 5.0f;
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
        Debug.Log(" Pickup_Shrink Consumed");
        _manager.LocalCar.transform.localScale = Vector3.one * reducedSize;
        Invoke("Reset",resetTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        
    }
    private void Reset()
    {
        _manager.LocalCar.transform.localScale = Vector3.one;
    }
}
