using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;

    public float minSwipeDistance = 10f;

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
                startPos = touch.position;

            if (touch.phase == TouchPhase.Ended)
            {
                endPos = touch.position;
                DetectSwipe();
            }
        }

        // PC (para testear)
        if (Input.GetMouseButtonDown(0))
            startPos = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            DetectSwipe();
        }
    }

    void DetectSwipe()
    {
        Vector2 delta = endPos - startPos;

        if (delta.magnitude < minSwipeDistance) return;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            // Horizontal
            if (delta.x > 0)
                OnSwipeRight?.Invoke();
            else
                OnSwipeLeft?.Invoke();
        }
        else
        {
            // Vertical
            if (delta.y > 0)
                OnSwipeUp?.Invoke();
            else
                OnSwipeDown?.Invoke();
        }
    }

}
