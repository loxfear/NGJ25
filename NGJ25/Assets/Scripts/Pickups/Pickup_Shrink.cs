using UnityEngine;

public class Pickup_Shrink : MonoBehaviour,IPickUp
{
    private GameManager _manager;
    
    [SerializeField]
    private float reducedSize = 0.75f;
    [SerializeField]
    private float resetTime = 5.0f;

    private Collision _collided;
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
        _collided.transform.localScale = Vector3.one * reducedSize;
        Invoke("Reset",resetTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        _collided = collision;
        Consume();
    }
    private void Reset()
    {
        _collided.transform.localScale = Vector3.one;
    }
}
