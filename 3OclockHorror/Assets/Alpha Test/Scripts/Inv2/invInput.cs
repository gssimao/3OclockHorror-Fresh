using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

#if UNITY_ANDROID
//Debug.Log("Android"); // this is working
#endif

public class invInput : MonoBehaviour
{

    /*[SerializeField]
    KeyCode invKey;
    [SerializeField]
    KeyCode escKey;*/

    /*[SerializeField]
    GameObject invCanvas;
    [SerializeField]
    GameObject tooltip;*/
    [SerializeField]
    GameObject Journal;
    [SerializeField]
    InputField jInput;
    [SerializeField]
    List<GameObject> objs;
    [SerializeField]
    GameObject escCanv;

    [SerializeField] private bool bTriggerActive;

    [SerializeField] private Image InteractDrawer;
    private Vector3 DrawerOriginalPosition;

    AudioManager manager;
    //private bool isFocus = true;

    private UniversalControls uControls;
    private void Awake()
    {
        DrawerOriginalPosition = InteractDrawer.gameObject.transform.localPosition;
        uControls = new UniversalControls();
        uControls.Enable();
        uControls.Player.Interact.performed += Interaction;// player controls
        uControls.Player.PauseMenu.performed += PauseGame;

        //InteractibleTrigger.EnableJournal += EnableJournal;
        //InteractibleTrigger.DisableJournal += DisableJournal;
    }
    private void OnEnable()
    {
        GameActionTrigger.TriggerInactive += TriggerInactive;
        GameActionTrigger.TriggerActive += TriggerActive;
    }
    private void OnDisable()
    {
        uControls.Disable();
        uControls.Player.Interact.performed -= Interaction; // player controls

        //InteractibleTrigger.EnableJournal -= EnableJournal;
        //InteractibleTrigger.DisableJournal -= DisableJournal;
        uControls.Player.PauseMenu.performed -= PauseGame;

        GameActionTrigger.TriggerInactive -= TriggerInactive;
        GameActionTrigger.TriggerActive -= TriggerActive;
    }

    void Start()
    {
        manager = FindObjectOfType<AudioManager>();
    }

    //private void EnableJournal() { isFocus = true; }
    //private void DisableJournal() { isFocus = false; }

    private void Interaction(InputAction.CallbackContext c)
    {
        ShowJournal(c);
    }
    private void PauseGame(InputAction.CallbackContext c)
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

    // triggerActive an trigger Inactive are resposible to check if the player is touching anything
    //if the player happens to not be touching anything we want to pull up the journal.
    private void TriggerActive()
    {
        bTriggerActive = true;
    }
    private void TriggerInactive()
    {
        bTriggerActive = false;
    }


    private void ShowJournal(InputAction.CallbackContext c)
    {
        if (Journal.activeSelf)
        {
            Journal.SetActive(false);
            playJournalSound();

        }
        else if (!bTriggerActive)
        {
            Journal.SetActive(true);
            playJournalSound();
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

        /*if (uControls.Player.PauseMenu.triggered)
        {
           
        }*/
        //isFocus = true;
    }

    void playJournalSound()
    {
        if (manager != null)
        {
            manager.Play("Journal", true);
        }
    }

}