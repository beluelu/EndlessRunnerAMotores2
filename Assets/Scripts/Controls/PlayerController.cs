using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int currentLane = 1;

    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    private bool isChangingLane = false;

    private float verticalVelocity = 0f;
    public float jumpForce;
    public float gravity;

    public bool isGrounded = true;
    private CapsuleCollider playerCollider;

    private float originalHeight;
    private Vector3 originalCenter;

    private PlayerAnimation playerAnim;

    public static Action IsGameOver;


    [Header("Collision Layers")]
    public LayerMask smallObstacleLayer;
    public LayerMask bigObstacleLayer;

    void Start()
    {
        playerAnim = GetComponent<PlayerAnimation>();


        if (RemoteConfigManager.Instance != null)
        {
            jumpForce = RemoteConfigManager.Instance.jumpForce;
            gravity = RemoteConfigManager.Instance.gravity;
        }

        Swipe swipe = FindAnyObjectByType<Swipe>();

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

        playerCollider = GetComponent<CapsuleCollider>();

        originalHeight = playerCollider.height;
        originalCenter = playerCollider.center;
    }

    public void StartRollCollider()
    {
        playerCollider.height = originalHeight / 2f;
        playerCollider.center = new Vector3(0, originalCenter.y / 2f, 0);
    }

    public void EndRollCollider()
    {
        playerCollider.height = originalHeight;
        playerCollider.center = originalCenter;
    }

    void Update()
    {
        if (playerAnim != null && playerAnim.isDead) return;
        

        float targetX = 0;

        if (currentLane == 0) targetX = -laneDistance;
        else if (currentLane == 2) targetX = laneDistance;

        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);

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

        transform.position = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);

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
        PlayerStats stats = GetComponent<PlayerStats>();
        int otherLayer = other.gameObject.layer;

        
        if (((1 << otherLayer) & smallObstacleLayer) != 0)
        {
            if (isGrounded)
            {
                if (stats != null)
                {
                    stats.TakeDamage(1);

                    
                    if (stats.currentLives <= 0)
                    {
                        Section.isGameOver = true;
                    }
                }

                TriggerStumble();
            }
        }
        
        else if (((1 << otherLayer) & bigObstacleLayer) != 0)
        {
            if (!playerAnim.isRolling)
            {
                if (stats != null)
                    stats.TakeDamage(999);

                
                Section.isGameOver = true;
            }
        }
    }

    void TriggerStumble()
    {
        if (playerAnim != null && playerAnim.isStumbling) return;
        playerAnim.Stumble();
    }
}
