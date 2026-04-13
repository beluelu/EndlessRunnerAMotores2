using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PauseScreen screen;

    [Header("Movimiento")]
    public float forwardSpeed = 10f;

    [Header("Carriles")]
    public float laneDistance = 2f; // distancia entre carriles
    public float laneChangeSpeed = 10f;

    private int currentLane = 1; // 0 = izquierda, 1 = centro, 2 = derecha

    void Update()
    {
        MoveForward();
        HandleInput();
        MoveToLane();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
    }

    void HandleInput()
    {
        // PC (para testear)
        if (Input.GetKeyDown(KeyCode.A))
            currentLane--;

        if (Input.GetKeyDown(KeyCode.D))
            currentLane++;

        // Mobile (swipe básico)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (touch.deltaPosition.x > 50)
                    currentLane++;
                else if (touch.deltaPosition.x < -50)
                    currentLane--;
            }
        }

        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    void MoveToLane()
    {
        Vector3 targetPosition = new Vector3(
            (currentLane - 1) * laneDistance,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            laneChangeSpeed * Time.deltaTime
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            screen.GameOver();
            Debug.Log("GAME OVER");
            Time.timeScale = 0f;
        }
    }
}