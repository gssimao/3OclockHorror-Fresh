using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTouchWalk : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField]
    private bool isTouching = false;
    [SerializeField]
    private Vector3 Target;

    private void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
    }
    private void OnEnable()
    {
        //playerMovement.EventOnStartTouch += GoToTarget;
        playerMovement.EventOnPerformedTouch += GoToTarget;
    }
    private void OnDisable()
    {
        //playerMovement.EventOnStartTouch -= GoToTarget;
        playerMovement.EventOnPerformedTouch -= GoToTarget;

    }
    private void Update()
    {
        if(isTouching)
        {


        }


    }

    void GoToTarget(Vector2 screenPosition, float time)
    {
        /*Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane);
        Vector3 worldCoordinates = Camera.main.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
        Target = worldCoordinates;*/

        // = Camera.main.ScreenToWorldPoint(TargetPosition);
    }

    public void ChangeIsTouching(bool value)
    {
        isTouching = value;
        playerMovement.movement.x = 0;
        playerMovement.movement.y = 0;
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
    private float ConvertToNewRangeX(Vector3 target, Vector3 currentPos)
    {
        
        if (target.x > currentPos.x) // going to the right
        {
            return 1;
        }
        else// going to the left
        {
            return -1;
        }
        
        
    }
   
}
