using UnityEngine;

public class ContinuousRotationLerp : MonoBehaviour
{
    public Vector3 startEulerAngles = new Vector3(0, 0, 0);
    public Vector3 endEulerAngles = new Vector3(0, 90, 0);
    public float lerpSpeed = 1f; // How fast it transitions

    private Quaternion startRotation;
    private Quaternion endRotation;

    void Start()
    {
        startRotation = Quaternion.Euler(startEulerAngles);
        endRotation = Quaternion.Euler(endEulerAngles);
    }

    void Update()
    {
        // t goes back and forth between 0 and 1
        float t = (Mathf.Sin(Time.time * lerpSpeed) + 1f) / 2f;

        // Lerp between the two rotations
        transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
    }
}
