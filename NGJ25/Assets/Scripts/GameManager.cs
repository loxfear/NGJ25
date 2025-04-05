using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Car carPrefab;
    
    [SerializeField]
    private PlayerController localPlayer;
    
    [SerializeField]
    private GameCamera gameCamera;
    
    [SerializeField]
    private PickupGenerator pickupGenerator;

    private Track localTrack;
    
    private Car localCar;
    
    //Race Type Related
    
    [SerializeField]
    private RaceType raceType;
    
    [ShowIf("raceType", RaceType.Laps)]
    public int Laps = 2;

    private int currentLap;

    public TrackCheckpoints checkpointTracker;
    public CheckpointInstancer cpInstancer;
    
    private void Awake()
    {
        this.localTrack = FindFirstObjectByType<Track>();
        
        this.localTrack.Initialize();
        
        this.localPlayer.Initialize(this.gameCamera);
        
        var localCar = Instantiate(this.carPrefab);
        
        localCar.Initialize(this.localTrack);

        this.localPlayer.SetCar(localCar);

        this.pickupGenerator = FindFirstObjectByType<PickupGenerator>();
        
        this.pickupGenerator.Initialize(localTrack.SplineExtrude.Container);
        
//        checkpointSpline.UpdateInstances();
        this.cpInstancer.Initialize(localTrack.SplineExtrude);
        this.checkpointTracker.Initialize(localCar.transform);
        
    }
    
    public void OnRaceWon()
    {
        
    }
    
    public void OnRaceLoss()
    {
        
    }
    
    public void InitializeRaceType()
    {
        switch (raceType)
        {
            case RaceType.Laps:
                currentLap = 0;
                break;
            case RaceType.Sprint:
                break;
            case RaceType.Elimination:
                break;
            
        }
        
    }
}
