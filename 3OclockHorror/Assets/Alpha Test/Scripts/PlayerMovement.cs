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
    public bool walkingSounds;
    public Vector2 movement;
    public AudioManager manager;
    public bool isPlaying = false; //for audio
    public string playerFloor = "FirstFloor";

    public Inventory plyInv;

    //A list of all canvases that should block player movement
    public List<GameObject> Canvases; //Canvases that won't be deleted between scenes
    [SerializeField]private bool canMove = true;

    private UniversalControls uControls;

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

    }


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
            
            movement.x = uControls.Player.MovePlayer.ReadValue<Vector2>().x; //Input.GetAxisRaw("Horizontal");
            movement.y = uControls.Player.MovePlayer.ReadValue<Vector2>().y; //Input.GetAxisRaw("Vertical");

        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        
        CheckWalkState();

        #region footsteps sounds 
        if (movement.x != 0 || movement.y != 0)
        {
            walkingSounds = true;
        }
        else
        {
            walkingSounds = false;
        }

        if (walkingSounds && !isPlaying && manager != null && myRoom.getName() != "Outside" && !InvCanvas.activeSelf && !Journal.activeSelf && canMove)
        {
            manager.Play("Player Footsteps", true);
            isPlaying = true;
        }
        if (walkingSounds && !isPlaying && manager != null && myRoom.getName() == "Outside" && !InvCanvas.activeSelf && !Journal.activeSelf && canMove)
        {
            manager.Play("Snow Footsteps", true);
            isPlaying = true;
        }
        else
        {
            isPlaying = false;
        }
        #endregion
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
        //canMove = true;

        anim.SetBool("walkingLeft", false);
        anim.SetBool("walkingRight", false);
        anim.SetBool("walkingForwards", false);
        anim.SetBool("walkingBackwards", false);
    }
    public void ChangeCanMove(bool state)
    {
        canMove = state;
    }
}
