using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public bool SwipeLeft { get; private set; }
    public bool SwipeRight { get; private set; }
    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }
    public bool isSwipping { get; private set; }

    public Vector2 Dist;
    private Vector2 delta;
    
    void Update()
    {
        SwipeLeft = SwipeRight = SwipeDown = SwipeUp = false;
        for(int i = 0;i<Input.touchCount;i++)
        {
            Touch t = Input.GetTouch(i);
            switch (t.phase)
            {
                case TouchPhase.Began:
                    isSwipping = true;
                    break;
                case TouchPhase.Canceled:
                case TouchPhase.Ended:
                case TouchPhase.Stationary:
                    isSwipping = false;
                    this.delta = Vector2.zero;
                    break;
                case TouchPhase.Moved:
                    delta = t.deltaPosition;
                    float dX = delta.x;
                    float dY = delta.y;
                    
                    if (dX > 10)
                        SwipeRight = true;
                    else if (dX < -10)
                        SwipeLeft = true;

                    if (dY > 10)
                        SwipeUp = true;
                    else if (dY < -10)
                        SwipeDown = true;
                    
                    break;
            }
            Dist = delta;
        }
    }
}
