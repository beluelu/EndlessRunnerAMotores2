using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Blink Settings")]
    private Animator animator;
    private Swipe swipe;

    public bool isRolling = false;
    private bool isJumping = false;

    public GameObject characterModel;
    public float blinkDuration = 1.5f;
    public float blinkInterval = 0.1f;

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
        
        if (isStumbling) return;

        Debug.Log("ANIM STUMBLE");
        animator.SetTrigger("Stumble");
        isStumbling = true;
        
        StopCoroutine(Blink());
        StartCoroutine(Blink());

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

        StopCoroutine("Blink");

        if (characterModel != null)
        {
            Renderer[] renderers = characterModel.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers) if (r != null) r.enabled = true;
        }

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

    public IEnumerator Blink()
    {
        if (characterModel == null) yield break;

        Renderer[] renderers = characterModel.GetComponentsInChildren<Renderer>();
        float timer = 0;
        bool isVisible = false;

        while (timer < blinkDuration)
        {
            
            if (isDead)
            {
                foreach (Renderer r in renderers) if (r != null) r.enabled = true;
                yield break;
            }

            foreach (Renderer r in renderers)
            {
                if (r != null) r.enabled = isVisible;
            }

            isVisible = !isVisible;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        foreach (Renderer r in renderers) if (r != null) r.enabled = true;
    }
}
