using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.EnhancedTouch;

public class InputManager : MonoBehaviour
{
    private InputTouch touchControls;
    private PlayerScript playerScript;

    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void PerformingTouchEvent(Vector2 position, float time);
    public event PerformingTouchEvent PerformingTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    void Awake()
    {
        touchControls = new InputTouch();
        playerScript = GameObject.Find("Player2").GetComponent<PlayerScript>();
    }
    private void OnEnable()
    {
        touchControls.Enable();
        TouchSimulation.Enable();
        Debug.Log("Enable input manager");

       // UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }
    private void OnDisable()
    {
        touchControls.Disable();
        TouchSimulation.Disable();

        //UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }
    private void Start()
    {
        touchControls.mainControls.TouchPress.started += context => StartTouch(context);
        touchControls.mainControls.TouchPress.canceled += context => EndTouch(context);
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Started");
        if (OnStartTouch != null) OnStartTouch(touchControls.mainControls.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        playerScript.ChangeIsTouching(true);
    }
    void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch ended");
        if (OnEndTouch != null) OnEndTouch(touchControls.mainControls.TouchPosition.ReadValue<Vector2>(), (float)context.time);

        playerScript.ChangeIsTouching(false);
    }

/*    private void FingerDown(Finger finger)
    {
        if (OnStartTouch != null)
        {

            OnStartTouch(finger.screenPosition, Time.time);

        }
    }*/
    private void Update()
    {
        if(playerScript.GetIsTouching())
        {
            playerScript.ChangeTarget(touchControls.mainControls.TouchPosition.ReadValue<Vector2>());
        }
    }

}
