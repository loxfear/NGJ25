using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingPathMover : MonoBehaviour
{
    public List<Transform> pathPoints; // Add your waypoints in the Inspector
    public float moveSpeed = 5f;       // Movement speed in units per second
    public float rotateSpeed = 360f;   // Rotation speed in degrees per second
    public bool smoothMovement = true; // If false, uses constant speed

    private int currentTargetIndex = 0;

    void Start()
    {
        if (pathPoints == null || pathPoints.Count < 2)
        {
            Debug.LogWarning("Please assign at least 2 path points.");
            enabled = false;
            return;
        }

        // Start at the first point
        transform.position = pathPoints[0].position;
        StartCoroutine(MoveThroughPath());
    }

    IEnumerator MoveThroughPath()
    {
        while (true)
        {
            Transform target = pathPoints[currentTargetIndex];

            while (Vector3.Distance(transform.position, target.position) > 0.01f)
            {
                // Move
                Vector3 direction = (target.position - transform.position).normalized;

                if (smoothMovement)
                {
                    // Smooth position movement with interpolation
                    transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                }
                else
                {
                    // Direct move
                    transform.position += direction * moveSpeed * Time.deltaTime;
                }

                // Optional: Rotate towards the direction of movement
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                }

                yield return null;
            }

            // Snap to target and advance to the next point
            transform.position = target.position;
            currentTargetIndex = (currentTargetIndex + 1) % pathPoints.Count; // Loop back to 0
        }
    }
}