using UnityEngine;

public class RotateBetweenAngles : MonoBehaviour
{
    [SerializeField] private Vector3 axis;
    [SerializeField] private float speed;
    [SerializeField] private float fromAngle;
    [SerializeField] private float toAngle;
    [SerializeField] private float stopTimeInBetween = 0f;
    [SerializeField] private bool ignoreTimescale;

    private Transform trans;
    private float currentAngle, minAngle, maxAngle;
    private bool rotatingForward;
    private float waitTime = 0f;
    private Quaternion initialRotation;
    
    void Awake()
    {
        trans = transform;
        initialRotation = trans.localRotation;

        minAngle = Mathf.Min(fromAngle, toAngle);
        maxAngle = Mathf.Max(fromAngle, toAngle);
        rotatingForward = fromAngle < toAngle;
    }

    void Update()
    {
        float elapsedTime = ignoreTimescale ? Time.unscaledDeltaTime : Time.deltaTime;
        if (waitTime > 0f)
        {
            waitTime -= elapsedTime;
            return;
        }

        currentAngle = rotatingForward ? currentAngle + speed * elapsedTime : currentAngle - speed * elapsedTime;

        if (currentAngle > maxAngle)
        {
            rotatingForward = false;
            currentAngle = maxAngle;
            waitTime = stopTimeInBetween;
        }
        else if (currentAngle < minAngle)
        {
            rotatingForward = true;
            currentAngle = minAngle;
            waitTime = stopTimeInBetween;
        }

        Vector3 transformedAxis = axis;

        Quaternion targetRotation = Quaternion.AngleAxis(currentAngle, transformedAxis);

        trans.localRotation = initialRotation * targetRotation;
    }
}
