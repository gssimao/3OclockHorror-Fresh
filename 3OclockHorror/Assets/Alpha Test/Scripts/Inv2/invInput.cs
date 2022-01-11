using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class invInput : MonoBehaviour
{
    /*[SerializeField]
    KeyCode invKey;
    [SerializeField]
    KeyCode escKey;*/
    [SerializeField]
    GameObject Journal;
    /*[SerializeField]
    GameObject invCanvas;
    [SerializeField]
    GameObject tooltip;*/
    [SerializeField]
    InputField jInput;
    [SerializeField]
    List<GameObject> objs;
    [SerializeField]
    GameObject escCanv;

    [SerializeField] private bool candles = false;
    [SerializeField] private bool WorkBench = false;
    [SerializeField] private bool Ladder = false;
    [SerializeField] private bool Note = false;
    [SerializeField] private bool RoomTeleport = false;

    [SerializeField] private Image InteractDrawer;
    private Vector3 DrawerOriginalPosition;


    AudioManager manager;
    private bool isFocus = true;

    private UniversalControls uControls;
    private void Awake()
    {
        DrawerOriginalPosition = InteractDrawer.gameObject.transform.localPosition;
        uControls = new UniversalControls();
        uControls.Enable();
        uControls.Player.Interact.performed += Interaction;
        //InteractibleTrigger.EnableJournal += EnableJournal;
        InteractibleTrigger.DisableJournal += DisableJournal;
    }
    private void OnDisable()
    {
        uControls.Disable();
        uControls.Player.Interact.performed -= Interaction;
        //InteractibleTrigger.EnableJournal -= EnableJournal;
        InteractibleTrigger.DisableJournal -= DisableJournal;
    }

    void Start()
    {
        manager = FindObjectOfType<AudioManager>();
    }

    //private void EnableJournal() { isFocus = true; }
    private void DisableJournal() { isFocus = false; }

    private void Interaction(InputAction.CallbackContext c)
    {
        if (WorkBench)
        {
            OpenBench.TriggerBench();
            return;
        }
        else if (candles)
        {
            Debug.Log("Do the thing with the Candles");
            return;
        }
        else if (Note)
        {
            Debug.Log("Reading Note");
            return;
        }
        else if (RoomTeleport)
        {
            Debug.Log("Teleporting to new room");
            return;
        }
        else
        {
            playSound();
            if (Journal.activeSelf)
            {
                Journal.SetActive(false);
            }
            else
            {
                Journal.SetActive(true);
            }
        }


    }

    private void ShowJournal(InputAction.CallbackContext c)
    {
        if (!isFocus) return;
        
        if (!jInput.isFocused)
        {
            if (Journal.activeSelf)
            {
                Journal.SetActive(false);
                playSound();

            }
            else
            {
                Journal.SetActive(true);
                playSound();
            }

            /*
            if (invCanvas.activeSelf)
            {
                tooltip.SetActive(false);
                invCanvas.SetActive(false);
            }
            else
            {
                invCanvas.SetActive(true);
            }
            */
        }
       
    }

    void ShowPauseMenu(InputAction.CallbackContext c)
    {
        if (!escCanv.activeSelf)
        {
            escCanv.SetActive(true);
        }
        else
        {
            escCanv.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        bool puzOpen = false;
        foreach(GameObject obj in objs)
        {
            if(obj.activeSelf)
            {
                puzOpen = true;
            }
        }
        /*
        if (isFocus)
        {
            if (uControls.Player.Interact.triggered && !jInput.isFocused && !puzOpen)
            {
                if (Journal.activeSelf)
                {
                    Journal.SetActive(false);
                    playSound();

                }
                else
                {
                    Journal.SetActive(true);
                    playSound();
                }
            }
        }*/
        if (uControls.Player.PauseMenu.triggered)
        {
            if (!escCanv.activeSelf)
            {
                escCanv.SetActive(true);
            }
            else
            {
                escCanv.SetActive(false);
            }
        }
        //isFocus = true;
    }

    void playSound()
    {
        if (manager != null)
        {
            manager.Play("Journal", true);
        }
    }

    

    public void BenchSwitch(bool state)
    {
        //LeanTween.moveY(InteractDrawer.gameObject, InteractDrawer.gameObject.transform.position.y+.5f, .5f).setEase(LeanTweenType.easeInQuad);

        WorkBench = state;
       /* if (state)
            StartCoroutine(LerpTopItem((InteractDrawer.gameObject.transform.localPosition + new Vector3(0,-195, 0)), .5f, InteractDrawer.gameObject));
        else
            StartCoroutine(LerpTopItem((InteractDrawer.gameObject.transform.localPosition + new Vector3(0, +195, 0)), .5f, InteractDrawer.gameObject));*/
    }
    public GameObject GetInteractDrawer()
    {
        return InteractDrawer.gameObject;
    }
    public void AdjustDrawer(Vector3 FinalbenchPosition, float interpolate)
    {
        InteractDrawer.transform.localPosition = Vector3.Lerp(GetOriginalDrawerPosition(), FinalbenchPosition, interpolate);
    }
    public Vector3 GetOriginalDrawerPosition()
    {
        return DrawerOriginalPosition;
    }

    public void CandleSwitch(bool state)
    {
        candles = state;
    }
    public void NoteSwitch(bool state)
    {
        Note = state;
    }
    public void LadderSwitch(bool state)
    {
        Ladder = state;
    }
    public void RoomTeleportSwitch(bool state)
    {
        RoomTeleport = state;
    }


}