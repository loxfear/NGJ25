using UnityEngine;

public class LocalBobUpDown : MonoBehaviour
{
    public float bobDistance = 0.5f;     // How far it bobs up/down
    public float bobSpeed = 2f;          // How fast it bobs

    private Vector3 initialLocalPos;

    void Start()
    {
        initialLocalPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * bobSpeed) * bobDistance;
        // Bob along local up axis
        transform.localPosition = initialLocalPos + transform.up * offset;
    }
}