using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Car currentCar;

    private void Initialize(Car car)
    {
        this.currentCar = car;
    }
}
