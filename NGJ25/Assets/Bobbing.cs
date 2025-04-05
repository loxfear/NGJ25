using UnityEngine;

public class Bobbing : MonoBehaviour
{
    [SerializeField] private float bobbingSpeed = 0.5f;
    [SerializeField] private float bobbingHeight = 0.5f;
    [SerializeField] private float bobbingOffset = 0.5f;
    
    private Vector3 startPosition;
    private float time;
    
    private void Start()
    {
        startPosition = transform.localPosition;
    }
    
    private void Update()
    {
        time += Time.deltaTime * bobbingSpeed;
        float newY = startPosition.y + Mathf.Sin(time) * bobbingHeight + bobbingOffset;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }
    
    private void OnEnable()
    {
        time = 0f; // Reset time when the object is enabled
    }
    
}
