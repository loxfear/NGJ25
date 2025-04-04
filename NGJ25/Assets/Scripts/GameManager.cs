using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Car carPrefab;
    
    [SerializeField]
    private PlayerController playerControllerPrefab;

    private Track localTrack;
    
    private PlayerController localPlayer;
    
    private Car localCar;
    
    public Car LocalCar => (localCar);
    public PlayerController PlayerController => (PlayerController);
    
    private void Awake()
    {
        this.localTrack = FindFirstObjectByType<Track>();
        
        this.localTrack.Initialize();

        this.localPlayer = Instantiate(this.playerControllerPrefab);
        
        this.localCar = Instantiate(this.carPrefab);
        
        this.localCar.Initialize(this.localTrack, this.localPlayer);
    }
    
}
