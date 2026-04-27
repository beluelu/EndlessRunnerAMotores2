using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int currentLane = 1;

    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    private bool isChangingLane = false;

    private float verticalVelocity = 0f;
    public float jumpForce = 7f;
    public float gravity = -20f;

    private bool isGrounded = true;

    private PlayerAnimation playerAnim;

    public static Action IsGameOver;

    void Start()
    {
        playerAnim = GetComponent<PlayerAnimation>();
        Swipe swipe = FindObjectOfType<Swipe>();

        if (swipe != null)
        {
            swipe.OnSwipeLeft += MoveLeft;
            swipe.OnSwipeRight += MoveRight;
            swipe.OnSwipeUp += Jump;
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

        // -------- POSICIÓN OBJETIVO --------
        float targetX = 0;

        if (currentLane == 0) targetX = -laneDistance;
        else if (currentLane == 2) targetX = laneDistance;

        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

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

        // -------- MOVIMIENTO --------
        transform.position = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);

        // 🔥 DESBLOQUEAR CAMBIO DE CARRIL
        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            isChangingLane = false;
        }
    }

    public void MoveLeft()
    {
        if (isChangingLane) return;

        if (currentLane > 0)
        {
            currentLane--;
            isChangingLane = true;
        }
    }

    public void MoveRight()
    {
        if (isChangingLane) return;

        if (currentLane < 2)
        {
            currentLane++;
            isChangingLane = true;
        }
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
            StartCoroutine(playerAnim.Fall());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SmallObstacle"))
        {
            TriggerStumble();
        }

        if (other.CompareTag("BigObstacle"))
        {
            TriggerFall();
        }
    }

    void TriggerStumble()
    {
        if (playerAnim != null && playerAnim.isStumbling) return;

        playerAnim.Stumble();
    }
}
