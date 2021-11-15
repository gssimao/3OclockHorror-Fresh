using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    private InputManager inputmanager;
    [SerializeField]
    private bool isTouching = false;
    [SerializeField]
    private Vector3 Target;

    private void Awake()
    {
        inputmanager = GameObject.Find("Player2").GetComponent<InputManager>();
    }
    private void OnEnable()
    {
        inputmanager.OnStartTouch += GoToTarget;
    }
    private void OnDisable()
    {
        inputmanager.OnStartTouch -= GoToTarget;
        
    }
    private void Update()
    {
        if(isTouching)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target, .01f);
            //Target =  inputmanager.GetFingerPosition();
        }
    }

    void GoToTarget(Vector2 screenPosition, float time)
    {
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane);
        Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
        Target = worldCoordinates;

        // = Camera.main.ScreenToWorldPoint(TargetPosition);
    }

    public void ChangeIsTouching(bool value)
    {
        isTouching = value;
    }
    public bool GetIsTouching()
    {
        return isTouching;
    }
    public void ChangeTarget(Vector2 screenPosition)
    {
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane);
        Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
        Target = worldCoordinates;
    }
}
