using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Swipe swipe;

    public bool isRolling = false;
    private bool isJumping = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        swipe = FindAnyObjectByType<Swipe>();

        if (swipe != null)
        {
            swipe.OnSwipeDown += Roll;
            swipe.OnSwipeUp += JumpAnim;
        }
    }

    void Roll()
    {
        if (IsBusy()) return;

        animator.SetTrigger("Roll");
        isRolling = true;

        GetComponent<PlayerController>().StartRollCollider();
    }

    void JumpAnim()
    {
        if (IsBusy()) return;

        Debug.Log("JUMP ANIM");
        animator.SetTrigger("Jump");
        isJumping = true;
    }

    public void EndRoll()
    {
        isRolling = false;

        GetComponent<PlayerController>().EndRollCollider();
    }

    public void EndJump()
    {
        Debug.Log("FIN JUMP");
        isJumping = false;
    }

    public bool isStumbling = false;

    public void Stumble()
    {
        if (IsBusy()) return;

        Debug.Log("ANIM STUMBLE");

        animator.SetTrigger("Stumble");
        isStumbling = true;
    }

    public void EndStumble()
    {
        Debug.Log("FIN STUMBLE");
        isStumbling = false;
    }

    private bool IsBusy()
    {
        return isRolling || isJumping || isStumbling || isDead;
    }

    public bool isDead = false;

    public IEnumerator Fall()
    {
        if (isDead) yield return null; 

        Debug.Log("ANIM FALL");

        isDead = true;

      
        isRolling = false;
        isJumping = false;
        isStumbling = false;

        
        animator.ResetTrigger("Roll");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Stumble");

       
        animator.SetTrigger("Fall");


        yield return new WaitForSeconds(2f);

        PlayerController.IsGameOver();
    }
}
