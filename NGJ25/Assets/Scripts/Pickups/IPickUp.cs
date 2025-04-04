using UnityEngine;

public interface IPickUp
{
    void Consume();

    void OnCollisionEnter(Collision collision);
}
