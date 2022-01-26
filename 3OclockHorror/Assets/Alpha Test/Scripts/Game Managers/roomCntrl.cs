using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class roomCntrl : MonoBehaviour
{
    private room CurrentRoom; // Current Room
    private room DestinationRoom; //DestRoom
    public GameObject entrancePointRoom;
    private PlayerMovement playerMovementScript;
    private invInput Listener;

    public bool transitionOnOff = true; //Use this toggle the transition on and off
    private float range = 0.5f;
    private float transitionTime = .3f;

    [Space]
    public bool locked;
    public Inventory PlayerInvetory;
    public Item MyKey;


    [SerializeField]
    GameObject lockCanv;
    [SerializeField]
    TaskListTracker taskManager;

    private AudioManager manager;

    private Image BlackBackground;

    bool opened = false;
    private UniversalControls uControls;

    [Space]
    public bool WatchHallwayTrigger = false;
    public WatcherAI watcher;
    [SerializeField]
    room WatcherHallway;


    private void Awake()
    {
        // get current room script
        CurrentRoom = this.gameObject.GetComponentInParent<room>();

        // get destination room script based on entrance point
        DestinationRoom = entrancePointRoom.transform.parent.parent.gameObject.GetComponent<room>(); 

        manager = FindObjectOfType<AudioManager>();
        if (playerMovementScript == null)
            playerMovementScript = GameObject.Find("Player2").GetComponent<PlayerMovement>();
        if (Listener == null)
            Listener = GameObject.Find("Listener").GetComponent<invInput>();
        
        BlackBackground = GameObject.Find("TransitionPanel").GetComponent<Image>();
        uControls = new UniversalControls();
        uControls.Enable();
    }
    private void OnDisable()
    {
        uControls.Disable(); 
    }
    private void Interact(InputAction.CallbackContext c)
    {
        Tooltip.Message = "";
        if (locked)
        {
            CheckKey();
        }
        else 
        {
            CameraCrossfade(playerMovementScript.gameObject, entrancePointRoom, DestinationRoom);
            manager.Play("Door Open", true);

        }
    }
    // Update is called once per frame
    void Update()
    {
        if(WatchHallwayTrigger)
        {
            if(WatcherHallway == null || watcher == null)
            {
                Debug.LogError("Missing WatcherHallway room script or Missing WatcherAI script at " + this.gameObject.name);
            }
            else
            {
                if (playerMovementScript.myRoom != WatcherHallway)
                {
                    ActivateHallway(false);
                }
            }
        }
    }
    void OnDrawGizmos()//Shows how far the player needs to be in order to use the door
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
        Vector3 plyPos = entrancePointRoom.transform.position;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(plyPos.x, plyPos.y - 0.3108585f, plyPos.z), new Vector3(0.1573486f, 0.1247783f, 1f));
    }
    private void OnDrawGizmosSelected()//Draws a line between the door and it's destination, which is markered by a red circle
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(gameObject.transform.position, entrancePointRoom.transform.position);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(entrancePointRoom.transform.position, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Player" && transitionOnOff) // no need to press the interact button
        {
            CameraCrossfade(playerMovementScript.gameObject, entrancePointRoom, DestinationRoom);
            
        }
        else if (collision.gameObject.tag == "Player" ) // need to press interact button
        {
            uControls.Player.Interact.performed += Interact;
        }
        else
        {
            //Same logic flow, just uses an NPC class instead of a playermovement class
            NPC exe = collision.gameObject.GetComponent<NPC>();
            if(exe != null)
            {
                if(exe.myRoom == CurrentRoom)
                {
                    exe.myRoom = DestinationRoom;
                }
                else
                {
                    exe.myRoom = CurrentRoom;
                }
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        uControls.Player.Interact.performed -= Interact;
    }

    public void CameraCrossfade(GameObject playerObject, GameObject entranceP, room Room)
    {
        StartCoroutine(ChangeRoom(playerObject, entranceP, Room));
       
    }
    private void ChangePlayerPosition(GameObject playerObject, GameObject entranceP)
    {

        playerObject.transform.position = entranceP.transform.position; // take player to the new place
        
    }
    IEnumerator ChangeRoom(GameObject playerObject, GameObject entranceP, room RoomNum)
    {
        //LeanTween.value(BlackBackground.gameObject, 0, 1, transitionTime).;
        playerObject.GetComponent<PlayerMovement>().myRoom = RoomNum;
        LeanTween.value(BlackBackground.gameObject, 0, 1, transitionTime).setEaseInBack().setOnUpdate((float val) =>
        {
            Color newColor = BlackBackground.color;
            newColor.a = val;
            BlackBackground.color = newColor;
        }).setOnComplete(()=> 
        {
            ChangePlayerPosition(playerObject, entranceP);
        });

        yield return new WaitForSeconds(transitionTime *2);
        LeanTween.value(BlackBackground.gameObject, 1, 0, transitionTime*2).setOnUpdate((float val) =>
        {
            Color newColor = BlackBackground.color;
            newColor.a = val;
            BlackBackground.color = newColor;
        }).setEaseInBack();


        if (WatchHallwayTrigger)
        {
            ActivateHallway(true);
        }

    }
    public float getRange() // This might Go away later (Gabe's update)
    {
        return range;
    }
    public void CheckKey()
    {
        if (PlayerInvetory != null && MyKey != null)
        {
            if (PlayerInvetory.ContainsItem(MyKey))
            {
                locked = false;
            }
            else
            {
                //Tooltip.Message = "This door is locked.";
                if(manager != null)
                {
                    manager.Play("Locked Door", false);
                }
            }
        }
        else if(lockCanv != null && !lockCanv.activeSelf)
        {
            lockCanv.SetActive(true);
            if (!opened)
            {
                taskManager.updateList("\n - A lock with roman numerals? Where would I get the code?");
                opened = true;
            }
        }
        else
        {
            //Tooltip.Message = "This door is locked.";
            Debug.LogError("Door is locked but there is no key or inv set");
        }
    }

    void ActivateHallway(bool toggle)
    {
        if (toggle)
        {
            watcher.WatcherHallway = true;
            watcher.circleAnim.gameObject.SetActive(true);
            watcher.ChangeRoom(WatcherHallway);
            playerMovementScript.GetComponent<LightMatch>().TurnOffLight(false);
        }
        else
        {
            watcher.WatcherHallway = false;
            watcher.circleAnim.gameObject.SetActive(false);
            playerMovementScript.GetComponent<LightMatch>().TurnOffLight(true);
        }
    }
}
