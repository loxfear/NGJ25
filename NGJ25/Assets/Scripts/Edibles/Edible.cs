using UnityEngine;

public class Edible : MonoBehaviour,IPickUp
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
        throw new System.NotImplementedException();
    }

    public void OnTriggerEnter(Collider other)
    {
        throw new System.NotImplementedException();
    }

    public void OnCollisionEnter(Collision collision)
    {
        throw new System.NotImplementedException();
    }
}
