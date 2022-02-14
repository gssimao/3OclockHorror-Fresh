﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightendingdoor : MonoBehaviour
{
    public sendMessage MessageRedBookHint;
    //The background data for the door to work
    [SerializeField]
    PlayerMovement player;
    [SerializeField]
    GameObject tpPoint;
    [SerializeField]
    room room;
    public invInput Listener;

    [Space] //The key for the door
    public bool locked;
    public Inventory pInv;
    public Item MyKey;

    //The symbols for the door
    [Space]
    [SerializeField]
    List<symbolUpdater> symbols;

    AudioManager manager;

    public Animator Fade;

    public EscCntrl escMenu;

    /*[SerializeField]
    TaskListTracker tasklist;
    bool taskGiven = false;*/

    bool opened = false;
    public bool transitionOnOff = true; //Use this toggle the transition on and off
    float transitionTime = 0.5f;

    UniversalControls uControls;
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
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(player.gameObject.transform.position, this.gameObject.transform.position);
        if (dist <= 0.5f)
        {
            //Listener.isFocus = false;
            if (uControls.Player.Interact.triggered && transitionOnOff)
            {
                if(locked)
                {
                    CheckKey();
                }

              

                if (player != null && !locked) //Make sure it's not null, check if door is locked
                {
                    CameraCrossfade(player.gameObject, tpPoint, player, room); // teleport player
                    //Turn on all the watchers
                    //Watcherbundle.SetActive(true);
                    escMenu.EndingComplete(true);
                    if (manager != null)
                    {
                        manager.Play("Door Open", true);
                    }
                }
            }
        }
    }

    public void CheckKey()
    {
        if (pInv != null && MyKey != null)
        {
            if (pInv.ContainsItem(MyKey)) // check if we have the key
            {
                locked = false;
                foreach (symbolUpdater sym in symbols)// check if all the symbols were turned on
                {
                    if (!sym.active)
                    {
                        locked = true;
                    }
                }
            }

            if (locked)
            {
                //toolTipScript.UpdateTooltipMessage("The handle won't budge - there appears to be a cross shaped slot.");
                MessageRedBookHint.PlayOnlyOnceTrigger();
                if (manager != null)
                {
                    manager.Play("Locked Door", false);
                }
            }
        }
    }

    public void CameraCrossfade(GameObject player, GameObject entranceP, PlayerMovement play, room RoomNum)
    {
        StartCoroutine(ChangeCamera(player, entranceP, play, RoomNum));
    }

    IEnumerator ChangeCamera(GameObject player, GameObject entranceP, PlayerMovement play, room RoomNum)
    {
        if (transitionOnOff)
        {
            Fade.gameObject.SetActive(true);
            Fade.SetTrigger("fadeOut");
        }

        player.transform.position = entranceP.transform.position;

        if (transitionOnOff)
        {
            yield return new WaitForSeconds(transitionTime);
            Fade.SetTrigger("fadeIn");
        }

        play.myRoom = RoomNum;
    }
}
