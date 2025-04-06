using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Transform car;
    [SerializeField] private Transform[] wheels;
    public Vector3 rotationAxis = new Vector3(0f, 1f, 0f); // Y-axis by default
    public float rotationSpeed = 90f;
    public float maxCarRotationAngle = 25f;
    [SerializeField] private RectTransform logo;

    private float initialEulerY;
    
    private PlayerControls playerControls;
    private bool canStartGame;

    private void Start()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        
        canStartGame = false;
        initialEulerY = car.localEulerAngles.y;
        StartCoroutine(LogoEntranceCo());
    }

    void Update()
    {
        float movementX = playerControls.Player.Move.ReadValue<Vector2>().x;
        if (movementX != 0f)
        {
            float rotationDir = movementX > 0f ? 1f: -1f;
            
            float desiredRotation = initialEulerY + rotationDir * maxCarRotationAngle;
            float targetYRotation = Mathf.MoveTowardsAngle(
                car.localEulerAngles.y,
                desiredRotation,
                rotationSpeed * Time.deltaTime
            );

            Vector3 newEuler = car.localEulerAngles;
            newEuler.y = targetYRotation;
            car.localEulerAngles = newEuler;
            
            foreach (var w in wheels)
            {
                Quaternion currentRotation = w.localRotation;

                // Target local rotation
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, rotationDir * 15f, 0f));

                // Smoothly rotate towards the target
                transform.localRotation = Quaternion.Lerp(
                    currentRotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            return;
        }

        if (Input.anyKey && canStartGame)
            SceneManager.LoadScene(1);
    }

    IEnumerator LogoEntranceCo()
    {
        float initialX = logo.position.x;
        float outOfScreenX = -500f;
        logo.position = new Vector3(outOfScreenX, logo.position.y, logo.position.z);

        yield return new WaitForSeconds(0.5f);
        
        float initialTime = Time.time;
        while (Time.time < initialTime + 2f)
        {
            float t = (Time.time - initialTime) / 2f;
            float e = Easing.OutBounce(t);

            logo.position = new Vector3(Mathf.Lerp(outOfScreenX, initialX, e), logo.position.y, logo.position.z);
            yield return null;
        }

        canStartGame = true;
    }
}
