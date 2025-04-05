using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    public Vector3 rotationAxis = new Vector3(0f, 1f, 0f); // Y-axis by default
    public float rotationSpeed = 90f; // Degrees per second

    void Update()
    {
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime);
    }
}