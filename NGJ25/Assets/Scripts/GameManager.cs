using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private PickupInstancer pickupGenerator;
    
    [SerializeField]
    private EnvironmentSetter environmentSetter;
    
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
    public Material SquirtMat;

    public GameObject winOverlay;

    private bool canReset = false;
    
    private void Awake()
    {
        this.localTrack = FindFirstObjectByType<Track>();
        
        this.localTrack.Initialize();
        
        this.localPlayer.Initialize(this.gameCamera);
        
        var localCar = Instantiate(this.carPrefab);
        
        localCar.Initialize(this.localTrack);

        this.localPlayer.SetCar(localCar);

        this.pickupGenerator = FindFirstObjectByType<PickupInstancer>();
        
        this.environmentSetter = FindFirstObjectByType<EnvironmentSetter>();
        
        this.pickupGenerator.Initialize(localTrack.SplineExtrude);
        
//        checkpointSpline.UpdateInstances();
        this.cpInstancer.Initialize(localTrack.SplineExtrude,this);
        this.checkpointTracker.Initialize(localCar.transform, this);
        
        
        InitializeRaceType();
        
        
    }

    public void OnCheckpointhit()
    {
        gameCamera.SpeedVolumeOn();
        
        currentCheckpoint += 1;
        bool resetLap = false;
        if (currentCheckpoint % 3 == 0)
        {
            currentLap += 1;
            resetLap = true;
        }

        if (currentLap == Laps - 1)
        {
            gameCamera.SpawnMessage("Final Lap");
            environmentSetter.isSafe = true;
            FinishLine.SetActive(true);
        }
         
        Debug.Log("Checkpoint" + currentCheckpoint);
        Debug.Log("Lap" + currentLap);
        
        gameCamera.UpdateLaps((currentLap+1)+ "/"+Laps,resetLap);

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

    public void OnSquirt()
    {
        SquirtMat.SetFloat("_VignettePower",2);
        SquirtMat.SetFloat("_VignetteIntensity",10);
    }

    public void OnResetSquirt()
    {
        SquirtMat.SetFloat("_VignettePower",0);
        SquirtMat.SetFloat("_VignetteIntensity",0);
    }

    public void RaceWon()
    {
        gameCamera.SpawnMessage("Game Over");
        gameCamera.RaceOver();
        
        StartCoroutine(ResetCo());
    }
        
            IEnumerator ResetCo()
            {
                winOverlay.SetActive(true);

                yield return new WaitForSeconds(1.5f);
                
                canReset = true;
            }
    

    public void PickUpMessage(string itemName)
    {
        gameCamera.SpawnMessage(itemName +"!!!");
        gameCamera.SpeedVolumeOn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10) || (canReset && Input.anyKeyDown))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void InitializeRaceType()
    {
        this.OnResetSquirt();
        
        switch (raceType)
        {
            case RaceType.Laps:
                currentLap = 0;
                currentCheckpoint = 0;
                gameCamera.UpdateLaps((currentLap+1)+ "/"+Laps,true);
                gameCamera.SpawnMessage("Lets GOOO!!!!");
                break;
            case RaceType.Sprint:
                break;
            case RaceType.Elimination:
                break;
            
        }
        
    }
}
