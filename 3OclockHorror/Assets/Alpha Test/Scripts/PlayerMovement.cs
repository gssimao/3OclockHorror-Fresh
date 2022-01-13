using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Journal;
    public GameObject InvCanvas;
    public float moveSpeed;
    public Animator anim;
    public room myRoom;
    public Rigidbody2D rb;
    public bool walking;
    public Vector2 movement;
    public AudioManager manager;
    public bool isPlaying = false; //for audio
    public string playerFloor = "FirstFloor";

    public Inventory plyInv;

    //A list of all canvases that should block player movement
    public List<GameObject> Canvases; //Canvases that won't be deleted between scenes
    public bool canMove = true;

    private UniversalControls uControls;
    private PlayerTouchWalk TouchWalkLogic;

    public bool leftSide = false;


    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent EventOnStartTouch;
    public delegate void PerformingTouchEvent(Vector2 position, float time);
    public event PerformingTouchEvent EventOnPerformedTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent EventOnEndTouch;

    private void Awake()
    {
        uControls = new UniversalControls();
        uControls.Enable();
        TouchWalkLogic = this.GetComponent<PlayerTouchWalk>();
    }
    private void OnDisable()
    {
        uControls.Disable();
    }
    void Start()
    {
        manager = FindObjectOfType<AudioManager>();

#if UNITY_ANDROID
        //Debug.Log("Android"); // this is working
#endif

#if UNITY_IPHONE
            Debug.Log("Iphone");
#endif

        //uControls.Player.TouchPress.started += context => StartTouch(context);

        //uControls.Player.TouchPress.performed += context => PerformTouch(context);
        //uControls.Player.TouchPress.canceled += context => EndTouch(context);



        /* if (manager != null)
         {
             //manager.Play("Heavy Wind");
         }
         //Debug.Log("hello");*/
    }

/*    void PerformTouch(InputAction.CallbackContext context) // delegate work to PlayerTouchWalk
    {
        Debug.Log("Touch Performed");
        if (EventOnPerformedTouch != null)
        {
            EventOnPerformedTouch(uControls.Player.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
        TouchWalkLogic.ChangeIsTouching(true);
    }
    void EndTouch(InputAction.CallbackContext context) // delegate work to PlayerTouchWalk
    {
        Debug.Log("Touch ended");
        if (EventOnEndTouch != null) EventOnEndTouch(uControls.Player.TouchPosition.ReadValue<Vector2>(), (float)context.time);

        TouchWalkLogic.ChangeIsTouching(false);
    }*/


    // Update is called once per frame
    void Update()
    {
       
        resetState();
        if (Canvases != null)
        {
            foreach (GameObject canv in Canvases)
            {
                if (canv.activeSelf)
                {
                    canMove = false;
                    //Debug.Log("Setting movement to false");
                }
            }
        }

        //Check if the player can move and is registering input
        if (canMove)
        {
            //for testing the touch
            movement.x = uControls.Player.MovePlayer.ReadValue<Vector2>().x; //Input.GetAxisRaw("Horizontal");
            movement.y = uControls.Player.MovePlayer.ReadValue<Vector2>().y; //Input.GetAxisRaw("Vertical");

            //for touch only
            /* if (TouchWalkLogic.GetIsTouching())
             {
                 TouchWalkLogic.ChangeTarget(uControls.Player.TouchPosition.ReadValue<Vector2>());
             }*/
            // ^^for touch only

        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        //Check the states for the walk animation.
        #region ChecKWalkStates 
        /*
                if (movement.x != 0 && movement.y != 0)
                {
                    if (movement.x < 0)
                    {
                        anim.SetBool("walkingLeft", true);
                    }

                    if (movement.x > 0)
                    {
                        anim.SetBool("walkingRight", true);
                    }
                }
                else
                {
                    if (movement.x != 0)
                    {
                        if (movement.x < 0)
                        {
                            anim.SetBool("walkingLeft", true);
                        }

                        if (movement.x > 0)
                        {
                            anim.SetBool("walkingRight", true);
                        }
                    }

                    if (movement.y != 0)
                    {
                        if (movement.y < 0)
                        {
                            anim.SetBool("walkingForwards", true);
                        }

                        if (movement.y > 0)
                        {
                            anim.SetBool("walkingBackwards", true);
                        }
                    }
                }*/
        #endregion
        CheckWalkState();

        if (movement.x != 0 || movement.y != 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

        if (walking && !isPlaying && manager != null && myRoom.getName() != "Outside" && !InvCanvas.activeSelf && !Journal.activeSelf && canMove)
        {
            manager.Play("Player Footsteps", true);
            isPlaying = true;
        }
        if (walking && !isPlaying && manager != null && myRoom.getName() == "Outside" && !InvCanvas.activeSelf && !Journal.activeSelf && canMove)
        {
            manager.Play("Snow Footsteps", true);
            isPlaying = true;
        }
        else
        {
            isPlaying = false;
        }
    }
    private void FixedUpdate()
    {
        // Movement
        if (canMove)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);  // take the comments out to go back to normal
        }
    }

    public void CheckWalkState()
    {

        if (movement.x != 0 && movement.y != 0)
        {
            if (movement.x < 0)
            {
                anim.SetBool("walkingLeft", true);
            }

            if (movement.x > 0)
            {
                anim.SetBool("walkingRight", true);
            }
        }
        else
        {
            if (movement.x != 0)
            {
                if (movement.x < 0)
                {
                    anim.SetBool("walkingLeft", true);
                }

                if (movement.x > 0)
                {
                    anim.SetBool("walkingRight", true);
                }
            }

            if (movement.y != 0)
            {
                if (movement.y < 0)
                {
                    anim.SetBool("walkingForwards", true);
                }

                if (movement.y > 0)
                {
                    anim.SetBool("walkingBackwards", true);
                }
            }
        }
    }


    public GameObject getJournal()
    {
        return Canvases[2];
    }
    public room GetMyroom() 
    {
        return myRoom;
    }
    public void changeRoom(room room)
    {
        myRoom = room;
    }
    public void resetState()
    {
        canMove = true;

        anim.SetBool("walkingLeft", false);
        anim.SetBool("walkingRight", false);
        anim.SetBool("walkingForwards", false);
        anim.SetBool("walkingBackwards", false);
    }
}
