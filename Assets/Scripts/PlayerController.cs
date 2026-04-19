using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PauseScreen screen;
    //private Rigidbody _rb;

    [Header("Movimiento")]
    public float forwardSpeed = 10f;
    public float JumpForce = 5f;

    [Header("Carriles")]
    public float laneDistance = 2.5f; // distancia entre carriles
    public float laneChangeSpeed = 10f;

    private int currentLane = 1; // 0 = izquierda, 1 = centro, 2 = derecha

    private void Awake()
    {
        //_rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        HandleInput();
        MoveToLane();
    }

    void HandleInput()
    {
        // PC (para testear)
        if (Input.GetKeyDown(KeyCode.A))
            currentLane--;

        if (Input.GetKeyDown(KeyCode.D))
            currentLane++;

        if (Input.GetKeyDown(KeyCode.W))
            Jump();

        // Mobile (swipe básico)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (touch.deltaPosition.x > 30)
                    currentLane++;
                else if (touch.deltaPosition.x < -30)
                    currentLane--;
                else if (touch.deltaPosition.y < 30)
                    Jump();
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

    void Jump()
    {
        //me rindo :(
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