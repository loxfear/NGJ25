using UnityEngine;

public class LookAtCameraOnAxis : MonoBehaviour
{
    public enum Axis { X, Y, Z }
    public Axis rotationAxis = Axis.Y;        // Axis to allow rotation on
    public float rotationSpeed = 5f;          // Lerp speed

    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void Update()
    {
        if (cam == null) return;

        Vector3 directionToCamera = cam.position - transform.position;

        // Flatten the direction based on the chosen axis
        switch (rotationAxis)
        {
            case Axis.X:
                directionToCamera.y = 0; // Only rotate around X
                directionToCamera.z = 0;
                break;
            case Axis.Y:
                directionToCamera.y = 0; // Only rotate around Y
                break;
            case Axis.Z:
                directionToCamera.x = 0; // Only rotate around Z
                directionToCamera.y = 0;
                break;
        }

        if (directionToCamera == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
