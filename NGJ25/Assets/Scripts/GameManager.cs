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
    private int currentCheckpoint;

    public TrackCheckpoints checkpointTracker;
    public CheckpointInstancer cpInstancer;
    public GameObject FinishLine;
    
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
        this.cpInstancer.Initialize(localTrack.SplineExtrude,this);
        this.checkpointTracker.Initialize(localCar.transform, this);
        
        
        InitializeRaceType();
        
        
    }

    public void OnCheckpointhit()
    {
        currentCheckpoint += 1;
        bool resetLap = false;
        if (currentCheckpoint % 3 == 0)
        {
            currentLap += 1;
            resetLap = true;
        }
        if(currentLap == Laps-1)
            FinishLine.SetActive(true);
            
        
        Debug.Log("Checkpoint" + currentCheckpoint);
        Debug.Log("Lap" + currentLap);
        
        gameCamera.UpdateLaps(currentLap+ "/"+Laps,resetLap);
        
        //To Do when lap get complete 
    }
    private void GameStart()
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        
    }

    private void RaceStart(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void RaceLoss(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void RaceWon(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void RaceWon()
    {
        Debug.Log("Game Over");
    }

    

    public void InitializeRaceType()
    {
        switch (raceType)
        {
            case RaceType.Laps:
                currentLap = 0;
                currentCheckpoint = 0;
                gameCamera.UpdateLaps(currentLap+ "/"+Laps,true);
                break;
            case RaceType.Sprint:
                break;
            case RaceType.Elimination:
                break;
            
        }
        
    }
}
