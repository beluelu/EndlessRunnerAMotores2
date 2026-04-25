using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int currentLane = 1;

    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    private float verticalVelocity = 0f;
    public float jumpForce = 7f;
    public float gravity = -20f;

    private bool isGrounded = true;

    private PlayerAnimation playerAnim;


    void Start()
    {
        playerAnim = GetComponent<PlayerAnimation>();
        Swipe swipe = FindObjectOfType<Swipe>();

        if (swipe != null)
        {
            swipe.OnSwipeLeft += MoveLeft;
            swipe.OnSwipeRight += MoveRight;
            swipe.OnSwipeUp += Jump;
            // ❌ swipe.OnSwipeDown += Crouch; ← ELIMINADO
        }
        else
        {
            Debug.LogError("No se encontró Swipe en la escena");
        }
    }

    void Update()
    {
        if (playerAnim != null && playerAnim.isDead) return;
        if (playerAnim != null && playerAnim.isStumbling) return;
        // -------- MOVIMIENTO LATERAL --------
        Vector3 targetPosition = new Vector3(0, transform.position.y, transform.position.z);

        if (currentLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (currentLane == 2)
            targetPosition += Vector3.right * laneDistance;

        // -------- SALTO --------
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        targetPosition.y += verticalVelocity * Time.deltaTime;

        if (targetPosition.y <= 0)
        {
            targetPosition.y = 0;
            verticalVelocity = 0;
            isGrounded = true;
        }

        // -------- APLICAR --------
        transform.position = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
    }

    public void MoveLeft()
    {
        if (currentLane > 0)
            currentLane--;
    }

    public void MoveRight()
    {
        if (currentLane < 2)
            currentLane++;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("SALTO");
            verticalVelocity = jumpForce;
            isGrounded = false;
        }
    }

    void TriggerFall()
    {
        if (playerAnim != null)
        {
            playerAnim.Fall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SmallObstacle"))
        {
            Debug.Log("Choque chico");
            TriggerStumble();
        }

        if (other.CompareTag("BigObstacle"))
        {
            Debug.Log("CHOQUE GRANDE");
            TriggerFall();
        }
    }

    void TriggerStumble()
    {
        if (playerAnim != null && playerAnim.isStumbling) return;

        Debug.Log("STUMBLE");

        playerAnim.Stumble();
    }
}
