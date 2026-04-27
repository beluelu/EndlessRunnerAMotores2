using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;

    public float minSwipeDistance = 50f;

    private bool canSwipe = true;

    public System.Action OnSwipeLeft;
    public System.Action OnSwipeRight;
    public System.Action OnSwipeUp;
    public System.Action OnSwipeDown;

    void Update()
    {
        // MOBILE
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                canSwipe = true;
            }

            if (touch.phase == TouchPhase.Ended && canSwipe)
            {
                endPos = touch.position;
                DetectSwipe();
                canSwipe = false;
            }
        }

        // PC (para testear)
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            canSwipe = true;
        }

        if (Input.GetMouseButtonUp(0) && canSwipe)
        {
            endPos = Input.mousePosition;
            DetectSwipe();
            canSwipe = false;
        }
    }

    void DetectSwipe()
    {
        Vector2 delta = endPos - startPos;

        if (delta.magnitude < minSwipeDistance) return;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            if (delta.x > 0)
                OnSwipeRight?.Invoke();
            else
                OnSwipeLeft?.Invoke();
        }
        else
        {
            if (delta.y > 0)
                OnSwipeUp?.Invoke();
            else
                OnSwipeDown?.Invoke();
        }
    }
}
