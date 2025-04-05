using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = FindFirstObjectByType<GameManager>();
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        _gameManager.RaceWon();
    }
}
