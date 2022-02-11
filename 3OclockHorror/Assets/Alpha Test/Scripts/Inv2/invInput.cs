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
     /*
    [SerializeField]
    InputField jInput; //this was probably used for typing in the journal
       [SerializeField]
        List<GameObject> objs; // these used to hold the puzzles for some reason
    */
    [SerializeField]
    GameObject escCanv;

    [SerializeField] private bool bTriggerActive;
    public List<GameActions> defaultAction;
    public List<GameActions> exitActions;
    [SerializeField] private bool JournalActive = false;

    AudioManager manager;
    //private bool isFocus = true;

    private UniversalControls uControls;
    private void Awake()
    {
        
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
        if (JournalActive) //journal is open asking to close
            StartCoroutine(nameof(TriggerExitAction)); //close the journal 
        else if (!bTriggerActive) //journal is closed asking to be open
            StartCoroutine(nameof(TriggerAction)); //open journal
    }
    IEnumerator TriggerAction() // swipe opens
    {
        playJournalSound();
        JournalActive = true;
        for (int x = 0; x < defaultAction.Count; x++)
        {
            yield return new WaitForSeconds(defaultAction[x].delay);
            defaultAction[x].Action();
        }
    }
    IEnumerator TriggerExitAction() //swipe closes 
    {
        JournalActive = false;
        playJournalSound();
        for (int x = 0; x < exitActions.Count; x++)
        {
            yield return new WaitForSeconds(exitActions[x].delay);
            exitActions[x].Action();
        }
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


    void playJournalSound()
    {
        if (manager != null)
        {
            manager.Play("Journal", true);
        }
    }

}