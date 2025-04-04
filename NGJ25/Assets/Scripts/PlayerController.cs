using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameCamera gameCamera;
    
    private Car currentCar;

    public void Initialize(GameCamera gameCamera)
    {
        this.gameCamera = gameCamera;
    }

    public void SetCar(Car car)
    {
        this.currentCar = car;

        this.gameCamera.FollowCar(car);
    }
}
