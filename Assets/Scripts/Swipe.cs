using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class Swipe :MonoBehaviour
{
    [SerializeField] private float MinSwipeDist = 50f; // pixels

    private Vector2 _startPos;
    private bool _isSwiping;

    private bool _wasStationary;
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void Update()
    {
        if (Touch.activeTouches.Count == 0) return;

        var touch = Touch.activeTouches[0];

        if (touch.phase == TouchPhase.Began)
        { // initial touch
            _startPos = touch.screenPosition;
            _isSwiping = true;
            _wasStationary = false;
        }
        else if (touch.phase == TouchPhase.Stationary && _isSwiping && !_wasStationary)
        { // if you stop moving, consider it the new start position
            _startPos = touch.screenPosition;
            _wasStationary = true;
        }
        else if(touch.phase == TouchPhase.Moved && _isSwiping)
        { // if you start moving after being stationary
            _wasStationary = false;
        }
        else if (touch.phase == TouchPhase.Ended && _isSwiping)
        { // finger has left screen
            Vector2 swipeDelta = touch.screenPosition - _startPos;

            if (swipeDelta.magnitude >= MinSwipeDist)
            {
                float angle = CalculateAngle(swipeDelta);
                Debug.Log($"Swipe angle: {angle:F1}");
            }

            _isSwiping = false;
            _wasStationary = false;
        }
    }

    public float CalculateAngle(Vector2 delta)
    {
        float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
        
        if (angle < 0) angle += 360f;

        return angle;

    }

    private void OnDisable()
    {
        _isSwiping = false;
        EnhancedTouchSupport.Disable();
    }
}


