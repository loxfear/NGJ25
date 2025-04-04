using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameCamera gameCamera;
    private Car currentCar;
    private PlayerControls playerControls;

    public void Initialize(GameCamera gameCamera)
    {
        this.playerControls = new PlayerControls();
        this.playerControls.Enable();
        this.gameCamera = gameCamera;
    }

    public void SetCar(Car car)
    {
        this.currentCar = car;

        this.gameCamera.FollowCar(car);
        
        this.currentCar.SetControls(playerControls);
    }
}
