using UnityEngine;

public interface IPickUp
{
    void Consume();

    void OnTriggerEnter(Collider other);
}
