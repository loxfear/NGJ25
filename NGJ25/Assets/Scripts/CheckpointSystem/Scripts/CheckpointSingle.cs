﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour {

    private TrackCheckpoints trackCheckpoints;
    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start() {
        //Hide();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Car>(out Car player)) {
            trackCheckpoints.CarThroughCheckpoint(this, other.transform);
            player.SetLastCheckpoint(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.TryGetComponent<Car>(out Car player)) {
            trackCheckpoints.CarThroughCheckpoint(this, collider.transform);
            player.SetLastCheckpoint(this.gameObject);
        }
    }

    public void SetTrackCheckpoints(TrackCheckpoints trackCheckpoints) {
        this.trackCheckpoints = trackCheckpoints;
    }

    public void Show() {
        meshRenderer.enabled = true;
    }

    public void Hide() {
        meshRenderer.enabled = false;
    }

}
